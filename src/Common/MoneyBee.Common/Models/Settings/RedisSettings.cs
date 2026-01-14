namespace MoneyBee.Common.Models.Settings
{
    public class RedisSettings
    {
        public const string SectionName = "RedisSettings";

        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
    }
}
