namespace ComputersExplorer.Repositories
{
    interface IContextSaveChanges
    {
        Task<int> SaveChanges();
    }
}
