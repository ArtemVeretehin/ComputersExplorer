using ComputersExplorer.DTO;
using ComputersExplorer.Models;
using ComputersExplorer.Repositories;

namespace ComputersExplorer.Logic
{
    public class UserLogicProvider
    {
        public UserRepository UserRepository;
        public UserLogicProvider(UserRepository _UserRepository)
        {
            this.UserRepository = _UserRepository;
        }

        public List<Users> GetUsers()
        {
            return UserRepository.GetAll().Select(u => new Users(u.Id, u.UserName, u.Password, u.RoleId)).ToList();
        }

        public bool isUserWithThisCredentialsExist(string username, string password = null)
        {
            if (password is null) return UserRepository.Find(u => u.UserName == username).Count() > 0;
            return UserRepository.Find(u => u.UserName == username && u.Password == password).Count() > 0;
        }


        public User GetUserById(int id)
        {
            return UserRepository.GetById(id);
        }

        public int GetUserRoleIdByName(string username)
        {
            var user = UserRepository.FindWithInclude(u => u.UserName == username,u => u.Role).FirstOrDefault();
            return user is not null ? user.Role.Id : -1;
        }

        public void DeleteUser(User user)
        {
            UserRepository.Remove(user);
        }

        public void AddUser(User user)
        {
            UserRepository.Add(user);
        }

        public Task<int> SaveChanges()
        {
            return UserRepository.SaveChanges();
        }
    }
}
