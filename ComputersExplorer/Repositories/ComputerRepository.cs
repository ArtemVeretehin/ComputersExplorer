using ComputersExplorer.DBO;

namespace ComputersExplorer.Repositories
{
    /// <summary>
    /// Класс репозитория компьютеров
    /// </summary>
    public class ComputerRepository : MsSqlRepo<Computer>
    {
        public ComputerRepository(ComputersExplorerContext context) : base(context)
        { }
    }
}
