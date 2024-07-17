namespace ArganaWeed_Api.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string NoteTexte { get; set; }
        public DateTime? NoteDate { get; set; }
        public DateTime? NoteRappelDate { get; set; }
        public int PlantuleId { get; set; }
        public string NoteUserName { get; set; }

        // Navigation property
        public Plantule Plantule { get; set; }
    }
}
