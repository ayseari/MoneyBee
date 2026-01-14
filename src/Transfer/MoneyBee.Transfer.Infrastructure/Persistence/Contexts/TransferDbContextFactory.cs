//namespace MoneyBee.Transfer.Infrastructure.Persistence.Contexts
//{
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.Design;
//    using Microsoft.Extensions.Configuration;

//    public class TransferDbContextFactory
//    : IDesignTimeDbContextFactory<TransferDbContext>
//    {
//        public TransferDbContext CreateDbContext(string[] args)
//        {
//            var basePath = Directory.GetCurrentDirectory();

//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(basePath)
//                .AddJsonFile("appsettings.json", optional: false)
//                .Build();

//            var optionsBuilder = new DbContextOptionsBuilder<TransferDbContext>();

//            optionsBuilder.UseSqlServer(
//                configuration.GetConnectionString("MoneyBeeTransferConnStr"));

//            return new TransferDbContext(optionsBuilder.Options);
//        }
//    }
//}
