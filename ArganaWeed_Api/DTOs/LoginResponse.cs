using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArganaWeedApp.Models;

namespace ArganaWeedApp
{
    public class LoginResponse:BaseResponse<User>
    {
        public int UserId { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }

        public LoginResponse()
        {
            Roles = new List<string>();
        }
    }
}
