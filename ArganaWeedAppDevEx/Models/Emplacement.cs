using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedAppDevEx.Models
{
    public class Emplacement : BaseModel
    {
        //public int EmplacementId { get; set; }
        public string EmplacementCode { get; set; }
        public string EmplacementDescription { get; set; }
    }
}
