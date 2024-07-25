using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedAppDevEx.Models
{
    public class Variete:BaseModel
    {
        public int VarieteId { get; set; }
        public string VarieteCode { get; set; }
        public string VarieteNom { get; set; }
        public string VarieteDescription { get; set; }
        public string VarieteCategorie { get; set; }
    }
}
