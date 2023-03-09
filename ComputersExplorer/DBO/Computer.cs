
using System.ComponentModel.DataAnnotations;

namespace ComputersExplorer.DBO
{
    /// <summary>
    /// Класс-сущность "Компьютер"
    /// </summary>
    public class Computer
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }
        [ConcurrencyCheck]
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
