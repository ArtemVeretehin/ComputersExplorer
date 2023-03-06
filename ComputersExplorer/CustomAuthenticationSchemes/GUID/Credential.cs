namespace ComputersExplorer.CustomAuthenticationSchemes.GUID
{
    public class Credential
    {
        public Credential(string UserName, string RoleName)
        {
            this.UserName = UserName;
            this.RoleName = RoleName;
        }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
