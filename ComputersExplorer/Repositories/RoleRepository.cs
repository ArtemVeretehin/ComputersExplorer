using ComputersExplorer.DBO;

namespace ComputersExplorer.Repositories
{
    /// <summary>
    /// Класс репозитория ролей
    /// </summary>
    public class RoleRepository : MsSqlRepo<Role>
    {
        public RoleRepository(ComputersExplorerContext context) : base(context)
        { }
    }
}
