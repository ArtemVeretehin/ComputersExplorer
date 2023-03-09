using ComputersExplorer.DTO;
using ComputersExplorer.DBO;
using ComputersExplorer.Repositories;
using System.Linq.Expressions;

namespace ComputersExplorer.Logic
{
    /// <summary>
    /// Класс-провайдер с логикой обработки запросов по пользователям
    /// </summary>
    public class UserLogicProvider
    {
        public UserRepository UserRepository;
        public UserLogicProvider(UserRepository _UserRepository)
        {
            this.UserRepository = _UserRepository;
        }

        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        /// <returns></returns>
        public List<Users> GetUsers()
        {
            return UserRepository.GetAll().Select(u => new Users(u.Id, u.UserName, u.Password, u.RoleId)).ToList();
        }

        /// <summary>
        /// Получение всех пользователей с подгрузкой связанной сущности
        /// </summary>
        /// <param name="navigationPath"></param>
        /// <returns></returns>
        public List<User> GetUsersWithInclude(Expression<Func<User,IEnumerable<Computer>>> navigationPath)
        {
            return UserRepository.GetAllWithInclude(navigationPath).ToList();
        }

        /// <summary>
        /// Проверка наличия пользователя с определенным именем / именем + паролем
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isUserWithThisCredentialsExist(string username, string password = null)
        {
            if (password is null) return UserRepository.Find(u => u.UserName == username).Count() > 0;
            return UserRepository.Find(u => u.UserName == username && u.Password == password).Count() > 0;
        }

        /// <summary>
        /// Получение пользователя по имени пользователя
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByName(string username)
        {
            return UserRepository.Find(u => u.UserName == username).FirstOrDefault();
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            return UserRepository.GetById(id);
        }

        /// <summary>
        /// Получение Id роли для пользователя с определенным именем
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetUserRoleIdByName(string username)
        {
            var user = UserRepository.FindWithInclude(u => u.UserName == username,u => u.Role).FirstOrDefault();
            return user is not null ? user.Role.Id : -1;
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            UserRepository.Remove(user);
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            UserRepository.Add(user);
        }

        /// <summary>
        /// Сохранение изменений контекста
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChanges()
        {
            return UserRepository.SaveChanges();
        }
    }
}
