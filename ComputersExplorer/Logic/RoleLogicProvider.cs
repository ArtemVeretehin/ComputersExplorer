using ComputersExplorer.DBO;
using ComputersExplorer.Repositories;


namespace ComputersExplorer.Logic
{
    /// <summary>
    /// Класс-провайдер с логикой обработки запросов по ролям пользователя
    /// </summary>
    public class RoleLogicProvider
    {
        public RoleRepository RoleRepository;
        public RoleLogicProvider(RoleRepository _RoleRepository)
        {
            this.RoleRepository = _RoleRepository;
        }

        /// <summary>
        /// Поиск роли по имени роли
        /// </summary>
        /// <param name="Rolename"></param>
        /// <returns></returns>
        public Role FindRoleByName(string Rolename)
        {
            return RoleRepository.Find(r => r.Name == Rolename).FirstOrDefault();
        }


        /// <summary>
        /// Поиск роли по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRoleById(int id)
        {
            return RoleRepository.GetById(id);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="Role"></param>
        public void DeleteRole(Role Role)
        {
            RoleRepository.Remove(Role);
        }

        /// <summary>
        /// Сохранение изменений контекста
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChanges()
        {
            return RoleRepository.SaveChanges();
        }
    }
}
