using AutoMapper;
using SuperSeller.Data;

namespace SuperSeller.Services
{
    public abstract class BaseEFService
    {
        protected BaseEFService(
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        protected ApplicationDbContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}