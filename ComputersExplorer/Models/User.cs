namespace ComputersExplorer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public List<Computer> Computers { get; set; } = new List<Computer>();
    }
}
