using Hans.UserSample.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hans.UserSample.Infrastructure.Data
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public async Task Save(TModel instance)
        {
            _context.Set<TModel>().Add(instance);
            _context.Entry(instance).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task Update(TModel instance)
        {
            _context.Set<TModel>().Attach(instance);
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TModel instance)
        {
            _context.Set<TModel>().Remove(instance);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<TModel>> FindAllAsync()
        {
            return await _context.Set<TModel>().ToListAsync();
        }

        public async Task<IList<TModel>> FindAllByAsync(Expression<Func<TModel, bool>> match)
        {
            return await _context.Set<TModel>().AsNoTracking().Where(match).ToListAsync();
        }

        public Task<TModel> FindOneByAsync(Expression<Func<TModel, bool>> match)
        {
            return _context.Set<TModel>().AsNoTracking().FirstOrDefaultAsync(match);
        }
    }
}
