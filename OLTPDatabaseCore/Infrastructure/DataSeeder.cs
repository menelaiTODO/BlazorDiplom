using Microsoft.EntityFrameworkCore;
using OLTPDatabaseCore.DatabaseContext;
using OLTPDatabaseCore.Models;

namespace OLTPDatabaseCore.Infrastructure
{
    public class DataSeeder
    {
        // количество компаний
        private const int CompaniesCount = 1000;
        
        // количество товаров в ассортименте
        private const int GoodsCount = 20000;

        // минимальная цена продажи товара
        private const double SalesPriceMinValue = 50;

        // максимальная цена продажи товара
        private const double SalesPriceMaxValue = 200000;

        // количество заказов
        private const double OrderCount = 10000;

        // максимальное кол-во товаров в заказе
        private const int MaxGoodsCountInOrder = 5;

        // генерация ИНН компаний
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

                if(context.Goods.Any())
                    context.Goods.ExecuteDelete();

                // генерация товаров
                for (int i = 0; i < GoodsCount; i++)
                {
                    context.Goods.Add(new Goods { Name = $"Товар #{i}", SalePrice = randomGeneratorSalePrice.NextDouble() * (SalesPriceMaxValue - SalesPriceMinValue) + SalesPriceMinValue, CreatorName = "lyubimov_ad" });
                    Console.WriteLine($"Создан товар c идентификатором {i}");
                }
               
                context.SaveChanges();

                if (context.Orders.Any())
                    context.Orders.ExecuteDelete();

                if (context.OrderGoods.Any())
                    context.OrderGoods.ExecuteDelete();

                var random = new Random();

                // генерация заказов
                for (int i = 0; i < OrderCount; i++)
                {
                    var randomCompany = context.Companies.OrderBy(item => Guid.NewGuid()).First();

                    var startDate = new DateTime(2023, 1, 1);
                    var daysToAdd = random.Next(364); 
                    var orderDate = startDate.AddDays(daysToAdd);

                    var goodsCountInOrder = random.Next(MaxGoodsCountInOrder);

                    var order = new Order { CompanyId = randomCompany.Id, CreatedDate = orderDate, CreatorName = "lyubimov_ad" };
                    
                    // создаем заказ
                    context.Orders.Add(order);

                    context.SaveChanges();
                   
                    var randomOrderGoods = context.Goods.OrderBy(item => Guid.NewGuid()).Take(random.Next(MaxGoodsCountInOrder)).Select(item => new OrderGoods { 
                        OrderId = order.Id,
                        GoodsId = item.Id,
                        CreatorName = "lyubimov_ad",
                        CreatedDate = orderDate
                    });

                    // добавим товары в корзину
                    context.OrderGoods.AddRange(randomOrderGoods);

                    Console.WriteLine($"Сгенерирован заказ {order.Id}");
                }

                context.SaveChanges();
                transaction.Commit();
            }
        }
    }
}
