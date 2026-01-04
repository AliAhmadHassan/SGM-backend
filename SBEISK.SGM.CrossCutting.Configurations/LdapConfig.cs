namespace SBEISK.SGM.CrossCutting.Configurations
{
    public class LdapConfig
    {
        public string Url { get; set; }
        public string BindDn { get; set; }
        public string BindCredentials { get; set; }
        public string SearchBase { get; set; }
        public string SearchFilter { get; set; }
        public int Port { get; set; }
        public string UsersFilter { get; set; }

    }
}
