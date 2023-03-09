namespace ComputersExplorer.DTO
{
    /// <summary>
    /// Запись с данными компьютера. Используется для возврата клиенту
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="UserId"></param>
    public record Computers(int Id, string Name, int? UserId);
}
