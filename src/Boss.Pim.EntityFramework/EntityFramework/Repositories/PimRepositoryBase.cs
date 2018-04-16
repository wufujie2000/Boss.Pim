using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Boss.Pim.EntityFramework.Repositories
{
    public abstract class PimRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<PimDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected PimRepositoryBase(IDbContextProvider<PimDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class PimRepositoryBase<TEntity> : PimRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected PimRepositoryBase(IDbContextProvider<PimDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
