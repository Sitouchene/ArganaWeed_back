namespace ArganaWeed_Api.Models
{
    public class PlantuleDetail
    {
        public int PlantuleId { get; set; }
        public int VarieteId { get; set; }
        public string Slug { get; set; }
        public string PlantuleDescription { get; set; }
        public DateTime DateReception { get; set; }
        public int ProvenanceId { get; set; }
        public string Stade { get; set; }
        public string Sante { get; set; }
        public string EmplacementCode { get; set; }
        public int EmplacementId { get; set; }
        public bool Statut { get; set; }
        public string Qrbase { get; set; }
        public DateTime? SortieDate { get; set; }
        public string SortieType { get; set; }
        public string SortieObservation { get; set; }
        public string VarieteNom { get; set; }
        public string VarieteCode { get; set; }
        public string ProvenanceNom { get; set; }
    }
}
