using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedApp.DTOs
{
    public class EmplacementRequest : BaseRequest
    {
        public string EmplacementCode { get; set; }
        public string EmplacementDescription { get; set; }
    }
}
