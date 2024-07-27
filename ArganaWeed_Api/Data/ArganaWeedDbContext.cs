using ArganaWeedApp.Models;
using ArganaWeedApp.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArganaWeedApp.Data
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
        // Dashboard
        public DbSet<PlantulesStats> PlantulesStats { get; set; }
        public DbSet<PlantulesParCategorie> PlantulesParCategorie { get; set; }
        public DbSet<PlantulesParStade> PlantulesParStade { get; set; }
        public DbSet<PlantulesParSante> PlantulesParSante { get; set; }
        public DbSet<EvolutionMensuellePlantules> EvolutionMensuellePlantules { get; set; }
        //Authentification
        public DbSet<AuthResult> AuthResults { get; set; }

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

            modelBuilder.Entity<PlantuleDetail>()
                .ToView("vwPlantuleDetails")
                .HasKey(p => p.PlantuleId);

            modelBuilder.Entity<PlantulesStats>()
                .ToView("vwPlantulesStats")
                .HasNoKey();

            modelBuilder.Entity<PlantulesParCategorie>()
                .ToView("vwPlantulesParCategorie")
                .HasNoKey();

            modelBuilder.Entity<PlantulesParStade>()
                .ToView("vwPlantulesParStade")
                .HasNoKey();

            modelBuilder.Entity<PlantulesParSante>()
                .ToView("vwPlantulesParSante")
                .HasNoKey();

            modelBuilder.Entity<EvolutionMensuellePlantules>()
                .ToView("vwEvolutionMensuellePlantules")
                .HasNoKey();

            modelBuilder.Entity<AuthResult>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }

        #region Users
        public async Task<UsersResponse> GetAllUsersAsync()
        {
            var users = await Users.FromSqlRaw("EXEC getAllUsers").ToListAsync();
            return new UsersResponse
            {
                Items = users
            };
        }

        public async Task<UsersResponse> GetUserByIdAsync(int userId)
        {
            var users = await Users.FromSqlRaw("EXEC getUserById @p0", userId).ToListAsync();
            return new UsersResponse
            {
                Items = users
            };
        }

        public async Task<UsersResponse> SearchUsersAsync(string searchString)
        {
            var users = await Users.FromSqlRaw("EXEC searchUsers @p0", searchString).ToListAsync();
            return new UsersResponse
            {
                Items = users
            };
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
        #endregion

        #region Provenances
        public async Task<ProvenancesResponse> GetAllProvenancesAsync()
        {
            var provenances = await Provenances.FromSqlRaw("EXEC getAllProvenances").ToListAsync();
            return new ProvenancesResponse
            {
                Items = provenances
            };
        }



        public async Task<ProvenancesResponse> GetProvenanceByIdAsync(int provenanceId)
        {
            var provenances = await Provenances.FromSqlRaw("EXEC getProvenanceById @p0", provenanceId).ToListAsync();
            return new ProvenancesResponse
            {
                Items = provenances
            };
        }

        public async Task<ProvenancesResponse> SearchProvenanceAsync(string searchString)
        {
            var provenances = await Provenances.FromSqlRaw("EXEC searchProvenance @p0", searchString).ToListAsync();
            return new ProvenancesResponse
            {
                Items = provenances
            };
        }

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

        public async Task<string> DeleteProvenanceByIdAsync(int provenanceId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC deleteProvenanceById @p0", provenanceId);
            return "Provenance supprimée avec succès!";
        }
        #endregion

        #region Emplacements
        public async Task<EmplacementsResponse> GetAllEmplacementsAsync()
        {
            var emplacements = await Emplacements.FromSqlRaw("EXEC getAllEmplacements").ToListAsync();
            return new EmplacementsResponse
            {
                Items = emplacements
            };
        }

        public async Task<EmplacementsResponse> GetEmplacementByIdAsync(int emplacementId)
        {
            var emplacements = await Emplacements.FromSqlRaw("EXEC getEmplacementById @p0", emplacementId).ToListAsync();
            return new EmplacementsResponse
            {
                Items = emplacements
            };
        }

        public async Task<EmplacementsResponse> SearchEmplacementAsync(string searchString)
        {
            var emplacements = await Emplacements.FromSqlRaw("EXEC searchEmplacement @p0", searchString).ToListAsync();
            return new EmplacementsResponse
            {
                Items = emplacements
            };
        }

        public async Task<string> AddEmplacementAsync(string emplacementCode, string emplacementDescription)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC addEmplacement @p0, @p1",
                new SqlParameter("@p0", emplacementCode),
                new SqlParameter("@p1", emplacementDescription ?? (object)DBNull.Value));
            return "Emplacement ajouté avec succès!";
        }

        public async Task<string> UpdateEmplacementAsync(int emplacementId, string emplacementCode, string emplacementDescription)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC updateEmplacement @p0, @p1, @p2",
                emplacementId, emplacementCode, emplacementDescription);

            return result == 1 ? "Emplacement mis à jour avec succès!" : "Erreur lors de la mise à jour de l'emplacement.";
        }

        public async Task<string> DeleteEmplacementByIdAsync(int emplacementId)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC deleteEmplacementById @p0", emplacementId);

            return result == 1 ? "Emplacement supprimé avec succès!" : "Erreur lors de la suppression de l'emplacement.";
        }
        #endregion

        #region Plantules
        public async Task<PlantulesDetailResponse> GetAllPlantulesAsync()
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getAllPlantules").ToListAsync();
            return new PlantulesDetailResponse
            {
                Items = plantules
            };
        }

        public async Task<PlantulesDetailResponse> GetPlantuleByIdAsync(int plantuleId)
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getPlantuleById @p0", plantuleId).ToListAsync();
            return new PlantulesDetailResponse
            {
                Items = plantules
            };
        }

        public async Task<PlantulesDetailResponse> SearchPlantulesAsync(string searchString)
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC searchPlantules @p0", searchString).ToListAsync();
            return new PlantulesDetailResponse
            {
                Items = plantules
            };
        }

        public async Task<PlantulesDetailResponse> GetPlantuleBySlugAsync(string slug)
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getPlantuleBySlug @p0", slug).ToListAsync();
            return new PlantulesDetailResponse
            {
                Items = plantules
            };
        }

        public async Task<PlantulesDetailResponse> GetPlantuleByVarieteAsync(string varieteCode)
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getPlantuleByVariete @p0", varieteCode).ToListAsync();
            return new PlantulesDetailResponse
            {
                Items = plantules
            };
        }

        public async Task<PlantulesDetailResponse> GetPlantulesActiveAsync()
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getPlantulesActive").ToListAsync();
            return new PlantulesDetailResponse
            {
                Items = plantules
            };
        }

        public async Task<PlantulesDetailResponse> GetPlantulesInactiveAsync()
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getPlantulesInactive").ToListAsync();
            return new PlantulesDetailResponse
            {
                Items = plantules
            };
        }

        public async Task<PlantulesDetailResponse> GetPlantulesArchivedAsync()
        {
            var plantules = await PlantuleDetails.FromSqlRaw("EXEC getPlantulesArchived").ToListAsync();
            return new PlantulesDetailResponse
            {
                Items = plantules
            };
        }

        public async Task<string> AddPlantuleAsync(int varieteId, string plantuleDescription, DateTime dateReception, int provenanceId, string stade, string sante, int emplacementId, string eventUserName)
        {
            try
            {
                await Database.ExecuteSqlRawAsync(
                    "EXEC AddPlantule @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7",
                    varieteId, plantuleDescription, dateReception, provenanceId, stade, sante, emplacementId, eventUserName);
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

        public async Task<string> ArchivePlantulesAsync(DateTime endDate, string eventUserName)
        {
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC archivePlantules @p0", endDate);

            return result > 0 ? "Plantules archivées avec succès!" : "Aucune plantule à archiver ou erreur lors de l'archivage.";
        }
        #endregion

        #region Notes
        //************ GET
        
        public async Task<NotesResponse> GetAllNotesAsync()
        {
            var notes = await Notes.FromSqlRaw("EXEC getAllNotes").ToListAsync();
            return new NotesResponse
            {
                Items = notes
            };
        }

        public async Task<NotesResponse> GetNoteByIdAsync(int noteId)
        {
            var notes = await Notes.FromSqlRaw("EXEC getNoteById @p0", noteId).ToListAsync();
            return new NotesResponse
            {
                Items = notes
            };
        }

        public async Task<NotesResponse> GetNotesByPlantuleIdAsync(int plantuleId)
        {
            var notes = await Notes.FromSqlRaw("EXEC getNotesByPlantuleId @p0", plantuleId).ToListAsync();
            return new NotesResponse
            {
                Items = notes
            };
        }

        public async Task<NotesResponse> GetNotesByDateAsync(DateTime noteDate)
        {
            var notes = await Notes.FromSqlRaw("EXEC getNotesByDate @p0", noteDate).ToListAsync();
            return new NotesResponse
            {
                Items = notes
            };
        }

        public async Task<NotesResponse> GetNotesByUserNameAsync(string userName)
        {
            var notes = await Notes.FromSqlRaw("EXEC getNotesByUserName @p0", userName).ToListAsync();
            return new NotesResponse
            {
                Items = notes
            };
        }

        //************** POST/UPDATE/DELETE

        public async Task<string> AddNoteAsync(string noteTexte, DateTime noteDate, DateTime? noteRappelDate, int plantuleId, string noteUserName)
        {
            try
            {
                await Database.ExecuteSqlRawAsync(
                    "EXEC addNote @p0, @p1, @p2, @p3, @p4",
                    noteTexte, noteDate, noteRappelDate, plantuleId, noteUserName);
                return "Note ajoutée avec succès!";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'ajout de la note: {ex.Message}";
            }
        }
        #endregion

        #region Events
        public async Task<EventsResponse> GetAllEventsAsync()
        {
            var events = await Events.FromSqlRaw("EXEC getAllEvents").ToListAsync();
            return new EventsResponse
            {
                Items = events
            };
        }

        public async Task<EventsResponse> GetEventByIdAsync(int eventId)
        {
            var events = await Events.FromSqlRaw("EXEC getEventById @p0", eventId).ToListAsync();
            return new EventsResponse
            {
                Items = events
            };
        }

        public async Task<EventsResponse> GetEventsByPlantuleIdAsync(int plantuleId)
        {
            var events = await Events.FromSqlRaw("EXEC getEventsByPlantuleId @p0", plantuleId).ToListAsync();
            return new EventsResponse
            {
                Items = events
            };
        }

        public async Task<EventsResponse> GetEventByDateAsync(DateTime eventDate)
        {
            var events = await Events.FromSqlRaw("EXEC getEventByDate @p0", eventDate).ToListAsync();
            return new EventsResponse
            {
                Items = events
            };
        }

        public async Task<EventsResponse> GetEventsByUserNameAsync(string userName)
        {
            var events = await Events.FromSqlRaw("EXEC getEventsByUserName @p0", userName).ToListAsync();
            return new EventsResponse
            {
                Items = events
            };
        }

        public async Task<EventsResponse> GetEventsByTypeAsync(string eventType)
        {
            var events = await Events.FromSqlRaw("EXEC getEventsByType @p0", eventType).ToListAsync();
            return new EventsResponse
            {
                Items = events
            };
        }

        public async Task<EventsResponse> GetEventsByNatureAsync(string eventNature)
        {
            var events = await Events.FromSqlRaw("EXEC getEventsByNature @p0", eventNature).ToListAsync();
            return new EventsResponse
            {
                Items = events
            };
        }

        public async Task<string> AddEventAsync(DateTime eventDateTime, string eventSource, string eventType, int plantuleId, string eventNature, string eventValeur, string eventUserName)
        {
            try
            {
                await Database.ExecuteSqlRawAsync(
                    "EXEC addEvent @p0, @p1, @p2, @p3, @p4, @p5, @p6",
                    eventDateTime, eventSource, eventType, plantuleId, eventNature, eventValeur, eventUserName);
                return "Événement ajouté avec succès!";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'ajout de l'événement: {ex.Message}";
            }
        }
        #endregion

        #region Varietes

        public async Task<VarietesResponse> GetAllVarietesAsync()
        {
            var varietes = await Varietes.FromSqlRaw("EXEC getAllVarietes").ToListAsync();
            return new VarietesResponse
            {
                Items = varietes
            };
        }

        public async Task<VarietesResponse> GetVarieteByIdAsync(int varieteId)
        {
            var varietes = await Varietes.FromSqlRaw("EXEC getVarieteById @p0", varieteId).ToListAsync();
            return new VarietesResponse
            {
                Items = varietes
            };
        }

        public async Task<VarietesResponse> SearchVarieteAsync(string searchString)
        {
            var varietes = await Varietes.FromSqlRaw("EXEC searchVariete @p0", searchString).ToListAsync();
            return new VarietesResponse
            {
                Items = varietes
            };
        }




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

        public async Task<string> DeleteVarieteByIdAsync(int varieteId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC deleteVarieteById @p0", varieteId);
            return "Variété supprimée avec succès!";
        }
        #endregion
    }
}
