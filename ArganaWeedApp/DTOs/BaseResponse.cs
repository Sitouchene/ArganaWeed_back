using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedApp
{
    public abstract class BaseResponse <T>
    {
        public bool Success { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        //ajout pour factorisation
        public List<T> Items { get; set; }


        protected BaseResponse()
        {
            this.Success = true;
            this.Message = string.Empty;
            this.Items = new List<T>();
        }
    }
}
