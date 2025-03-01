using FuzzyDataDbCore.DatabaseContext;
using FuzzyDataDbCore.Repository.Interface;

namespace FuzzyDataDbCore.Repository.Base
{
    public abstract class RepositoryBase : IRepository
    {
        protected FuzzyDataDbContext DbContext { get; set; }

        public RepositoryBase(FuzzyDataDbContext dbContext) 
        {
            DbContext = dbContext;
        }
    }
}
