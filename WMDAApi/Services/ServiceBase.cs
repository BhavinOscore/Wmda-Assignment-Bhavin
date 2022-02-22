using WMDAApi.Models;

namespace WMDAApi.Services
{
    public abstract class ServiceBase
    {
        protected readonly IAppDbContext _dbContext;
        public ServiceBase(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
