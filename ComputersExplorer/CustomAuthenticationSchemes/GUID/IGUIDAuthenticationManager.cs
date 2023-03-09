namespace ComputersExplorer.CustomAuthenticationSchemes.GUID
{
    /// <summary>
    /// Интерфейс для менеджера GUID-аутентификации.
    /// </summary>
    public interface IGUIDAuthenticationManager
    {
        /// <summary>
        /// Метод для генерации токена
        /// </summary>
        /// <param name="UserRoleName"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        string Authenticate(string UserRoleName, string username);
        
        
        /// <summary>
        /// Словарь "Токен,Данные пользователя"
        /// </summary>
        IDictionary<string, Credential> Tokens { get; }
    }
}
