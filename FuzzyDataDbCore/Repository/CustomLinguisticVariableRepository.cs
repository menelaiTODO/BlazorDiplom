using FuzzyDataDbCore.DatabaseContext;
using FuzzyDataDbCore.Models;
using FuzzyDataDbCore.Repository.Base;

namespace FuzzyDataDbCore.Repository
{
    public class CustomLinguisticVariableRepository : RepositoryBase
    {
        private object _lockValue = new object();

        public CustomLinguisticVariableRepository(FuzzyDataDbContext dbContext) : base(dbContext) { }

        public void SaveCustomLinguisticVariable(CustomLinguisticVariable data, IEnumerable<(double XValue, double YValue)> points)
        {
            using (var tran = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.CustomLinguisticVariables.Add(data);
                    DbContext.SaveChanges();

                    var i = 0;
                    foreach (var item in points)
                    {
                        var p = new Point 
                        {
                            XValue = item.XValue,
                            YValue = item.YValue,
                            PointSeq = i,
                            CreatedDate = data.CreatedDate,
                            CreatorName = data.CreatorName,
                            CustomLinguisticVariableId = data.Id
                        };

                        DbContext.Points.Add(p);

                        i++;
                    }

                    DbContext.SaveChanges();

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }
        }
    }
}
