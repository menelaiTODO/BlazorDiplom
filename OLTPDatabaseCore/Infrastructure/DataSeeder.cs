using Microsoft.EntityFrameworkCore;
using OLTPDatabaseCore.DatabaseContext;
using OLTPDatabaseCore.Models;

namespace OLTPDatabaseCore.Infrastructure
{
    public class DataSeeder
    {
        private const int CompaniesCount = 1000;
        private const int GoodsCount = 20000;
        private const double SalesPriceMinValue = 50;
        private const double SalesPriceMaxValue = 200000;

        private const long TaxpayerNumberMinValue = 100000000000;
        private const long TaxpayerNumberMaxValue = 999999999999;

        public void SeedData(OLTPDbContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                if (context.Companies.Any())
                    context.Companies.ExecuteDelete();

                var randomGeneratorTaxPayerNumber = new Random();
                var randomGeneratorSalePrice = new Random();

                // генерация компаний
                for (int i = 0; i < CompaniesCount; i++)
                {
                    context.Companies.Add(new Company { Name = $"Компания #{i}", TaxpayerNumber = randomGeneratorTaxPayerNumber.NextInt64(TaxpayerNumberMinValue, TaxpayerNumberMaxValue).ToString(), CreatorName = "lyubimov_ad" });
                    Console.WriteLine($"Создана компания c идентификатором {i}");
                }

                // генерация товаров
                for (int i = 0; i < GoodsCount; i++)
                {
                    context.Goods.Add(new Goods { Name = $"Товар #{i}", SalePrice = randomGeneratorSalePrice.NextDouble() * (SalesPriceMaxValue - SalesPriceMinValue) + SalesPriceMinValue, CreatorName = "lyubimov_ad" });
                    Console.WriteLine($"Создан товар c идентификатором {i}");
                }

                context.SaveChanges();
                transaction.Commit();
            }
        }
    }
}
