using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ArganaWeedApp.DTOs;
using ArganaWeedApp.Models;
using ArganaWeedApp.ViewModels;

namespace ArganaWeedApp.Services
{
    public class ApiService
    {

        private static readonly string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5153" : "http://localhost:5153";
        private static readonly HttpClient sharedClient = new HttpClient
        {
            BaseAddress = new Uri(BaseAddress),
        };


        #region GenericApi

       
        public static async Task<List<TItem>> GetItemsAsync<TRequest, TResponse, TItem>(string endpoint)
             where TRequest : BaseRequest, new()
             where TResponse : BaseResponse<TItem>, new()
        {
            var request = new TRequest();
            var clientRequest = new ClientRequestResponse<TRequest, TResponse, TItem>(request, endpoint);

            try
            {
                TResponse response = await clientRequest.ReceiveGet();

                if (response != null && response.Items != null)
                {
                    return response.Items;
                }
                else
                {
                    await AlertService.Instance.ShowAlert("Erreur", "Erreur ou aucun élément trouvé !", "OK");
                    return new List<TItem>();
                }
            }
            catch (Exception ex)
            {
                await AlertService.Instance.ShowAlert("Erreur", $"Erreur lors de l'appel au serveur {endpoint}: {ex.Message}", "OK");
                return new List<TItem>();
            }
        }
        



        public static async Task<string> UpdateItemAsync<TRequest, TResponse, TItem>(string endpoint, int id, TRequest request)
                    where TRequest : class
                    where TResponse : BaseResponse<TItem>, new()
        {
            try
            {
                string jsonData = ServiceSerializer<TRequest>.Serialize(request);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await sharedClient.PutAsync($"{endpoint}/{id}", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                if (IsJson(responseBody))
                {
                    TResponse deserializedResponse = ServiceSerializer<TResponse>.Deserialize(responseBody);

                    if (deserializedResponse != null && deserializedResponse.Items != null && deserializedResponse.Items.Count > 0)
                    {
                        await AlertService.Instance.ShowAlert("Succès", "Élément mis à jour avec succès.", "OK");
                        return "Élément mis à jour avec succès.";
                    }
                    else
                    {
                        return "Erreur lors de la mise à jour de l'élément ou aucun élément mis à jour.";
                    }
                }
                else
                {
                    return responseBody;
                }
            }
            catch (HttpRequestException e)
            {
                return $"Erreur lors de l'appel au serveur {endpoint} : {e.Message}";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'appel au serveur {endpoint} : {ex.Message}";
            }
        }

        public static async Task<string> DeleteItemAsync<TResponse, TItem>(string endpoint, int id)
            where TResponse : BaseResponse<TItem>, new()
        {
            try
            {
                HttpResponseMessage response = await sharedClient.DeleteAsync($"{endpoint}/{id}");
                string responseBody = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                if (IsJson(responseBody))
                {
                    TResponse deserializedResponse = ServiceSerializer<TResponse>.Deserialize(responseBody);

                    if (deserializedResponse != null && deserializedResponse.Items != null && deserializedResponse.Items.Count > 0)
                    {
                        await AlertService.Instance.ShowAlert("Succès", "Élément supprimé avec succès.", "OK");
                        return "Élément supprimé avec succès.";
                    }
                    else
                    {
                        return "Erreur lors de la suppression de l'élément ou aucun élément supprimé.";
                    }
                }
                else
                {
                    return responseBody;
                }
            }
            catch (HttpRequestException e)
            {
                return $"Erreur lors de l'appel au serveur {endpoint} : {e.Message}";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'appel au serveur {endpoint} : {ex.Message}";
            }
        }


        public static async Task AddItemAsync<TRequest, TResponse, TItem>(string endpoint, TRequest request)
             where TRequest : class
             where TResponse : BaseResponse<TItem>, new()
        {
            try
            {
                string jsonData = ServiceSerializer<TRequest>.Serialize(request);
                //Console.WriteLine("Sending data: " + jsonData); // Log the data being sent
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await sharedClient.PostAsync(endpoint, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine("Response data: " + responseBody); // Log the response data
                response.EnsureSuccessStatusCode();

                // Vérifiez si la réponse est un JSON valide
                if (IsJson(responseBody))
                {
                    TResponse deserializedResponse = ServiceSerializer<TResponse>.Deserialize(responseBody);

                    if (deserializedResponse != null && deserializedResponse.Items != null && deserializedResponse.Items.Count > 0)
                    {
                        await AlertService.Instance.ShowAlert("Succès", "Élément ajouté avec succès.", "OK");
                    }
                    else
                    {
                        await AlertService.Instance.ShowAlert("Erreur", "Erreur lors de l'ajout de l'élément ou aucun élément ajouté.", "OK");
                    }
                }
                else
                {
                    //Console.WriteLine(responseBody); // Affichez la réponse du serveur
                    await AlertService.Instance.ShowAlert("Succès", responseBody, "OK");
                }
            }
            catch (HttpRequestException e)
            {
                //Console.WriteLine($"Request error: {e.Message}");
                await AlertService.Instance.ShowAlert("Erreur", $"Erreur lors de l'appel au serveur {endpoint} : {e.Message}", "OK");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"General error: {ex.Message}");
                await AlertService.Instance.ShowAlert("Erreur", $"Erreur lors de l'appel au serveur {endpoint} : {ex.Message}", "OK");
            }
        }

        public static async Task<string> UpdateItemNoIdAsync<TRequest, TResponse, TItem>(string endpoint, TRequest request)
    where TRequest : class
    where TResponse : BaseResponse<TItem>, new()
        {
            try
            {
                string jsonData = ServiceSerializer<TRequest>.Serialize(request);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await sharedClient.PutAsync(endpoint, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                if (IsJson(responseBody))
                {
                    TResponse deserializedResponse = ServiceSerializer<TResponse>.Deserialize(responseBody);

                    if (deserializedResponse != null && deserializedResponse.Items != null && deserializedResponse.Items.Count > 0)
                    {
                        await AlertService.Instance.ShowAlert("Succès", "Élément mis à jour avec succès.", "OK");
                        return "Élément mis à jour avec succès.";
                    }
                    else
                    {
                        return "Erreur lors de la mise à jour de l'élément ou aucun élément mis à jour.";
                    }
                }
                else
                {
                    return responseBody;
                }
            }
            catch (HttpRequestException e)
            {
                return $"Erreur lors de l'appel au serveur {endpoint} : {e.Message}";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'appel au serveur {endpoint} : {ex.Message}";
            }
        }



        private static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }


        /*
        public static async Task AddItemAsync<TRequest, TResponse, TItem>(string endpoint, TRequest request)
            where TRequest : BaseRequest
            where TResponse : BaseResponse<TItem>, new()
        {
            var clientRequest = new ClientRequestResponse<TRequest, TResponse, TItem>(request, endpoint);

            try
            {
                TResponse response = await clientRequest.ReceivePost();

                if (response != null && response.Items != null && response.Items.Count > 0)
                {
                    await AlertService.Instance.ShowAlert("Succès", "Élément ajouté avec succès.", "OK");
                }
                else
                {
                    await AlertService.Instance.ShowAlert("Erreur", "Erreur lors de l'ajout de l'élément ou aucun élément ajouté.", "OK");
                }
            }
            catch (Exception ex)
            {
                await AlertService.Instance.ShowAlert("Erreur", $"Erreur lors de l'appel au serveur {endpoint} : {ex.Message}", "OK");
            }
        }

        
        public static async Task AddItemAsync<TRequest, TResponse, TItem>(string endpoint, TRequest request)
            where TRequest : BaseRequest
            where TResponse : BaseResponse<TItem>, new()
        {
            var clientRequest = new ClientRequestResponse<TRequest, TResponse, TItem>(request, endpoint);
            TResponse response = await clientRequest.ReceivePost();

            if (response != null && response.Items != null && response.Items.Count > 0)
            {
                Console.WriteLine("Item added successfully.");
                Console.WriteLine(response.Items[0].ToString());
            }
            else
            {
                Console.WriteLine("Error adding item or no item added.");
            }
        }*/
        #endregion

        #region Emplacements

        public static async Task<List<Emplacement>> GetEmplacementsAsync()
        {
            return await GetItemsAsync<GenericRequest, EmplacementsResponse, Emplacement>("/api/emplacements");
        }

        public static async Task<Emplacement> GetEmplacementByIdAsync(int id)
        {
            var items = await GetItemsAsync<GenericRequest, EmplacementsResponse, Emplacement>($"/api/emplacements/{id}");
            return items.FirstOrDefault();
        }

        public static async Task<List<Emplacement>> SearchEmplacementsAsync(string searchString)
        {
            return await GetItemsAsync<GenericRequest, EmplacementsResponse, Emplacement>($"/api/emplacements/search/{searchString}");
        }

        public static async Task AddEmplacementAsync(Emplacement emplacement)
        {
            var emplacementRequest = new EmplacementRequest
            {
                EmplacementCode = emplacement.EmplacementCode,
                EmplacementDescription = emplacement.EmplacementDescription
            };

            await AddItemAsync<EmplacementRequest, EmplacementsResponse, Emplacement>("/api/emplacements", emplacementRequest);
        }

        public static async Task<string> UpdateEmplacementAsync(Emplacement emplacement)
        {
            return await UpdateItemAsync<Emplacement, EmplacementsResponse, Emplacement>("/api/emplacements", emplacement.EmplacementId, emplacement);
        }


        public static async Task<string> DeleteEmplacementAsync(int emplacementId)
        {
            return await DeleteItemAsync<EmplacementsResponse, Emplacement>("/api/emplacements", emplacementId);
        }

        
        public static async Task AddPlantuleAsync(PlantuleAddRequest request)
        {
            await AddItemAsync<PlantuleAddRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules", request);
        }

        public static async Task<int> GetLatestPlantuleIdAsync()
        {
            var plantules = await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules");
            return plantules.Max(p => p.PlantuleId);
        }



        public static async Task<string> UpdatePlantuleEmplacementAsync(int id, UpdateEmplacementRequest request)
        {
            return await UpdateItemAsync<UpdateEmplacementRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/emplacement", id, request);
        }

        public static async Task<string> UpdatePlantuleSortieAsync(int id, UpdateSortieRequest request)
        {
            return await UpdateItemAsync<UpdateSortieRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/sortie", id, request);
        }

        
        public static async Task<string> UpdatePlantuleStadeAsync(int id, UpdateStadeRequest request)
        {
            return await UpdateItemAsync<UpdateStadeRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/stade", id, request);
        }

        public static async Task<string> UpdatePlantuleSanteAsync(int id, UpdateSanteRequest request)
        {
            return await UpdateItemAsync<UpdateSanteRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/sante", id, request);
        }

        public static async Task<string> ArchivePlantulesAsync(ArchivePlantulesRequest request)
        {
            return await UpdateItemAsync<ArchivePlantulesRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/archive", 0, request);
        }


        #endregion


        #region Varite GET
        /*
        public static async Task GetVarietesAsync()
        {
            await GetItemsAsync<GenericRequest, VarietesResponse, Variete>("/api/varietes");
        }

        public static async Task GetVarieteByIdAsync(int id)
        {
            await GetItemsAsync<GenericRequest, VarietesResponse, Variete>($"/api/varietes/{id}");
        }

        public static async Task SearchVarieteAsync(string searchString)
        {
            await GetItemsAsync<GenericRequest, VarietesResponse, Variete>($"/api/varietes/search/{searchString}");
        }
        */
        public static async Task<List<Variete>> GetVarietesAsync()
        {
            return await GetItemsAsync<GenericRequest, VarietesResponse, Variete>("/api/Varietes");
        }

        public static async Task<Variete> GetVarieteByIdAsync(int id)
        {
            var items = await GetItemsAsync<GenericRequest, VarietesResponse, Variete>($"/api/Varietes/{id}");
            return items.FirstOrDefault();
        }

        public static async Task<List<Variete>> SearchVarietesAsync(string searchString)
        {
            return await GetItemsAsync<GenericRequest, VarietesResponse, Variete>($"/api/Varietes/search/{searchString}");
        }

        public static async Task AddVarieteAsync(Variete Variete)
        {
            var VarieteRequest = new VarieteRequest
            {
                VarieteCode = Variete.VarieteCode,
                VarieteNom = Variete.VarieteNom,
                VarieteCategorie = Variete.VarieteCategorie,
                VarieteDescription = Variete.VarieteDescription

            };

            await AddItemAsync<VarieteRequest, VarietesResponse, Variete>("/api/Varietes", VarieteRequest);
        }

        public static async Task<string> UpdateVarieteAsync(Variete Variete)
        {
            return await UpdateItemAsync<Variete, VarietesResponse, Variete>("/api/Varietes", Variete.VarieteId, Variete);
        }

        public static async Task<string> DeleteVarieteAsync(int VarieteId)
        {
            return await DeleteItemAsync<VarietesResponse, Variete>("/api/Varietes", VarieteId);
        }


        #endregion

        #region Plantules GET
        public static async Task GetPlantulesAsync()
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules");
        }
        /*
        public static async Task GetPlantuleByIdAsync(int id)
        {
            //await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/{id}");
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/{id}");

        }
        */
        public static async Task<PlantuleDetail> GetPlantuleByIdAsync(int id)
        {
            var items = await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/{id}");
            return items.FirstOrDefault();
        }


        public static async Task<List<PlantuleDetail>> SearchPlantulesAsync(string searchString)
        {
            return await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/search/{searchString}");
        }

        public static async Task GetPlantuleBySlugAsync(string slug)
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/slug/{slug}");
        }

        public static async Task GetPlantulesByVarieteAsync(string varieteCode)
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/variete/{varieteCode}");
        }

        public static async Task<List<PlantuleDetail>> GetPlantulesActiveAsync()
        {
            return await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/active");
        }


        public static async Task<List<PlantuleDetail>> GetPlantulesInactiveAsync()
        {
            return await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/inactive");
        }

        public static async Task<List<PlantuleDetail>> GetPlantulesArchivedAsync()
        {
            return await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/archived");
        }








        #endregion

        #region Events GET
        public static async Task<List<Event>> GetEventsAsync()
        {
            return await GetItemsAsync<GenericRequest, EventsResponse, Event>("/api/events");
        }

        public static async Task<Event> GetEventByIdAsync(int id)
        {
            var items = await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/{id}");
            return items.FirstOrDefault();
        }

        public static async Task<List<Event>> GetEventsByPlantuleIdAsync(int plantuleId)
        {
            return await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/plantule/{plantuleId}");
        }

        public static async Task<List<Event>> GetEventsByDateAsync(string eventDate)
        {
            return await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/date/{eventDate}");
        }

        public static async Task<List<Event>> GetEventsByUserNameAsync(string userName)
        {
            return await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/username/{userName}");
        }

        public static async Task<List<Event>> GetEventsByTypeAsync(string eventType)
        {
            return await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/type/{eventType}");
        }

        public static async Task<List<Event>> GetEventsByNatureAsync(string eventNature)
        {
            return await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/nature/{eventNature}");
        }
        #endregion

        #region Notes GET
        
        public static async Task<List<Note>> GetNotesAsync()
        {
            return await GetItemsAsync<GenericRequest, NotesResponse, Note>("/api/notes");
        }

        public static async Task<Note> GetNoteByIdAsync(int id)
        {
            var items = await GetItemsAsync<GenericRequest, NotesResponse, Note>($"/api/notes/{id}");
            return items.FirstOrDefault();
        }

        public static async Task<List<Note>> GetNotesByPlantuleIdAsync(int plantuleId)
        {
            return await GetItemsAsync<GenericRequest, NotesResponse, Note>($"/api/notes/plantule/{plantuleId}");
        }

        public static async Task<List<Note>> GetNotesByDateAsync(string noteDate)
        {
            return await GetItemsAsync<GenericRequest, NotesResponse, Note>($"/api/notes/date/{noteDate}");
        }

        public static async Task<List<Note>> GetNotesByUserNameAsync(string userName)
        {
            return await GetItemsAsync<GenericRequest, NotesResponse, Note>($"/api/notes/username/{userName}");
        }

        public static async Task AddNoteAsync(Note note)
        {
            var noteRequest = new NoteRequest
            {
                NoteTexte = note.NoteTexte,
                NoteRappelDate = note.NoteRappelDate,
                NoteDate = note.NoteDate,
                PlantuleId = note.PlantuleId
            };

            await AddItemAsync<NoteRequest, NotesResponse, Note>("/api/notes", noteRequest);
        }



        #endregion

        #region Provenances GET

        public static async Task<List<Provenance>> GetProvenancesAsync()
        {
            return await GetItemsAsync<GenericRequest, ProvenancesResponse, Provenance>("/api/provenances");
        }

        public static async Task<Provenance> GetProvenanceByIdAsync(int id)
        {
            var items = await GetItemsAsync<GenericRequest, ProvenancesResponse, Provenance>($"/api/provenances/{id}");
            return items.FirstOrDefault();
        }

        public static async Task<List<Provenance>> SearchProvenancesAsync(string searchString)
        {
            return await GetItemsAsync<GenericRequest, ProvenancesResponse, Provenance>($"/api/provenances/search/{searchString}");
        }

        public static async Task AddProvenanceAsync(Provenance provenance)
        {
            var provenanceRequest = new ProvenanceRequest
            {
                ProvenanceNom = provenance.ProvenanceNom,
                ProvenanceDescription = provenance.ProvenanceDescription
            };

            await AddItemAsync<ProvenanceRequest, ProvenancesResponse, Provenance>("/api/provenances", provenanceRequest);
        }


        public static async Task<string> UpdateProvenanceAsync(Provenance provenance)
        {
            return await UpdateItemAsync<Provenance, ProvenancesResponse, Provenance>("/api/provenances", provenance.ProvenanceId, provenance);
        }

        public static async Task<string> DeleteProvenanceAsync(int provenanceId)
        {
            return await DeleteItemAsync<ProvenancesResponse, Provenance>("/api/provenances", provenanceId);
        }







        #endregion

        #region Users GET
        /*
        public static async Task GetUsersAsync()
        {
            await GetItemsAsync<GenericRequest, UsersResponse, User>("/api/users");
        }

        public static async Task GetUserByIdAsync(int id)
        {
            await GetItemsAsync<GenericRequest, UsersResponse, User>($"/api/users/{id}");
        }

        public static async Task SearchUsersAsync(string searchString)
        {
            await GetItemsAsync<GenericRequest, UsersResponse, User>($"/api/users/search/{searchString}");
        }
        */
        public static async Task<List<User>> GetUsersAsync()
        {
            return await GetItemsAsync<GenericRequest, UsersResponse, User>("/api/users");
        }

        public static async Task<User> GetUserByIdAsync(int id)
        {
            var items = await GetItemsAsync<GenericRequest, UsersResponse, User>($"/api/users/{id}");
            return items.FirstOrDefault();
        }

        public static async Task<List<User>> SearchUsersAsync(string searchString)
        {
            return await GetItemsAsync<GenericRequest, UsersResponse, User>($"/api/users/search/{searchString}");
        }



        #endregion
        #region Users PostUpdateDelete
        public static async Task AddUserAsync(User user)
        {
            var userRequest = new UserRequest
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                //HashedPassword = user.HashedPassword,
                IsActive = user.IsActive,
                IsAdministrator = user.IsAdministrator,
                IsOwner = user.IsOwner,
                IsAgent = user.IsAgent,
                IsViewer = user.IsViewer
            };

            await AddItemAsync<UserRequest, UsersResponse, User>("/api/users", userRequest);
        }




        #endregion

        #region ImportPlantules
        /*
        public static async Task<string> ImportPlantulesAsync(List<PlantuleImport> plantules)
        {
            try
            {
                var content = JsonContent.Create(plantules);
                HttpResponseMessage response = await sharedClient.PostAsync("/api/plantules/import", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return "Importation réussie.";
                }
                else
                {
                    return $"Erreur lors de l'importation des données : {responseBody}";
                }
            }
            catch (HttpRequestException e)
            {
                return $"Erreur lors de l'appel au serveur : {e.Message}";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'importation des données : {ex.Message}";
            }
        }
        */
        public static async Task<string> ImportPlantulesAsync(List<PlantuleImport> plantules)
        {
            try
            {
                var content = JsonContent.Create(plantules);
                HttpResponseMessage response = await sharedClient.PostAsync("/api/plantules/import", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return "Importation réussie.";
                }
                else
                {
                    return $"Erreur lors de l'importation des données : {responseBody}";
                }
            }
            catch (HttpRequestException e)
            {
                return $"Erreur lors de l'appel au serveur : {e.Message}";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de l'importation des données : {ex.Message}";
            }
        }


        #endregion

        #region LoboInfos



        public static async Task<LaboInfo> GetLaboInfoAsync()
        {
            try
            {
                var items = await GetItemsAsync<GenericRequest, LaboInfosResponse, LaboInfo>("/api/laboInfos");
                return items.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Log or handle the error as needed
                // You might want to log the exception or return a default LaboInfo object
                return null;
            }
        }

        public static async Task<string> UpdateLaboInfoAsync(LaboInfo laboInfo)
        {
            try
            {
                return await UpdateItemNoIdAsync<LaboInfo, LaboInfosResponse, LaboInfo>("/api/LaboInfos", laboInfo);
            }
            catch (Exception ex)
            {
                return $"Erreur lors de la mise à jour: {ex.Message}";
            }
            
        }

        /*
        public static async Task<string> UpdateLaboInfoAsync(LaboInfo laboInfo)
            {
                try
                {
                    return await UpdateItemAsync<LaboInfo, LaboInfoResponse, LaboInfo>("/api/laboInfos", 0, laboInfo);
                }
                catch (Exception ex)
                {
                    // Log or handle the error as needed
                    return $"Erreur lors de la mise à jour: {ex.Message}";
                }
            }*/




        #endregion

    }
}

