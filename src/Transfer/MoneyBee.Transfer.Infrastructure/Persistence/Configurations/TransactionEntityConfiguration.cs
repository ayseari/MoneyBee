namespace MoneyBee.Transfer.Infrastructure.Persistence.Configurations
{
    using MoneyBee.Transfer.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Transaction Entity Configuration
    /// </summary>
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            // Table
            builder.ToTable("Transactions");

            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            // Transaction
            builder.Property(x => x.SenderId)
                   .IsRequired();

            builder.Property(x => x.ReceiverId)
                   .IsRequired();

            builder.Property(x => x.TransactionCode)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Fee)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Currency)
                   .IsRequired();

            builder.Property(x => x.AmountInTRY)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ExchangeRate)
                   .HasPrecision(18, 6);

            builder.Property(x => x.RiskLevel)
                   .IsRequired();

            builder.Property(x => x.RiskScore)
                   .IsRequired();

            builder.Property(x => x.FeeRefunded)
                   .IsRequired();

            // Indexes
            builder.HasIndex(x => x.TransactionCode)
                   .IsUnique();

            builder.HasIndex(x => x.SenderId);
            builder.HasIndex(x => x.ReceiverId);
            builder.HasIndex(x => x.CreatedAt);

            builder.HasMany(x => x.StatusLogs)
                   .WithOne(x => x.Transaction)
                   .HasForeignKey(x => x.TransactionId);

        }
    }
}