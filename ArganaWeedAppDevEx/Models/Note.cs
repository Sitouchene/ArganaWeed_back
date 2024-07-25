using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedAppDevEx.Models
{
    public class Note:BaseModel
    {
        //public int NoteId { get; set; }
        public string NoteTexte { get; set; }
        public DateTime? NoteDate { get; set; }
        public DateTime? NoteRappelDate { get; set; }
        public int PlantuleId { get; set; }
        public string NoteUserName { get; set; }
    }
}
