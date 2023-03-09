using ComputersExplorer.DBO;

namespace ComputersExplorer.Repositories
{
    /// <summary>
    /// Класс репозитория пользователей
    /// </summary>
    public class UserRepository : MsSqlRepo<User>
    {
        public UserRepository(ComputersExplorerContext context) : base(context)
        { }
    }
}
