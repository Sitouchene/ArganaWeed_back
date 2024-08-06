using System.Globalization;

namespace ArganaWeedApp.Models
{
    
    #region DashbordModel

    public class PlantulesStats
    {
        public int TotalPlantules { get; set; }
        public int TotalPlantulesActive { get; set; }
        public int TotalPlantulesArchived { get; set; }
        public int Capacite {  get; set; }
        
        //public int TotalPlantulesToArchive { get; set; }
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

    #endregion

    #region ParametrageModels

    public class Emplacement
    {
        public int EmplacementId { get; set; }
        public string EmplacementCode { get; set; }
        public string EmplacementDescription { get; set; }

        public override string ToString()
        {
            return $"{EmplacementId}  {EmplacementCode}  {EmplacementDescription}";
        }
    }

    public class Variete
    {
        public int VarieteId { get; set; }
        public string VarieteCode { get; set; }
        public string VarieteNom { get; set; }
        public string VarieteDescription { get; set; }
        public string VarieteCategorie { get; set; }

        public override string ToString()
        {
            return $"{VarieteId}  {VarieteCode}  {VarieteNom}   {VarieteCategorie}";
        }
    }

    public class Provenance
    {
        public int ProvenanceId { get; set; }
        public string ProvenanceNom { get; set; }
        public string ProvenanceDescription { get; set; }

        public override string ToString()
        {
            return $"{ProvenanceId}  {ProvenanceNom}  {ProvenanceDescription}";
        }
    }






    #endregion

    #region ExploitationModels

    public class Plantule
    {
        public int PlantuleId { get; set; }
        public int VarieteId { get; set; }
        public string Slug { get; set; }
        public string PlantuleDescription { get; set; }
        public DateTime DateReception { get; set; }
        public int ProvenanceId { get; set; }
        public string Stade { get; set; }
        public string Sante { get; set; }
        public int EmplacementId { get; set; }
        public bool Statut { get; set; }
        public bool IsArchived { get; set; }
        public string Qrbase { get; set; }
        public DateTime? SortieDate { get; set; }
        public string SortieType { get; set; }
        public string SortieObservation { get; set; }

        // Navigation properties
        public Variete Variete { get; set; }
        public Provenance Provenance { get; set; }
        public Emplacement Emplacement { get; set; }
    }

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
        public bool IsArchived { get; set; }
        public string Qrbase { get; set; }
        public DateTime? SortieDate { get; set; }
        public string SortieType { get; set; }
        public string SortieObservation { get; set; }
        public string VarieteNom { get; set; }
        public string VarieteCode { get; set; }
        public string ProvenanceNom { get; set; }
        public string DateReceptionFormatted => DateReception.ToString("d", CultureInfo.CurrentCulture);
        public Color BackgroundColor => Statut ? Colors.Transparent : Colors.Gray;
        public Color SanteColor
        {
            get
            {
                return Sante switch
                {
                    "Bon" => Colors.Green,
                    "Moyen" => Colors.Yellow,
                    "Mauvais" => Colors.Orange,
                    "En danger" => Colors.Red,
                    _ => Colors.Gray
                };
            }
        }



        public override string ToString()
        {
            return $"{PlantuleId}  {Slug}  {VarieteNom}   {DateReception}   {Stade}   {Sante}   {Statut}";
        }
    }

    public class Note
    {
        public int NoteId { get; set; }
        public string NoteTexte { get; set; }
        public DateTime? NoteDate { get; set; }
        public DateTime? NoteRappelDate { get; set; }
        public int PlantuleId { get; set; }
        public string NoteUserName { get; set; }

        public string NoteDateFormatted => NoteDate?.ToString("d", CultureInfo.CurrentCulture) ?? string.Empty;
        public string NoteRappelDateFormatted => NoteRappelDate?.ToString("d", CultureInfo.CurrentCulture) ?? string.Empty;

        // Navigation property
        public Plantule Plantule { get; set; }

        public override string ToString()
        {
            return $"{PlantuleId}  {NoteTexte}  {NoteDate}   {NoteUserName} ";
        }

    }

    public class Event
    {
        public int EventId { get; set; }
        public DateTime EventDatetime { get; set; }
        public string EventSource { get; set; }
        public string EventType { get; set; }
        public int PlantuleId { get; set; }
        public string EventNature { get; set; }
        public string EventValeur { get; set; }
        public string EventUserName { get; set; }
        public string EventDatetimeFormatted => EventDatetime.ToString("d", CultureInfo.CurrentCulture);

        // Navigation property
        public Plantule Plantule { get; set; }

        public override string ToString()
        {
            return $"{PlantuleId}  {EventDatetime}  {EventType}   {EventNature} {EventValeur}     {EventUserName}";
        }
    }

    #endregion

    #region AdminModels
    
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        //public string Salt { get; set; }

        public string UserEmail { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsViewer { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"{UserId}  {UserName}  {UserEmail}   Admin:{IsAdministrator} Owner:{IsOwner} Agent:{IsAgent} Viewer:{IsViewer} Active: {IsActive}";
        }
    }

    /*Code pour tests*/
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> UserRoles { get; set; }
    }

    public class UserRepository
    {
        private List<User> users = new List<User>
    {
        new User { UserId = 9, UserName = "salim", HashedPassword = "123Soleil", UserEmail = "sitouchene@gmail.com", IsAdministrator = true, IsOwner = true, IsAgent = true, IsViewer = true, IsActive = true },
        new User { UserId = 13, UserName = "harold", HashedPassword = "123Soleil", UserEmail = "horold@catb.ca", IsAdministrator = true, IsOwner = false, IsAgent = false, IsViewer = true, IsActive = true },
        new User { UserId = 11, UserName = "khadidja", HashedPassword = "123Soleil", UserEmail = "khadidja@catb.ca", IsAdministrator = false, IsOwner = false, IsAgent = true, IsViewer = true, IsActive = true },
        new User { UserId = 12, UserName = "alexandre", HashedPassword = "123Soleil", UserEmail = "alexandre@catb.ca", IsAdministrator = false, IsOwner = false, IsAgent = false, IsViewer = true, IsActive = true },
        new User { UserId = 11, UserName = "isidore", HashedPassword = "123Soleil", UserEmail = "imanzala@gmail.com", IsAdministrator = false, IsOwner = true, IsAgent = true, IsViewer = true, IsActive = false }
    };

        public async Task<AuthenticationResult> AuthenticateUserAsync(string usernameOrEmail, string password)
        {
            var user = users.FirstOrDefault(u => u.UserName == usernameOrEmail || u.UserEmail == usernameOrEmail);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = "Échec de l''authentification : utilisateur non trouvé.",
                    UserRoles = new List<string>()
                };
            }

            if (user.HashedPassword != password)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = "Échec de l''authentification : mot de passe incorrect.",
                    UserRoles = new List<string>()
                };
            }

            if (!user.IsActive)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = "Échec de l''authentification : le compte est suspendu.",
                    UserRoles = new List<string>()
                };
            }

            var roles = new List<string>();
            if (user.IsAdministrator) roles.Add("Administrator");
            if (user.IsOwner) roles.Add("Owner");
            if (user.IsAgent) roles.Add("Agent");
            if (user.IsViewer) roles.Add("Viewer");

            return new AuthenticationResult
            {
                Success = true,
                Message = "Authentification réussie!",
                UserRoles = roles
            };
        }
    }
    /*Fin du code pour tests*/


    public class UserRoleUpdateModel
    {
        public bool IsAdministrator { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsViewer { get; set; }
    }

    #endregion

    #region AuthenticationModels

    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public string CurrentUser { get; set; }
        public int? UserId { get; set; }
    }


    public class TokenRequest
    {
        public string Token { get; set; }
    }

    public class AuthResult
    {
        public bool Authenticated { get; set; }
        public string Message { get; set; }
        public string CurrentUser { get; set; }
        public int? UserId { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsViewer { get; set; }
    }

    public class JwtTokenInfo
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }


    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }

    #endregion

    #region LaboInfos
    public class LaboInfo
    {
        public int CapaciteLabo { get; set; }
        public int CapaciteLicence { get; set; }
        public string NomLabo { get; set; }
        public string AdresseL1 { get; set; }
        public string AdresseL2 { get; set; }
        public string Email { get; set; }
        public string Representant { get; set; }
        public string RepresentantEmail { get; set; }
        public string Contact1 { get; set; }
        public string Contact1Email { get; set; }
    }
    #endregion

}
