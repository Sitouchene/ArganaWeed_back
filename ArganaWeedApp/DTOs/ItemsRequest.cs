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

    public class ProvenanceRequest : BaseRequest
    {
        public string ProvenanceNom { get; set; }
        public string ProvenanceDescription { get; set; }
    }

    public class VarieteRequest : BaseRequest
    {
        public string VarieteCode { get; set; }
        public string VarieteNom { get; set; }
        public string VarieteCategorie { get; set; }
        public string VarieteDescription { get; set; }
    }


    public class UserRequest : BaseRequest
    {
        public string  UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsViewer { get; set; }

    }


    public class PlantuleAddRequest
    {
        public int VarieteId { get; set; }
        public string PlantuleDescription { get; set; }
        public DateTime DateReception { get; set; }
        public int ProvenanceId { get; set; }
        public string Stade { get; set; }
        public string Sante { get; set; }
        public int EmplacementId { get; set; }
    }

    

    public class UpdateSortieRequest
    {
        public DateTime? SortieDate { get; set; }
        public string SortieType { get; set; }
        public string SortieObservation { get; set; }
    }

    public class UpdateStadeRequest
    {
        public string Stade { get; set; }
    }

    public class UpdateSanteRequest
    {
        public string Sante { get; set; }
    }

    public class ArchivePlantulesRequest
    {
        public DateTime EndDate { get; set; }
    }

    public class UpdateEmplacementRequest
    {
        public int EmplacementId { get; set; }
    }


    public class NoteRequest
    {
        public string NoteTexte { get; set; }
        public DateTime? NoteDate { get; set; }
        public DateTime? NoteRappelDate { get; set; }
        public int PlantuleId { get; set; }
    }






}
