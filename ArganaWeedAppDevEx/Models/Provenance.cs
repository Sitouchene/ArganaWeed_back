using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedAppDevEx.Models
{
    public class Provenance:BaseModel
    {
        //public int ProvenanceId { get; set; }
        public string ProvenanceNom { get; set; }
        public string ProvenanceDescription { get; set; }
    }
}