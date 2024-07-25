using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedApi
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }

        protected BaseResponse()
        {
            this.Success = true;
            this.Message = string.Empty;
        }
    }
}
