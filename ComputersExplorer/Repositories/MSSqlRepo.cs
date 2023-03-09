using ComputersExplorer.DBO;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace ComputersExplorer.Repositories
{    
    /// <summary>
    /// Класс, реализующий интерфейсы IContextSaveChanges, IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MsSqlRepo<T> : IContextSaveChanges, IRepository<T> where T : class
    {
        ComputersExplorerContext _context;

        public MsSqlRepo(ComputersExplorerContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Добавление сущности в контекст
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Добавление списка сущностей в контекст
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Выборка данных из контекста по определенному условию
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        /// <summary>
        /// Выборка данных из контекста по определенному условию с подгрузкой связанной сущности
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="navigationPath"></param>
        /// <returns></returns>
        public IEnumerable<T> FindWithInclude(Expression<Func<T, bool>> expression, Expression<Func<T,Role>> navigationPath)
        {
            return _context.Set<T>().Where(expression).Include(navigationPath);
        }

        /// <summary>
        /// Выгрузка всех данных из контекста с подгрузкой связанной сущности
        /// </summary>
        /// <param name="navigationPath"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllWithInclude(Expression<Func<T, IEnumerable<Computer>>> navigationPath)
        {
            return _context.Set<T>().Include(navigationPath);
        }

        /// <summary>
        /// Выгрузка всех данных из контекста
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Выборка сущности из контекста по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Удаление сущности из контекста
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Удаление списка сущностей из контекста
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }


        /// <summary>
        /// Сохранение данных контекста
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}