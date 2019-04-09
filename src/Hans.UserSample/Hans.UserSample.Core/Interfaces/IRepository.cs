using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hans.UserSample.Core.Interfaces
{
    public interface IRepository<TModel> where TModel : class
    {
        Task Save(TModel instance);
        Task Update(TModel instance);
        Task Delete(TModel instance);
        Task<IList<TModel>> FindAllAsync();
        Task<IList<TModel>> FindAllByAsync(Expression<Func<TModel, bool>> match);
        Task<TModel> FindOneByAsync(Expression<Func<TModel, bool>> match);
    }
}
