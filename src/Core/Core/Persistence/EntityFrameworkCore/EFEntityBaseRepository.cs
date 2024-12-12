using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.EntityFrameworkCore
{
    public class EFEntityBaseRepository<TEntity, TContext> : IEntityBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        public EFEntityBaseRepository(TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; }

        public TEntity Add(TEntity entity)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            return Context.Add(entity).Entity;
        }

        public TEntity Update(TEntity entity)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            Context.Update(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().FirstOrDefault(expression);
        }


        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            return await query.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? Context.Set<TEntity>().AsNoTracking()
                : Context.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query.ToListAsync();
        }



        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

        public TResult InTransaction<TResult>(Func<TResult> action, Action successAction = null, Action<Exception> exceptionAction = null)
        {
            var result = default(TResult);
            try
            {
                if (Context.Database.ProviderName.EndsWith("InMemory"))
                {
                    result = action();
                    SaveChanges();
                }
                else
                {
                    using var tx = Context.Database.BeginTransaction();
                    try
                    {
                        result = action();
                        SaveChanges();
                        tx.Commit();
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        throw;
                    }
                }

                successAction?.Invoke();
            }
            catch (Exception ex)
            {
                if (exceptionAction == null)
                {
                    throw;
                }

                exceptionAction(ex);
            }

            return result;
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return await Context.Set<TEntity>().CountAsync();
            }
            else
            {
                return await Context.Set<TEntity>().CountAsync(expression);
            }
        }

        public int GetCount(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Context.Set<TEntity>().Count() : Context.Set<TEntity>().Count(expression);
        }
    }
}
