using System.ComponentModel.DataAnnotations;

namespace ComputersExplorer.Models
{
    public class User
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public string UserName { get; set; }
        [ConcurrencyCheck]
        public string Password { get; set; }
        [ConcurrencyCheck]
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public List<Computer>? Computers { get; set; } = new List<Computer>();
    }
}
