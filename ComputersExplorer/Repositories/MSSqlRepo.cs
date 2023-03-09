using ComputersExplorer.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace ComputersExplorer.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        IEnumerable<T> FindWithInclude(Expression<Func<T, bool>> expression, Expression<Func<T,Role>> navigationPath);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }

    interface IContextSaveChanges
    {
        Task<int> SaveChanges();
    }

    public class MsSqlRepo<T> : IContextSaveChanges, IRepository<T> where T : class
    {
        ComputersExplorerContext _context;

        public MsSqlRepo(ComputersExplorerContext context)
        {
            this._context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IEnumerable<T> FindWithInclude(Expression<Func<T, bool>> expression, Expression<Func<T,Role>> navigationPath)
        {
            return _context.Set<T>().Where(expression).Include(navigationPath);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }



    public class UserRepository : MsSqlRepo<User>
    {
        public UserRepository(ComputersExplorerContext context) : base(context)
        { }
    }

    public class ComputerRepository : MsSqlRepo<Computer>
    {
        public ComputerRepository(ComputersExplorerContext context) : base(context)
        { }
    }

    public class RoleRepository : MsSqlRepo<Role>
    {
        public RoleRepository(ComputersExplorerContext context) : base(context)
        { }
    }

}








