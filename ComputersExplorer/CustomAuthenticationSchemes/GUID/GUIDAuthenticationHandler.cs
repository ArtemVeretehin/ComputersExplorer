using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ComputersExplorer.CustomAuthenticationSchemes.GUID
{
    /// <summary>
    /// Обработчик событий для GUID-аутентификации
    /// </summary>
    public class GUIDAuthenticationHandler : AuthenticationHandler<GUIDAuthenticationOptions>
    {
        private readonly IGUIDAuthenticationManager GUIDAuthenticationManager;
        public GUIDAuthenticationHandler(
            IOptionsMonitor<GUIDAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IGUIDAuthenticationManager _GUIDAuthenticationManager
            ) 
            : base(options, logger, encoder, clock)
        {
            GUIDAuthenticationManager = _GUIDAuthenticationManager;
        }

        /// <summary>
        /// Основной метод обработки данных пользователя  
        /// </summary>
        /// <returns></returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //Если нет заголовка Authorization
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("Header Not Found"));
            }


            string token = Request.Headers["Authorization"];

            //Если токен не отправлен
            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("token is Empty!"));
            }

            try
            {
                return validateToken(token);
            }
            catch (Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Метод валидации токена. Сопоставляет отправленный токен с ранее сохраненными: если такой токен есть - производится установка
        /// идентичности пользователя и возврат тикета, если нет - возвращается ошибка аутентификации
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private Task<AuthenticateResult> validateToken(string token)
        {
            var validatedToken = GUIDAuthenticationManager.Tokens.FirstOrDefault(t => t.Key == token);
            if (validatedToken.Key == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("validation Failed"));
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, validatedToken.Value.UserName),
                new Claim(ClaimTypes.Role, validatedToken.Value.RoleName)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name); 
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}



