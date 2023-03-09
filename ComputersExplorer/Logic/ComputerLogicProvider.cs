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
            return ComputerRepository.GetAll().Select(u => new Computers(u.Id, u.ComputerName, u.Password, u.RoleId)).ToList();
        }

        public bool isComputerWithThisCredentialsExist(string Computername, string password = null)
        {
            if (password is null) return ComputerRepository.Find(u => u.ComputerName == Computername).Count() > 0;
            return ComputerRepository.Find(u => u.ComputerName == Computername && u.Password == password).Count() > 0;
        }


        public Computer GetComputerById(int id)
        {
            return ComputerRepository.GetById(id);
        }

        public int GetComputerRoleIdByName(string Computername)
        {
            var Computer = ComputerRepository.FindWithInclude(u => u.ComputerName == Computername,u => u.Role).FirstOrDefault();
            return Computer is not null ? Computer.Role.Id : -1;
        }

        public void DeleteComputer(Computer Computer)
        {
            ComputerRepository.Remove(Computer);
        }

        public void AddComputer(Computer Computer)
        {
            ComputerRepository.Add(Computer);
        }

        public Task<int> SaveChanges()
        {
            return ComputerRepository.SaveChanges();
        }
    }
}
