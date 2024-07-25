using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ArganaWeedAppDevEx.Models;

namespace ArganaWeedAppDevEx.Services
{
    public class ApiService<T> : IDataStore<T> where T : BaseModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public ApiService(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<bool> AddItemAsync(T item)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{item.Id}", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<T> GetItemAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{_endpoint}/{id}");
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<T>>(_endpoint);
        }

        public IEnumerable<T> GetItems(bool forceRefresh = false)
        {
            // Only for mock purposes; not used with API
            throw new NotImplementedException();
        }

        public async Task<bool> CheckApiConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_endpoint);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
