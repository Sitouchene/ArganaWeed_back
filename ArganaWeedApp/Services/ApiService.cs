using System;
using System.Net.Http;
using System.Threading.Tasks;
using ArganaWeedApp.DTOs;
using ArganaWeedApp.Models;

namespace ArganaWeedApp.Services
{
    public class ApiService
    {
        /* Test avec android
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://10.0.2.2:5153"),
        };
        */
        private static readonly string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5153" : "http://localhost:5153";
        private static readonly HttpClient sharedClient = new HttpClient
        {
            BaseAddress = new Uri(BaseAddress),
        };


        #region GenericApi

        /*
        public static async Task GetItemsAsync<TRequest, TResponse, TItem>(string endpoint)
            where TRequest : BaseRequest, new()
            where TResponse : BaseResponse<TItem>, new()
        {
            var request = new TRequest();
            var clientRequest = new ClientRequestResponse<TRequest, TResponse, TItem>(request, endpoint);
            TResponse response = await clientRequest.ReceiveGet();

            if (response != null && response.Items != null)
            {
                Console.WriteLine(response.Items.Count);
                foreach (var item in response.Items)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine("Erreur ou aucun élément trouvé !");
            }
        }*/
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
                    Console.WriteLine("Erreur ou aucun élément trouvé !");
                    return new List<TItem>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel au serveur {endpoint}: {ex.Message}");
                throw new RemoteCallException($"Error while Calling server {endpoint}", ex);
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
        }
        #endregion

        #region Emplacement GET
        
        public static async Task<List<Emplacement>> GetEmplacementsAsync()
        {
            return await GetItemsAsync<GenericRequest, EmplacementsResponse, Emplacement>("/api/emplacements");
        }


        /*public static async Task GetEmplacementsAsync()
        {
            await GetItemsAsync<GenericRequest, EmplacementsResponse, Emplacement>("/api/emplacements");
        }*/

        public static async Task GetEmplacementByIdAsync(int id)
        {
            await GetItemsAsync<GenericRequest, EmplacementsResponse, Emplacement>($"/api/emplacements/{id}");
        }

        public static async Task SearchEmplacementsAsync(string searchString)
        {
            await GetItemsAsync<GenericRequest, EmplacementsResponse, Emplacement>($"/api/emplacements/search/{searchString}");
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
        #endregion


        #region Varite GET
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



        #endregion

        #region Plantules GET
        public static async Task GetPlantulesAsync()
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules");
        }

        public static async Task GetPlantuleByIdAsync(int id)
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/{id}");
        }

        public static async Task SearchPlantulesAsync(string searchString)
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/search/{searchString}");
        }

        public static async Task GetPlantuleBySlugAsync(string slug)
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/slug/{slug}");
        }

        public static async Task GetPlantulesByVarieteAsync(string varieteCode)
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>($"/api/plantules/variete/{varieteCode}");
        }

        public static async Task GetPlantulesActiveAsync()
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/active");
        }

        public static async Task GetPlantulesInactiveAsync()
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/inactive");
        }

        public static async Task GetPlantulesArchivedAsync()
        {
            await GetItemsAsync<GenericRequest, PlantulesDetailResponse, PlantuleDetail>("/api/plantules/archived");
        }
        #endregion

        #region Events GET
        public static async Task GetEventsAsync()
        {
            await GetItemsAsync<GenericRequest, EventsResponse, Event>("/api/events");
        }

        public static async Task GetEventByIdAsync(int id)
        {
            await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/{id}");
        }

        public static async Task GetEventsByPlantuleIdAsync(int plantuleId)
        {
            await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/plantule/{plantuleId}");
        }

        public static async Task GetEventsByDateAsync(string eventDate)
        {
            await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/date/{eventDate}");
        }

        public static async Task GetEventsByUserNameAsync(string userName)
        {
            await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/username/{userName}");
        }

        public static async Task GetEventsByTypeAsync(string eventType)
        {
            await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/type/{eventType}");
        }

        public static async Task GetEventsByNatureAsync(string eventNature)
        {
            await GetItemsAsync<GenericRequest, EventsResponse, Event>($"/api/events/nature/{eventNature}");
        }
        #endregion

        #region Notes GET
        public static async Task GetNotesAsync()
        {
            await GetItemsAsync<GenericRequest, NotesResponse, Note>("/api/notes");
        }

        public static async Task GetNoteByIdAsync(int id)
        {
            await GetItemsAsync<GenericRequest, NotesResponse, Note>($"/api/notes/{id}");
        }

        public static async Task GetNotesByPlantuleIdAsync(int plantuleId)
        {
            await GetItemsAsync<GenericRequest, NotesResponse, Note>($"/api/notes/plantule/{plantuleId}");
        }

        public static async Task GetNotesByDateAsync(string noteDate)
        {
            await GetItemsAsync<GenericRequest, NotesResponse, Note>($"/api/notes/date/{noteDate}");
        }

        public static async Task GetNotesByUserNameAsync(string userName)
        {
            await GetItemsAsync<GenericRequest, NotesResponse, Note>($"/api/notes/username/{userName}");
        }
        #endregion

        #region Provenances GET
        public static async Task GetProvenancesAsync()
        {
            await GetItemsAsync<GenericRequest, ProvenancesResponse, Provenance>("/api/provenances");
        }

        public static async Task GetProvenanceByIdAsync(int id)
        {
            await GetItemsAsync<GenericRequest, ProvenancesResponse, Provenance>($"/api/provenances/{id}");
        }

        public static async Task SearchProvenanceAsync(string searchString)
        {
            await GetItemsAsync<GenericRequest, ProvenancesResponse, Provenance>($"/api/provenances/search/{searchString}");
        }
        #endregion

        #region Users GET
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
        #endregion

    }
}



/*
public static async Task Login()
{
    Console.WriteLine("*******  0  *******");

    LoginRequest loginRequest = new LoginRequest
    {
        Email = "salim",
        Password = "123Soleil"
    };

    try
    {
        var o = new ClientRequestResponse<LoginRequest, LoginResponse>(loginRequest, "/Auth/login");
        LoginResponse response = await o.ReceivePost();
        Console.WriteLine(response.Success);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
*/