namespace ArganaWeedRest.Models
{
    public class PlantulesStats
    {
        public int TotalPlantules { get; set; }
        public int TotalPlantulesActive { get; set; }
        public int TotalPlantulesToArchive { get; set; }
        public int TotalPlantulesArchived { get; set; }
    }

    public class PlantulesParCategorie
    {
        public string Categorie { get; set; }
        public int NombreParCategorie { get; set; }
    }

    public class PlantulesParStade
    {
        public string Stade { get; set; }
        public int NombreParStade { get; set; }
    }

    public class PlantulesParSante
    {
        public string Sante { get; set; }
        public int NombreParSante { get; set; }
    }

    public class EvolutionMensuellePlantules
    {
        public int Annee { get; set; }
        public int Mois { get; set; }
        public int PlantulesRecus { get; set; }
        public int PlantulesSortis { get; set; }
        public int FluxNet { get; set; }
        public int StockActif { get; set; }
    }
}

