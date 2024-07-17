using ArganaWeed_Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ArganaWeed_Api.Data
{
    public class ArganaWeedDbContext : DbContext
    {
        public ArganaWeedDbContext(DbContextOptions<ArganaWeedDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Variete> Varietes { get; set; }
        public DbSet<Provenance> Provenances { get; set; }
        public DbSet<Emplacement> Emplacements { get; set; }
        public DbSet<Plantule> Plantules { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<PlantuleDetail> PlantuleDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emplacement>().HasKey(e => e.EmplacementId);
            modelBuilder.Entity<Plantule>().HasKey(p => p.PlantuleId);
            modelBuilder.Entity<Note>().HasKey(n => n.NoteId);
            modelBuilder.Entity<Event>().HasKey(e => e.EventId);
            modelBuilder.Entity<Plantule>()
                .HasOne(p => p.Variete)
                .WithMany()
                .HasForeignKey(p => p.VarieteId);

            modelBuilder.Entity<Plantule>()
                .HasOne(p => p.Provenance)
                .WithMany()
                .HasForeignKey(p => p.ProvenanceId);

            modelBuilder.Entity<Plantule>()
                .HasOne(p => p.Emplacement)
                .WithMany()
                .HasForeignKey(p => p.EmplacementId);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Plantule)
                .WithMany()
                .HasForeignKey(n => n.PlantuleId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Plantule)
                .WithMany()
                .HasForeignKey(e => e.PlantuleId);

            //modelBuilder.Entity<Plantule>().ToView("vwPlantuleDetails");
            
            modelBuilder.Entity<PlantuleDetail>()
           .ToView("vwPlantuleDetails")
           .HasKey(p => p.PlantuleId);

            base.OnModelCreating(modelBuilder);
        }

        // Users

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await Users.FromSqlRaw("EXEC getAllUsers").ToListAsync();
        }

        public async Task<List<User>> SearchUsersAsync(string searchString)
        {
            return await Users.FromSqlRaw("EXEC searchUsers @p0", searchString).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var users = await Users.FromSqlRaw("EXEC getUserById @p0", userId).ToListAsync();
            return users.Count > 0 ? users[0] : null;
        }

        public async Task<string> AddUserAsync(string userName, string userEmail, string userPassword, bool isAdministrator, bool isOwner, bool isAgent, bool isViewer, bool isActive)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC addUser @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7",
                userName, userEmail, userPassword, isAdministrator, isOwner, isAgent, isViewer, isActive);
            return "Utilisateur ajouté avec succès!";
        }

        public async Task<string> UpdateUserRolesAsync(int userId, bool isAdministrator, bool isOwner, bool isAgent, bool isViewer)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC updateUserRoles @p0, @p1, @p2, @p3, @p4",
                userId, isAdministrator, isOwner, isAgent, isViewer);

            return "Mise à jour des rôles de l'utilisateur effectuée avec succès!";
        }

        public async Task<string> SuspendUserAsync(int userId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC suspendUser @p0", userId);
            return "Utilisateur suspendu avec succès!";
        }

        public async Task<string> ActivateUserAsync(int userId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC activateUser @p0", userId);
            return "Utilisateur activé avec succès!";
        }

        public async Task<string> DeleteUserByIdAsync(int userId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC deleteUserById @p0", userId);
            return "Utilisateur supprimé avec succès!";
        }

        public async Task<string> DeleteAllUsersAsync()
        {
            await Database.ExecuteSqlRawAsync("EXEC deleteAllUsers");
            return "Tous les utilisateurs ont été supprimés avec succès!";
        }

        public async Task<string> ChangePasswordAsync(int userId, string currentPassword, string newPassword, string confirmPassword, string role)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC changePassword @p0, @p1, @p2, @p3, @p4",
                userId, currentPassword, newPassword, confirmPassword, role);
            return "Le mot de passe a été modifié avec succès!";
        }

        // PROVENANCES

        public async Task<string> AddProvenanceAsync(string provenanceNom, string provenanceDescription)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC addProvenance @p0, @p1",
                provenanceNom, provenanceDescription);
            return "Provenance ajoutée avec succès!";
        }

        public async Task<string> UpdateProvenanceAsync(int provenanceId, string provenanceNom, string provenanceDescription)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC updateProvenance @p0, @p1, @p2",
                provenanceId, provenanceNom, provenanceDescription);
            return "Provenance mise à jour avec succès!";
        }

        public async Task<List<Provenance>> GetAllProvenancesAsync()
        {
            return await Provenances.FromSqlRaw("EXEC getAllProvenances").ToListAsync();
        }

        public async Task<Provenance> GetProvenanceByIdAsync(int provenanceId)
        {
            var provenances = await Provenances.FromSqlRaw("EXEC getProvenanceById @p0", provenanceId).ToListAsync();
            return provenances.Count > 0 ? provenances[0] : null;
        }

        public async Task<string> DeleteProvenanceByIdAsync(int provenanceId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC deleteProvenanceById @p0", provenanceId);
            return "Provenance supprimée avec succès!";
        }

        public async Task<List<Provenance>> SearchProvenanceAsync(string searchString)
        {
            return await Provenances.FromSqlRaw("EXEC searchProvenance @p0", searchString).ToListAsync();
        }

        // Variete
        public async Task<string> AddVarieteAsync(string varieteCode, string varieteNom, string varieteDescription, string varieteCategorie)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC addVariete @p0, @p1, @p2, @p3",
                varieteCode, varieteNom, varieteDescription, varieteCategorie);
            return "Variété ajoutée avec succès!";
        }

        public async Task<string> UpdateVarieteAsync(int varieteId, string varieteCode, string varieteNom, string varieteDescription, string varieteCategorie)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC updateVariete @p0, @p1, @p2, @p3, @p4",
                varieteId, varieteCode, varieteNom, varieteDescription, varieteCategorie);
            return "Variété mise à jour avec succès!";
        }

        public async Task<List<Variete>> GetAllVarietesAsync()
        {
            return await Varietes.FromSqlRaw("EXEC getAllVarietes").ToListAsync();
        }

        public async Task<Variete> GetVarieteByIdAsync(int varieteId)
        {
            var varietes = await Varietes.FromSqlRaw("EXEC getVarieteById @p0", varieteId).ToListAsync();
            return varietes.Count > 0 ? varietes[0] : null;
        }

        public async Task<string> DeleteVarieteByIdAsync(int varieteId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC deleteVarieteById @p0", varieteId);
            return "Variété supprimée avec succès!";
        }

        public async Task<List<Variete>> SearchVarieteAsync(string searchString)
        {
            return await Varietes.FromSqlRaw("EXEC searchVariete @p0", searchString).ToListAsync();
        }

        // PLANTULES METHODES
        // Methods for Plantules

        // PUT et POST
        public async Task<string> AddPlantuleAsync(int varieteId, string plantuleDescription, DateTime dateReception, int provenanceId, string stade, string sante, int emplacementId, string eventUserName)
        {
            try
            {
                await Database.ExecuteSqlRawAsync(
                    "EXEC AddPlantule @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7",
                    varieteId, plantuleDescription, dateReception, provenanceId, stade, sante, emplacementId, eventUserName
                );
                return "Plantule ajoutée avec succès!";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'ajout de la plantule: {ex.Message}";
            }
        }


        public async Task<string> UpdatePlantuleEmplacementAsync(int id, int emplacementId, string eventUserName)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updatePlantuleEmplacement @p0, @p1, @p2",
                id, emplacementId, eventUserName);

            return result == 1 ? "Emplacement mis à jour avec succès!" : "Erreur lors de la mise à jour de l'emplacement.";
        }


        public async Task<string> UpdatePlantuleStadeAsync(int id, string stade, string eventUserName)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updatePlantuleStade @p0, @p1, @p2",
                id, stade, eventUserName);

            return result == 1 ? "Stade mis à jour avec succès!" : "Erreur lors de la mise à jour du stade.";
        }

        public async Task<string> UpdatePlantuleSanteAsync(int id, string sante, string eventUserName)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updatePlantuleSante @p0, @p1, @p2",
                id, sante, eventUserName);

            return result == 1 ? "Santé mise à jour avec succès!" : "Erreur lors de la mise à jour de la santé.";
        }



        public async Task<string> UpdatePlantuleSortieAsync(int id, DateTime? sortieDate, string sortieType, string sortieObservation, string eventUserName)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updatePlantuleSortie @p0, @p1, @p2, @p3, @p4",
                id, sortieDate, sortieType, sortieObservation, eventUserName);

            return result == 1 ? "Sortie mise à jour avec succès!" : "Erreur lors de la mise à jour de la sortie.";
        }

        // GET
        public async Task<List<PlantuleDetail>> SearchPlantulesAsync(string searchString)
        {
            return await PlantuleDetails.FromSqlRaw("EXEC searchPlantules @p0", searchString).ToListAsync();
        }

        public async Task<List<PlantuleDetail>> GetAllPlantulesAsync()
        {
            return await PlantuleDetails.FromSqlRaw("EXEC getAllPlantules").ToListAsync();
        }

        public async Task<List<PlantuleDetail>> GetPlantulesActiveAsync()
        {
            return await PlantuleDetails.FromSqlRaw("EXEC getPlantulesActive").ToListAsync();
        }

        public async Task<List<PlantuleDetail>> GetPlantuleByVarieteAsync(string varieteCode)
        {
            return await PlantuleDetails.FromSqlRaw("EXEC getPlantuleByVariete @p0", varieteCode).ToListAsync();
        }

        public async Task<List<PlantuleDetail>> GetPlantulesInactiveAsync()
        {
            return await PlantuleDetails.FromSqlRaw("EXEC getPlantulesInactive").ToListAsync();
        }

        public async Task<PlantuleDetail> GetPlantuleBySlugAsync(string slug)
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getPlantuleBySlug @p0", slug).ToListAsync();
            return plantules.FirstOrDefault();
        }

        public async Task<PlantuleDetail> GetPlantuleByIdAsync(int id)
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getPlantuleById @p0", id).ToListAsync();
            return plantules.FirstOrDefault();
        }

        /*
        public async Task<string> UpdatePlantuleEmplacementAsync(int id, int emplacementId, string eventUserName)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updatePlantuleEmplacement @p0, @p1, @p2",
                id, emplacementId, eventUserName);

            return result == 1 ? "Emplacement mis à jour avec succès!" : "Erreur lors de la mise à jour de l'emplacement.";
        }

        public async Task<string> UpdatePlantuleStadeAsync(int id, string stade, string eventUserName)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updatePlantuleStade @p0, @p1, @p2",
                id, stade, eventUserName);

            return result == 1 ? "Stade mis à jour avec succès!" : "Erreur lors de la mise à jour du stade.";
        }

        public async Task<string> UpdatePlantuleSortieAsync(int id, DateTime? sortieDate, string sortieType, string sortieObservation, string eventUserName)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updatePlantuleSortie @p0, @p1, @p2, @p3, @p4",
                id, sortieDate, sortieType, sortieObservation, eventUserName);

            return result == 1 ? "Sortie mise à jour avec succès!" : "Erreur lors de la mise à jour de la sortie.";
        }

        */


        // NOTES
        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await Notes.FromSqlRaw("EXEC getAllNotes").ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(int noteId)
        {
            var notes = await Notes.FromSqlRaw("EXEC getNoteById @p0", noteId).ToListAsync();
            return notes.FirstOrDefault();
        }

        public async Task<List<Note>> GetNotesByPlantuleIdAsync(int plantuleId)
        {
            return await Notes.FromSqlRaw("EXEC getNotesByPlantuleId @p0", plantuleId).ToListAsync();
        }

        public async Task<List<Note>> GetNotesByDateAsync(DateTime noteDate)
        {
            return await Notes.FromSqlRaw("EXEC getNotesByDate @p0", noteDate).ToListAsync();
        }

        public async Task<List<Note>> GetNotesByUserNameAsync(string userName)
        {
            return await Notes.FromSqlRaw("EXEC getNotesByUserName @p0", userName).ToListAsync();
        }

        public async Task<string> AddNoteAsync(string noteTexte, DateTime noteDate, DateTime? noteRappelDate, int plantuleId, string noteUserName)
        {
            try
            {
                await Database.ExecuteSqlRawAsync(
                    "EXEC addNote @p0, @p1, @p2, @p3, @p4",
                    noteTexte, noteDate, noteRappelDate, plantuleId, noteUserName
                );
                return "Note ajoutée avec succès!";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'ajout de la note: {ex.Message}";
            }
        }



        // EVENTS
        // Methods for Events
        // Events methods

        public async Task<string> AddEventAsync(DateTime eventDateTime, string eventSource, string eventType, int plantuleId, string eventNature, string eventValeur, string eventUserName)
        {
            try
            {
                await Database.ExecuteSqlRawAsync(
                    "EXEC addEvent @p0, @p1, @p2, @p3, @p4, @p5, @p6",
                    eventDateTime, eventSource, eventType, plantuleId, eventNature, eventValeur, eventUserName
                );
                return "Événement ajouté avec succès!";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'ajout de l'événement: {ex.Message}";
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            var events = await Events.FromSqlRaw("EXEC getEventById @p0", eventId).ToListAsync();
            return events.FirstOrDefault();
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await Events.FromSqlRaw("EXEC getAllEvents").ToListAsync();
        }

        public async Task<List<Event>> GetEventsByPlantuleIdAsync(int plantuleId)
        {
            return await Events.FromSqlRaw("EXEC getEventsByPlantuleId @p0", plantuleId).ToListAsync();
        }

        public async Task<List<Event>> GetEventByDateAsync(DateTime eventDate)
        {
            return await Events.FromSqlRaw("EXEC getEventByDate @p0", eventDate).ToListAsync();
        }

        public async Task<List<Event>> GetEventsByUserNameAsync(string userName)
        {
            return await Events.FromSqlRaw("EXEC getEventsByUserName @p0", userName).ToListAsync();
        }

        public async Task<List<Event>> GetEventsByTypeAsync(string eventType)
        {
            return await Events.FromSqlRaw("EXEC getEventsByType @p0", eventType).ToListAsync();
        }

        public async Task<List<Event>> GetEventsByNatureAsync(string eventNature)
        {
            return await Events.FromSqlRaw("EXEC getEventsByNature @p0", eventNature).ToListAsync();
        }

        // Emplacements
        // Emplacements methods
        public async Task<string> AddEmplacementAsync(string emplacementCode, string emplacementDescription)
        {
            var success = 0;
            var message = string.Empty;
            try
            {
                await Database.ExecuteSqlRawAsync(
                    "EXEC addEmplacement @p0, @p1",
                    new SqlParameter("@p0", emplacementCode),
                    new SqlParameter("@p1", emplacementDescription ?? (object)DBNull.Value));
                success = 1;
                message = "Emplacement ajouté avec succès!";
            }
            catch (Exception ex)
            {
                success = 0;
                message = $"Échec de l'ajout de l'emplacement : {ex.Message}";
            }

            return message;
        }

        public async Task<string> UpdateEmplacementAsync(int emplacementId, string emplacementCode, string emplacementDescription)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updateEmplacement @p0, @p1, @p2",
                emplacementId, emplacementCode, emplacementDescription);

            return result == 1 ? "Emplacement mis à jour avec succès!" : "Erreur lors de la mise à jour de l'emplacement.";
        }


        public async Task<List<Emplacement>> GetAllEmplacementsAsync()
        {
            return await Emplacements.FromSqlRaw("EXEC getAllEmplacements").ToListAsync();
        }

        public async Task<Emplacement> GetEmplacementByIdAsync(int emplacementId)
        {
            var emplacements = await Emplacements.FromSqlRaw("EXEC getEmplacementById @p0", emplacementId).ToListAsync();
            return emplacements.FirstOrDefault();
        }

        public async Task<string> DeleteEmplacementByIdAsync(int emplacementId)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC deleteEmplacementById @p0",
                emplacementId);

            return result == 1 ? "Emplacement supprimé avec succès!" : "Erreur lors de la suppression de l'emplacement.";
        }


        public async Task<List<Emplacement>> SearchEmplacementAsync(string searchString)
        {
            return await Emplacements.FromSqlRaw("EXEC searchEmplacement @p0", searchString).ToListAsync();
        }



    }
    
}
