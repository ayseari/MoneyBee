namespace MoneyBee.Customer.Infrastructure.Persistence.Configurations
{
    using MoneyBee.Customer.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            // Table
            builder.ToTable("Customers");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.NationalId)
                   .HasMaxLength(11);

            builder.Property(x => x.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.DateOfBirth)
                   .IsRequired();

            builder.Property(x => x.TaxNumber)
                   .HasMaxLength(20);

            builder.Property(x => x.CompanyName)
                   .HasMaxLength(200);

            builder.Property(x => x.UpdatedAt)
                    .IsRequired(false);

            // Enums
            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasConversion<byte>();

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<byte>();

            // Indexes
            builder.HasIndex(x => x.NationalId)
                   .IsUnique()
                   .HasFilter("[NationalId] IS NOT NULL");

            builder.HasIndex(x => x.PhoneNumber)
                   .IsUnique();

            builder.HasIndex(x => x.TaxNumber)
                   .HasFilter("[TaxNumber] IS NOT NULL");
        }
    }

}
