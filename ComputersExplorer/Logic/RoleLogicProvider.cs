using ComputersExplorer.DTO;
using ComputersExplorer.Models;
using ComputersExplorer.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ComputersExplorer.Logic
{
    public class RoleLogicProvider
    {
        public RoleRepository RoleRepository;
        public RoleLogicProvider(RoleRepository _RoleRepository)
        {
            this.RoleRepository = _RoleRepository;
        }

        public Role FindRoleByName(string Rolename)
        {
            return RoleRepository.Find(r => r.Name == Rolename).FirstOrDefault();
        }


        public Role GetRoleById(int id)
        {
            return RoleRepository.GetById(id);
        }

        public void DeleteRole(Role Role)
        {
            RoleRepository.Remove(Role);
        }

        public Task<int> SaveChanges()
        {
            return RoleRepository.SaveChanges();
        }
    }
}
