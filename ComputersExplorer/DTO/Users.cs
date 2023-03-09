namespace ComputersExplorer.DTO
{
    /// <summary>
    /// Запись с данными пользователя. Используется для возврата клиенту
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    /// <param name="RoleId"></param>
    public record Users(int Id, string UserName, string Password, int? RoleId);
}
