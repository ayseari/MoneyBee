namespace MoneyBee.Common.DomainModels
{
    using System;

    /// <summary>
    /// Base entity class
    /// </summary>
    public abstract class Entity<T>
    {
        protected Entity()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}