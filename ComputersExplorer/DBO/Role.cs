namespace ComputersExplorer.DBO
{
    /// <summary>
    /// Класс-сущность "Роль пользователя"
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new();
    }
}
