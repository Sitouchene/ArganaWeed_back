using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedAppDevEx.Models
{
    public class User:BaseModel
    {
        //public int UserId { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string UserEmail { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsViewer { get; set; }
        public bool IsActive { get; set; }
    }
}