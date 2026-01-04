namespace SBEISK.SGM.CrossCutting.Configurations
{
    public class JwtTokenConfiguration
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
