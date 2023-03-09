using ComputersExplorer.DTO;
using ComputersExplorer.Models;
using ComputersExplorer.Repositories;

namespace ComputersExplorer.Logic
{
    public class ComputerLogicProvider
    {
        public ComputerRepository ComputerRepository;
        public ComputerLogicProvider(ComputerRepository _ComputerRepository)
        {
            this.ComputerRepository = _ComputerRepository;
        }

       
        public List<Computers> GetComputers()
        {
            return ComputerRepository.GetAll().Select(c => new Computers(c.Id, c.Name, c.UserId)).ToList();
        }

        
   

        public void AddComputer(Computer Computer)
        {
            ComputerRepository.Add(Computer);
        }

        public void DeleteComputer(Computer Computer)
        {
            ComputerRepository.Remove(Computer);
        }

        public Computer GetComputerById(int id)
        {
            return ComputerRepository.GetById(id);
        }

        public Task<int> SaveChanges()
        {
            return ComputerRepository.SaveChanges();
        }
    }
}
