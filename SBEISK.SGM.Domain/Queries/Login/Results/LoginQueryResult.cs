using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Queries.Login.Results
{
    public class LoginQueryResult
    {
        public string Token { get; set; }
        public bool Succeeded { get; set; }
        public string Username { get; set; }
        public IList<string> Permissions { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
