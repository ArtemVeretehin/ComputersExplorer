namespace ComputersExplorer.CustomAuthenticationSchemes.GUID
{
    /// <summary>
    /// Запись представляющая данные пользователя (имя, роль)
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="RoleName"></param>
    public record Credential(string UserName, string RoleName);
}
