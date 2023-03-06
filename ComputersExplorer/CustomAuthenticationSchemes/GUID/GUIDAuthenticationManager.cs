using ComputersExplorer;
using ComputersExplorer.Models;
using Microsoft.EntityFrameworkCore;
namespace ComputersExplorer.CustomAuthenticationSchemes.GUID
{

    public class GUIDAuthenticationManager : IGUIDAuthenticationManager
    {
        private readonly IDictionary<string, Credential> tokens = new Dictionary<string, Credential>();

        public IDictionary<string, Credential> Tokens => tokens;

        public string Authenticate(string username, string password, ComputersExplorerContext context)
        {
            if (!context.Users.Any(u => u.UserName == username && u.Password == password))
            {
                return null;
            }

            var token = Guid.NewGuid().ToString();
            
            var RoleName = context.Roles.FirstOrDefault(r => r.Id == context.Users.FirstOrDefault(u => u.UserName == username).Role.Id).Name;

            tokens.Add(token, new Credential(username, RoleName));

            return token;
        }
    }
}