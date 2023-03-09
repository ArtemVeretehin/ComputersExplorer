namespace ComputersExplorer.CustomAuthenticationSchemes.GUID
{

    public class GUIDAuthenticationManager : IGUIDAuthenticationManager
    {
        private readonly IDictionary<string, Credential> tokens = new Dictionary<string, Credential>();

        public IDictionary<string, Credential> Tokens => tokens;

        public string Authenticate(string UserRoleName, string username)
        {
            var token = Guid.NewGuid().ToString();
            tokens.Add(token, new Credential(username, UserRoleName));

            return token;
        }
    }
}