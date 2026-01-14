namespace MoneyBee.Transfer.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MoneyBee.Transfer.Domain.Entities;

    public class TransactionStatusLogEntityConfiguration
        : IEntityTypeConfiguration<TransactionStatusLogEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionStatusLogEntity> builder)
        {
            builder.ToTable("TransactionStatusLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd(); // long identity

            builder.Property(x => x.TransactionId)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.ChangedBy)
                   .IsRequired()
                   .HasMaxLength(36);

            builder.Property(x => x.ChangeReason)
                   .HasMaxLength(250);

            builder.Property(x => x.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasOne(x => x.Transaction)
                   .WithMany(t => t.StatusLogs)
                   .HasForeignKey(x => x.TransactionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ⚡ Index
            builder.HasIndex(x => x.TransactionId);
        }
    }
}