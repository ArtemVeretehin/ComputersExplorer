using ComputersExplorer.DBO;
using System.Linq.Expressions;

namespace ComputersExplorer.Repositories
{
    /// <summary>
    /// Интерфейс, декларирующий основные действия с сущностями 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindWithInclude(Expression<Func<T, bool>> expression, Expression<Func<T, Role>> navigationPath);
        IEnumerable<T> GetAllWithInclude(Expression<Func<T, IEnumerable<Computer>>> navigationPath);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
