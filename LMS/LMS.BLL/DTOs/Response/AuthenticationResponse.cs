using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.DTOs.Response
{
    public class AuthenticationResponse
    {
       // public JwtToken JwtToken { get; set; }
        public string JwtToken { get; set; }
        public string UserId { get; set; }
        public string RefreshToken { get; set; }
        public bool Result { get; set; }
        public string FullName { get; set; }
   
    }

    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Issued { get; set; }
        public DateTime? Expires { get; set; }
    }
}
