using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMg.BLL.DTOs
{
    public class AuthResult
    {
        public string Token { get; set; }
       // public string RefreshToken { get; set; }
        public bool Result { get; set; }
        public List<String> Errors { get; set; }
    }
}
