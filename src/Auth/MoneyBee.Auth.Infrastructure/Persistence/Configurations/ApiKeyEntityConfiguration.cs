namespace MoneyBee.Auth.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MoneyBee.Auth.Domain.Entities;

    /// <summary>
    /// Api key entity configuration
    /// </summary>
    public class ApiKeyEntityConfiguration : IEntityTypeConfiguration<ApiKeyEntity>
    {
        public void Configure(EntityTypeBuilder<ApiKeyEntity> entity)
        {
            entity.ToTable("ApiKeys");

            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.HashedKey).IsUnique();
            entity.HasIndex(e => e.IsActive);

            entity.Property(e => e.HashedKey).IsRequired().HasMaxLength(64);
            entity.Property(e => e.ClientName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
        }
    }
}