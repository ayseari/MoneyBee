namespace MoneyBee.Common.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using MoneyBee.Common.Caching;
    using MoneyBee.Common.Constants;
    using MoneyBee.Common.Helpers;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Api KEy auth middleware
    /// </summary>
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IRedisDistributedCacheService _cache;
        private static readonly ConcurrentDictionary<string, Queue<DateTime>> _requestLog = new();
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration, IRedisDistributedCacheService cache)
        {
            _next = next;
            _configuration = configuration;
            _cache = cache;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Health check endpoint'i bypass
            if ((context.Request.Path.StartsWithSegments("/health") || context.Request.Path.StartsWithSegments("/swagger")) && context.Request.Method == HttpMethods.Get)
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("X-API-Key", out var apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing");
                return;
            }
            // API Key validation
            var hashedApiKey = ApiKeyHelper.HashApiKey(apiKey.ToString());
            var validKeys = await _cache.GetAsync<List<string>>(RedisConstants.ApiKeysCacheKey) ?? new List<string>(); ;
            if (!validKeys.Contains(hashedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            // Rate limiting check
            if (!await CheckRateLimitAsync(apiKey.ToString()))
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit exceeded. Maximum 100 requests per minute allowed");
                return;
            }

            await _next(context);
        }

        /// <summary>
        /// Check rate limit
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        private async Task<bool> CheckRateLimitAsync(string apiKey)
        {
            await _semaphore.WaitAsync();
            try
            {
                var now = DateTime.UtcNow;
                if (!_requestLog.ContainsKey(apiKey))
                {
                    _requestLog[apiKey] = new Queue<DateTime>();
                }

                var requests = _requestLog[apiKey];

                // Son 1 dakikadan eski requestleri temizle
                while (requests.Count > 0 && requests.Peek() < now.AddMinutes(-1))
                {
                    requests.Dequeue();
                }

                if (requests.Count >= 100)
                {
                    return false;
                }

                requests.Enqueue(now);
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
