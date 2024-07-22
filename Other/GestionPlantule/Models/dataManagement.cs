using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static QRCoder.PayloadGenerator.ShadowSocksConfig;

namespace GestionPlantule.Models
{
    public class dataManagement
    {
        private static User? _currentUser;

        // Méthode pour obtenir l'utilisateur actuel
        public User? GetCurrentUser()
        {
            return _currentUser;
        }

        // Méthode pour définir l'utilisateur actuel
        public static void SetCurrentUser(User? user)
        {
            _currentUser = user;

            // Mettre à jour le label utilisateur si nécessaire
            var mainPage = Application.Current.MainPage as MainPage;
            mainPage?.userNameChange(_currentUser?.UserName);
        }

        // Génération de salt
        private string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        // Hachage du mot de passe avec le sel et le nom d'utilisateur
        private string HashPassword(string userName, string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = userName + password + salt;
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hash = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hash);
            }
        }
        public List<Plantule> GetArchivedPlantules()
        {
            using (var dbContext = new ArganaweedContext())
            {
                return dbContext.plantules.Where(p => p.ArchiveStatut == true).ToList();
            }
        }

        // Création d'un nouvel utilisateur
        public void CreateUser(string userName, string password, string userEmail, string statut)
        {
            using (var context = new ArganaweedContext())
            {
                var salt = GenerateSalt();
                var hashedPassword = HashPassword(userName, password, salt);

                var user = new User
                {
                    UserName = userName,
                    HashedPassword = hashedPassword,
                    UserEmail = userEmail,
                    Salt = salt,
                    Statut = statut
                };

                context.users.Add(user);
                context.SaveChanges();
                Console.WriteLine("Utilisateur créé avec succès.");
            }
        }

        // Modification d'un utilisateur existant
        public void UpdateUser(int userId, string userName, string userEmail, string statut)
        {
            try
            {
                using (var context = new ArganaweedContext())
                {
                    var user = context.users.Find(userId);
                    if (user == null) throw new Exception("Utilisateur non trouvé.");

                    user.UserName = userName;
                    user.UserEmail = userEmail;
                    user.Statut = statut;

                    context.SaveChanges();
                    Console.WriteLine("Utilisateur mis à jour avec succès.");
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Une erreur est survenue lors de la mise à jour de l'utilisateur.");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur inattendue est survenue lors de la mise à jour de l'utilisateur.");
                Console.WriteLine(ex.Message);
            }
        }

        // Suppression d'un utilisateur
        public void DeleteUser(int userId)
        {
            try
            {
                using (var context = new ArganaweedContext())
                {
                    var user = context.users.Find(userId);
                    if (user == null) throw new Exception("Utilisateur non trouvé.");

                    context.users.Remove(user);
                    context.SaveChanges();
                    Console.WriteLine("Utilisateur supprimé avec succès.");
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Une erreur est survenue lors de la suppression de l'utilisateur.");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur inattendue est survenue lors de la suppression de l'utilisateur.");
                Console.WriteLine(ex.Message);
            }
        }

        // Authentification d'un utilisateur
        public bool AuthenticateUser(string userEmail, string password)
        {
            try
            {
                using (var context = new ArganaweedContext())
                {
                    var user = context.users.SingleOrDefault(u => u.UserEmail == userEmail);
                    if (user == null)
                    {
                        Console.WriteLine("Authentification échouée. Utilisateur non trouvé.");
                        return false;
                    }

                    var hashedPassword = HashPassword(user.UserName, password, user.Salt);
                    if (user.HashedPassword == hashedPassword)
                    {
                        SetCurrentUser(user);
                        Console.WriteLine("Authentification réussie.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Authentification échouée. Mot de passe incorrect.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue lors de l'authentification de l'utilisateur.");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Déconnexion de l'utilisateur
        public void LogoutUser()
        {
            _currentUser = null; // Réinitialise l'utilisateur actuel
            Console.WriteLine("Utilisateur déconnecté avec succès.");
        }

        // User Operations
        #region User Operations

        public void AddUser(string userName, string hashedPassword, string userEmail, string salt, string statut)
        {
            using (var context = new ArganaweedContext())
            {
                var user = new User
                {
                    UserName = userName,
                    HashedPassword = hashedPassword,
                    UserEmail = userEmail,
                    Salt = salt,
                    Statut = statut
                };
                context.users.Add(user);
                context.SaveChanges();
            }
        }

        public void UpdateUsers(int userId, string userName, string userEmail, string statut)
        {
            using (var context = new ArganaweedContext())
            {
                var user = context.users.Find(userId);
                if (user != null)
                {
                    user.UserName = userName;
                    user.UserEmail = userEmail;
                    user.Statut = statut;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteUsers(int userId)
        {
            using (var context = new ArganaweedContext())
            {
                var user = context.users.Find(userId);
                if (user != null)
                {
                    context.users.Remove(user);
                    context.SaveChanges();
                }
            }
        }

        public User GetUserById(int userId)
        {
            using (var context = new ArganaweedContext())
            {
                return context.users.Find(userId);
            }
        }

        public List<User> GetAllUsers()
        {
            using (var context = new ArganaweedContext())
            {
                return context.users.ToList();
            }
        }

        public List<User> SearchUsers(string search, string filter = null)
        {
            using (var context = new ArganaweedContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return GetAllUsers();
                }
                if (string.IsNullOrEmpty(filter))
                {
                    return context.users.Where(u => u.UserName.Contains(search) || u.UserEmail.Contains(search)).ToList();
                }
                return context.users.Where(u => EF.Property<string>(u, filter).Contains(search)).ToList();
            }
        }

        #endregion

        // Provenance Operations
        #region Provenance Operations

        public void AddProvenance(string provenanceNom, string provenanceDescription)
        {
            using (var context = new ArganaweedContext())
            {
                var provenance = new Provenance
                {
                    ProvenanceNom = provenanceNom,
                    ProvenanceDescription = provenanceDescription
                };
                context.provenances.Add(provenance);
                context.SaveChanges();
            }
        }

        public void UpdateProvenance(int provenanceId, string provenanceNom, string provenanceDescription)
        {
            using (var context = new ArganaweedContext())
            {
                var provenance = context.provenances.Find(provenanceId);
                if (provenance != null)
                {
                    provenance.ProvenanceNom = provenanceNom;
                    provenance.ProvenanceDescription = provenanceDescription;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteProvenance(int provenanceId)
        {
            using (var context = new ArganaweedContext())
            {
                var provenance = context.provenances.Find(provenanceId);
                if (provenance != null)
                {
                    context.provenances.Remove(provenance);
                    context.SaveChanges();
                }
            }
        }

        public Provenance GetProvenanceById(int provenanceId)
        {
            using (var context = new ArganaweedContext())
            {
                return context.provenances.Find(provenanceId);
            }
        }

        public List<Provenance> GetAllProvenances()
        {
            using (var context = new ArganaweedContext())
            {
                return context.provenances.ToList();
            }
        }

        public List<Provenance> SearchProvenances(string search, string filter = null)
        {
            using (var context = new ArganaweedContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return GetAllProvenances();
                }
                if (string.IsNullOrEmpty(filter))
                {
                    return context.provenances.Where(p => p.ProvenanceNom.Contains(search) || p.ProvenanceDescription.Contains(search)).ToList();
                }
                return context.provenances.Where(p => EF.Property<string>(p, filter).Contains(search)).ToList();
            }
        }

        #endregion

        // Emplacement Operations
        #region Emplacement Operations

        public void AddEmplacement(string emplacementCode, string emplacementDescription, int storageMax)
        {
            using (var context = new ArganaweedContext())
            {
                var emplacement = new Emplacement
                {
                    EmplacementCode = emplacementCode,
                    EmplacementDescription = emplacementDescription,
                    StorageMax = storageMax
                };
                context.emplacements.Add(emplacement);
                context.SaveChanges();
            }
        }

        public void UpdateEmplacement(int emplacementId, string emplacementCode, string emplacementDescription, int storageMax)
        {
            using (var context = new ArganaweedContext())
            {
                var emplacement = context.emplacements.Find(emplacementId);
                if (emplacement != null)
                {
                    emplacement.EmplacementCode = emplacementCode;
                    emplacement.EmplacementDescription = emplacementDescription;
                    emplacement.StorageMax = storageMax;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteEmplacement(int emplacementId)
        {
            using (var context = new ArganaweedContext())
            {
                var emplacement = context.emplacements.Find(emplacementId);
                if (emplacement != null)
                {
                    context.emplacements.Remove(emplacement);
                    context.SaveChanges();
                }
            }
        }

        public Emplacement GetEmplacementById(int emplacementId)
        {
            using (var context = new ArganaweedContext())
            {
                return context.emplacements.Find(emplacementId);
            }
        }

        public List<Emplacement> GetAllEmplacements()
        {
            using (var context = new ArganaweedContext())
            {
                return context.emplacements.ToList();
            }
        }

        public List<Emplacement> SearchEmplacements(string search, string filter = null)
        {
            using (var context = new ArganaweedContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return GetAllEmplacements();
                }
                if (string.IsNullOrEmpty(filter))
                {
                    return context.emplacements.Where(e => e.EmplacementCode.Contains(search) || e.EmplacementDescription.Contains(search)).ToList();
                }
                return context.emplacements.Where(e => EF.Property<string>(e, filter).Contains(search)).ToList();
            }
        }

        #endregion

        // Variete Operations
        #region Variete Operations

        public void AddVariete(string varieteCode, string varieteNom, string varieteDescription)
        {
            using (var context = new ArganaweedContext())
            {
                var variete = new Variete
                {
                    VarieteCode = varieteCode,
                    VarieteNom = varieteNom,
                    VarieteDescription = varieteDescription
                };
                context.varietes.Add(variete);
                context.SaveChanges();
            }
        }

        public void UpdateVariete(int varieteId, string varieteCode, string varieteNom, string varieteDescription)
        {
            using (var context = new ArganaweedContext())
            {
                var variete = context.varietes.Find(varieteId);
                if (variete != null)
                {
                    variete.VarieteCode = varieteCode;
                    variete.VarieteNom = varieteNom;
                    variete.VarieteDescription = varieteDescription;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteVariete(int varieteId)
        {
            using (var context = new ArganaweedContext())
            {
                var variete = context.varietes.Find(varieteId);
                if (variete != null)
                {
                    context.varietes.Remove(variete);
                    context.SaveChanges();
                }
            }
        }

        public Variete GetVarieteById(int varieteId)
        {
            using (var context = new ArganaweedContext())
            {
                return context.varietes.Find(varieteId);
            }
        }

        public List<Variete> GetAllVarietes()
        {
            using (var context = new ArganaweedContext())
            {
                return context.varietes.ToList();
            }
        }

        public List<Variete> SearchVarietes(string search, string filter = null)
        {
            using (var context = new ArganaweedContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return GetAllVarietes();
                }
                if (string.IsNullOrEmpty(filter))
                {
                    return context.varietes.Where(v => v.VarieteCode.Contains(search) || v.VarieteNom.Contains(search) || v.VarieteDescription.Contains(search)).ToList();
                }
                return context.varietes.Where(v => EF.Property<string>(v, filter).Contains(search)).ToList();
            }
        }

        #endregion

        // Plantule Operations
        #region Plantule Operations



        public void AddPlantule(int varieteId, string plantuleVariete, string plantuleDescription, DateTime dateReception, int provenanceId, string stade, string sante, int emplacementId, bool statut, string qrbase, DateTime? sortieDate, bool archiveStatut)
        {
            using (var context = new ArganaweedContext())
            {
                var plantule = new Plantule
                {
                    VarieteId = varieteId,
                    PlantuleVariete = plantuleVariete,
                    PlantuleDescription = plantuleDescription,
                    DateReception = dateReception,
                    ProvenanceId = provenanceId,
                    Stade = stade,
                    Sante = sante,
                    EmplacementId = emplacementId,
                    Statut = statut,
                    Qrbase = qrbase,
                    SortieDate = sortieDate,
                    ArchiveStatut = archiveStatut
                };
                context.plantules.Add(plantule);
                context.SaveChanges();
            }
        }

        public void UpdatePlantule(int plantuleId, int varieteId, string plantuleVariete, string plantuleDescription, DateTime dateReception, int provenanceId, string stade, string sante, int emplacementId, bool statut, string qrbase, DateTime? sortieDate, bool archiveStatut)
        {
            using (var context = new ArganaweedContext())
            {
                var plantule = context.plantules.Find(plantuleId);
                if (plantule != null)
                {
                    plantule.VarieteId = varieteId;
                    plantule.PlantuleVariete = plantuleVariete;
                    plantule.PlantuleDescription = plantuleDescription;
                    plantule.DateReception = dateReception;
                    plantule.ProvenanceId = provenanceId;
                    plantule.Stade = stade;
                    plantule.Sante = sante;
                    plantule.EmplacementId = emplacementId;
                    plantule.Statut = statut;
                    plantule.Qrbase = qrbase;
                    plantule.SortieDate = sortieDate;
                    plantule.ArchiveStatut = archiveStatut;
                    context.SaveChanges();
                }
            }
        }

        public void DeletePlantule(int plantuleId)
        {
            using (var context = new ArganaweedContext())
            {
                var plantule = context.plantules.Find(plantuleId);
                if (plantule != null)
                {
                    context.plantules.Remove(plantule);
                    context.SaveChanges();
                }
            }
        }

        public Plantule GetPlantuleById(int plantuleId)
        {
            using (var context = new ArganaweedContext())
            {
                return context.plantules.Find(plantuleId);
            }
        }

        public List<Plantule> GetAllPlantule()
        {
            using (var context = new ArganaweedContext())
            {
                return context.plantules.ToList();
            }
        }

        public List<Plantule> SearchPlantules(string search, string filter = null)
        {
            using (var context = new ArganaweedContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return GetAllPlantules();
                }
                if (string.IsNullOrEmpty(filter))
                {
                    return context.plantules.Where(p => p.PlantuleVariete.Contains(search) || p.PlantuleDescription.Contains(search)).ToList();
                }
                return context.plantules.Where(p => EF.Property<string>(p, filter).Contains(search)).ToList();
            }
        }

        public int CountPlantulesByAttribute(string attributeName)
        {
            using (var context = new ArganaweedContext())
            {
                return context.plantules.Count(p => EF.Property<string>(p, attributeName) != null);
            }
        }

        #endregion

        // Event Operations
        #region Event Operations

        public void AddEvent(int plantuleId, DateTime eventDatetime, string eventUserName, string eventSource, string eventLog)
        {
            using (var context = new ArganaweedContext())
            {
                var eventEntity = new Event
                {
                    PlantuleId = plantuleId,
                    EventDatetime = eventDatetime,
                    EventUserName = eventUserName,
                    EventSource = eventSource,
                    EventLog = eventLog
                };
                context.events.Add(eventEntity);
                context.SaveChanges();
            }
        }

        public void UpdateEvent(int eventId, int plantuleId, DateTime eventDatetime, string eventUserName, string eventSource, string eventLog)
        {
            using (var context = new ArganaweedContext())
            {
                var eventEntity = context.events.Find(eventId);
                if (eventEntity != null)
                {
                    eventEntity.PlantuleId = plantuleId;
                    eventEntity.EventDatetime = eventDatetime;
                    eventEntity.EventUserName = eventUserName;
                    eventEntity.EventSource = eventSource;
                    eventEntity.EventLog = eventLog;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteEvent(int eventId)
        {
            using (var context = new ArganaweedContext())
            {
                var eventEntity = context.events.Find(eventId);
                if (eventEntity != null)
                {
                    context.events.Remove(eventEntity);
                    context.SaveChanges();
                }
            }
        }

        public Event GetEventById(int eventId)
        {
            using (var context = new ArganaweedContext())
            {
                return context.events.Find(eventId);
            }
        }

        public List<Event> GetAllEvents()
        {
            using (var context = new ArganaweedContext())
            {
                return context.events.ToList();
            }
        }

        public List<Event> SearchEvents(string search, string filter = null)
        {
            using (var context = new ArganaweedContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return GetAllEvents();
                }
                if (string.IsNullOrEmpty(filter))
                {
                    return context.events.Where(e => e.EventUserName.Contains(search) || e.EventSource.Contains(search) || e.EventLog.Contains(search)).ToList();
                }
                return context.events.Where(e => EF.Property<string>(e, filter).Contains(search)).ToList();
            }
        }

        #endregion

        // Note Operations
        #region Note Operations

        public void AddNote(string noteTexte, DateTime noteDate, int plantuleId, string noteUserName)
        {
            using (var context = new ArganaweedContext())
            {
                var note = new Note
                {
                    NoteTexte = noteTexte,
                    NoteDate = noteDate,
                    PlantuleId = plantuleId,
                    NoteUserName = noteUserName
                };
                context.notes.Add(note);
                context.SaveChanges();
            }
        }

        public void UpdateNote(int noteId, string noteTexte, DateTime noteDate, int plantuleId, string noteUserName)
        {
            using (var context = new ArganaweedContext())
            {
                var note = context.notes.Find(noteId);
                if (note != null)
                {
                    note.NoteTexte = noteTexte;
                    note.NoteDate = noteDate;
                    note.PlantuleId = plantuleId;
                    note.NoteUserName = noteUserName;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteNote(int noteId)
        {
            using (var context = new ArganaweedContext())
            {
                var note = context.notes.Find(noteId);
                if (note != null)
                {
                    context.notes.Remove(note);
                    context.SaveChanges();
                }
            }
        }

        public Note GetNoteById(int noteId)
        {
            using (var context = new ArganaweedContext())
            {
                return context.notes.Find(noteId);
            }
        }

        public List<Note> GetAllNotes()
        {
            using (var context = new ArganaweedContext())
            {
                return context.notes.ToList();
            }
        }

        public List<Note> SearchNotes(string search, string filter = null)
        {
            using (var context = new ArganaweedContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return GetAllNotes();
                }
                if (string.IsNullOrEmpty(filter))
                {
                    return context.notes.Where(n => n.NoteTexte.Contains(search) || n.NoteUserName.Contains(search)).ToList();
                }
                return context.notes.Where(n => EF.Property<string>(n, filter).Contains(search)).ToList();
            }
        }

        #endregion

        // Gestion plantule
        #region Gestion plantule
        // Méthode pour gérer les plantules
        public void GererPlantule(string plantuleVariete, string plantuleDescription, DateTime dateReception, string provenanceNom, string stade, string sante, string emplacementCode, bool statut, DateTime? sortieDate, string qrCode)
        {
            using (var context = new ArganaweedContext())
            {
                // Extraire le code de la variété et l'incrément
                string varieteCode = plantuleVariete.Substring(0, 3);
                string increment = plantuleVariete.Substring(3);

                // Vérifier ou ajouter la variété
                var variete = context.varietes.FirstOrDefault(v => v.VarieteCode == varieteCode);
                if (variete == null)
                {
                    variete = new Variete
                    {
                        VarieteCode = varieteCode,
                        VarieteNom = "Nom par défaut", // Ajustez selon vos besoins
                        VarieteDescription = "Description par défaut" // Ajustez selon vos besoins
                    };
                    context.varietes.Add(variete);
                    context.SaveChanges();
                    increment = "001"; // Initialiser l'incrément
                }
                else
                {
                    // Si la variété existe, trouver le prochain incrément disponible
                    var lastPlantule = context.plantules
                        .Where(p => p.VarieteId == variete.VarieteId)
                        .OrderByDescending(p => p.PlantuleVariete)
                        .FirstOrDefault();

                    if (lastPlantule != null)
                    {
                        int lastIncrement = int.Parse(lastPlantule.PlantuleVariete.Substring(3));
                        increment = (lastIncrement + 1).ToString("D3");
                    }
                    else
                    {
                        increment = "001"; // Initialiser l'incrément si aucune plantule n'existe pour cette variété
                    }
                }

                // Mettre à jour le PlantuleVariete avec le nouvel incrément
                plantuleVariete = varieteCode + increment;

                // Vérifier ou ajouter la provenance
                var provenance = context.provenances.FirstOrDefault(p => p.ProvenanceNom == provenanceNom);
                if (provenance == null)
                {
                    provenance = new Provenance
                    {
                        ProvenanceNom = provenanceNom,
                        ProvenanceDescription = "Description par défaut" // Ajustez selon vos besoins
                    };
                    context.provenances.Add(provenance);
                    context.SaveChanges();
                }

                // Vérifier ou ajouter l'emplacement
                var emplacement = context.emplacements.FirstOrDefault(e => e.EmplacementCode == emplacementCode);
                if (emplacement == null)
                {
                    emplacement = new Emplacement
                    {
                        EmplacementCode = emplacementCode,
                        EmplacementDescription = "Description par défaut", // Ajustez selon vos besoins
                        StorageMax = 100 // Ajustez selon vos besoins
                    };
                    context.emplacements.Add(emplacement);
                    context.SaveChanges();
                }

                // Vérifier ou ajouter la plantule
                var plantule = context.plantules.FirstOrDefault(p => p.PlantuleVariete == plantuleVariete);
                if (plantule != null)
                {
                    // Mise à jour de la plantule existante
                    plantule.PlantuleDescription = plantuleDescription;
                    plantule.DateReception = dateReception;
                    plantule.Stade = stade;
                    plantule.Sante = sante;
                    plantule.Statut = statut;
                    plantule.Qrbase = qrCode;
                    plantule.SortieDate = sortieDate;
                    if (sortieDate != null && !statut)
                    {
                        plantule.ArchiveStatut = true;
                    }
                    context.SaveChanges();
                    AddEvent(plantule.PlantuleId, DateTime.Now, GetCurrentUser()?.UserName ?? "Système", "Mise à jour", "Modification de la plantule");
                }
                else
                {
                    // Création de la nouvelle plantule
                    plantule = new Plantule
                    {
                        VarieteId = variete.VarieteId,
                        PlantuleVariete = plantuleVariete,
                        PlantuleDescription = plantuleDescription,
                        DateReception = dateReception,
                        ProvenanceId = provenance.ProvenanceId,
                        Stade = stade,
                        Sante = sante,
                        EmplacementId = emplacement.EmplacementId,
                        Statut = statut,
                        Qrbase = qrCode,
                        SortieDate = sortieDate,
                        ArchiveStatut = sortieDate != null && !statut
                    };
                    context.plantules.Add(plantule);
                    context.SaveChanges();
                    AddEvent(plantule.PlantuleId, DateTime.Now, GetCurrentUser()?.UserName ?? "Système", "Insertion", "Création de la plantule");
                }

                // Ajouter un événement pour la sortie si la date de sortie est non nulle
                if (sortieDate != null)
                {
                    AddEvent(plantule.PlantuleId, DateTime.Now, GetCurrentUser()?.UserName ?? "Système", "Sortie", "Sortie de la plantule");
                }
            }
        }

        public void AddOrUpdatePlantule(string plantuleId, string varieteNom, string plantuleDescription, DateTime dateReception, string provenanceNom, string stade, string sante, string emplacementCode, DateTime? sortieDate, string qrCode)
        {
            using (var context = new ArganaweedContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Verify or add Variete
                        var variete = context.varietes.FirstOrDefault(v => v.VarieteNom == varieteNom);
                        if (variete == null)
                        {
                            throw new Exception("Variete not found");
                        }

                        // Generate PlantuleVariete if null
                        if (string.IsNullOrEmpty(plantuleId))
                        {
                            var lastPlantule = context.plantules
                                .Where(p => p.VarieteId == variete.VarieteId)
                                .OrderByDescending(p => p.PlantuleVariete)
                                .FirstOrDefault();

                            int increment = lastPlantule != null
                                ? int.Parse(lastPlantule.PlantuleVariete.Substring(3)) + 1
                                : 1;

                            plantuleId = variete.VarieteCode + increment.ToString("D3");
                        }

                        // Verify or add Provenance
                        var provenance = context.provenances.FirstOrDefault(p => p.ProvenanceNom == provenanceNom);
                        if (provenance == null)
                        {
                            provenance = new Provenance
                            {
                                ProvenanceNom = provenanceNom,
                                ProvenanceDescription = "Default Description"
                            };
                            context.provenances.Add(provenance);
                            context.SaveChanges();
                        }

                        // Verify or add Emplacement
                        var emplacement = context.emplacements.FirstOrDefault(e => e.EmplacementCode == emplacementCode);
                        if (emplacement == null)
                        {
                            emplacement = new Emplacement
                            {
                                EmplacementCode = emplacementCode,
                                EmplacementDescription = "Default Description",
                                StorageMax = 100
                            };
                            context.emplacements.Add(emplacement);
                            context.SaveChanges();
                        }

                        Plantule plantule = context.plantules.FirstOrDefault(p => p.PlantuleVariete == plantuleId);
                        if (plantule != null)
                        {
                            // Update existing plantule
                            plantule.PlantuleDescription = plantuleDescription;
                            plantule.DateReception = dateReception;
                            plantule.ProvenanceId = provenance.ProvenanceId;
                            plantule.Stade = stade;
                            plantule.Sante = sante;
                            plantule.EmplacementId = emplacement.EmplacementId;
                            plantule.Statut = sortieDate == null; // Statut set to true if sortieDate is null
                            plantule.Qrbase = qrCode;
                            plantule.SortieDate = sortieDate;
                            plantule.ArchiveStatut = sortieDate != null; // ArchiveStatut set to true if sortieDate is not null

                            context.SaveChanges();
                            //AddEvent(plantule.PlantuleId, DateTime.Now, GetCurrentUser()?.UserName ?? "System", "Update", "Updated Plantule");
                        }
                        else
                        {
                            // Create new plantule
                            plantule = new Plantule
                            {
                                VarieteId = variete.VarieteId,
                                PlantuleVariete = plantuleId,
                                PlantuleDescription = plantuleDescription,
                                DateReception = dateReception,
                                ProvenanceId = provenance.ProvenanceId,
                                Stade = stade,
                                Sante = sante,
                                EmplacementId = emplacement.EmplacementId,
                                Statut = sortieDate == null, // Statut set to true if sortieDate is null
                                Qrbase = qrCode,
                                SortieDate = sortieDate,
                                ArchiveStatut = sortieDate != null // ArchiveStatut set to true if sortieDate is not null
                            };
                            context.plantules.Add(plantule);
                            context.SaveChanges();
                            //AddEvent(plantule.PlantuleId, DateTime.Now, GetCurrentUser()?.UserName ?? "System", "Insert", "Inserted Plantule");
                        }

                        if (sortieDate != null)
                        {
                            //AddEvent(plantule.PlantuleId, DateTime.Now, GetCurrentUser()?.UserName ?? "System", "Sortie", "Plantule exited");
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public List<Plantule> GetAllPlantules()
        {
            using (var context = new ArganaweedContext())
            {
                return context.plantules.Include(p => p.Variete).Include(p => p.Provenance).Include(p => p.Emplacement).ToList();
            }
        }

        #endregion
    }
}
