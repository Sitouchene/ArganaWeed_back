using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeed_Api
{
    public class LoginRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
