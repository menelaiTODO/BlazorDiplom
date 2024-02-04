using DatawarehouseCore.DatabaseContext;
using DatawarehouseCore.Models;
using OLTPDatabaseCore.DatabaseContext;

namespace OLTPDatabaseCore.Jobs
{
    public class SalesETLJob : IJob
    {
        private OLTPDbContext _oltpContext;
        private DWHDbContext _dwhDbContext;
        private bool _isRunning;

        public SalesETLJob(OLTPDbContext dbContext, DWHDbContext dWHDbContext)
        {
            _oltpContext = dbContext;
            _dwhDbContext = dWHDbContext;
        }

        public string Name => nameof(SalesETLJob);

        public DateTime? LastRunDatetime
        {
            get
            {
                return _oltpContext.JobLastRunDateTimes.Where(item => item.Name == Name).FirstOrDefault()?.LastRunDateTime;
            }
            set
            {
                if (value is null)
                    throw new ArgumentNullException("Последний запуск джоба не может быть null");

                var item = _oltpContext.JobLastRunDateTimes.Where(item => item.Name == Name).FirstOrDefault();

                if (item is null)
                    _oltpContext.JobLastRunDateTimes.Add(new Models.JobLastRunDateTime { Name = Name, LastRunDateTime = (DateTime)value });
                else
                    item.LastRunDateTime = (DateTime)value;
            }
        }

        public async Task RunAsync()
        {
            if (_isRunning)
                return;

            await Task.Run(() =>
            {
                _isRunning = true;

                using var transaction = _dwhDbContext.Database.BeginTransaction();
                try
                {
                    var startDateTime = DateTime.Now;

                    var lastRun = LastRunDatetime ?? new DateTime(2000, 1, 1);

                    var productsDWH = _oltpContext.Goods.Where(item => item.CreatedDate >= lastRun)
                        .Select(item => new DimProduct { ProductId = item.Id, Name = item.Name });


                    _dwhDbContext.DimProducts.AddRangeAsync(productsDWH);

                    var ordersDWH = _oltpContext.Orders.Where(item => item.CreatedDate >= lastRun)
                        .Select(item => new DimOrder { OrderId = item.Id, AccountName = item.CreatorName });

                    _dwhDbContext.DimOrders.AddRangeAsync(ordersDWH);

                    var factSalesDWH = (from og in _oltpContext.OrderGoods.Where(item => item.CreatedDate >= lastRun)
                                        join g in _oltpContext.Goods on og.GoodsId equals g.Id
                                        select new FactSales
                                        {
                                            ProductId = og.GoodsId,
                                            OrderId = og.OrderId,
                                            OrderDate = og.CreatedDate,
                                            Sum = g.SalePrice
                                        });

                    _dwhDbContext.FactSales.AddRange(factSalesDWH);

                    var dwhDates = factSalesDWH.Select(item => new DimDate { OrderDate = item.OrderDate, Day = (int)item.OrderDate.DayOfWeek, Year = item.OrderDate.Year, Month = item.OrderDate.Month, MonthName = item.OrderDate.ToString("MMMM") });

                    _dwhDbContext.DimDates.AddRange(dwhDates); 
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    _dwhDbContext.SaveChanges();
                    transaction.Commit();
                    _isRunning = false;
                }
            });
        }
    }
}
