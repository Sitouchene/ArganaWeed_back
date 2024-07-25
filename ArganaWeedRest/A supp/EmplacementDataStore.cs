using ArganaWeedAppDevEx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArganaWeedAppDevEx.Services
{
    public class EmplacementDataStore : IDataStore<Emplacement>
    {
        readonly List<Emplacement> emplacements;

        public EmplacementDataStore()
        {
            emplacements = new List<Emplacement>()
            {
                new Emplacement { EmplacementId = Guid.NewGuid().ToString(), EmplacementCode = "E001", EmplacementDescription = "First Emplacement" },
                new Emplacement { EmplacementId = Guid.NewGuid().ToString(), EmplacementCode = "E002", EmplacementDescription = "Second Emplacement" }
            };
        }

        public async Task<bool> AddItemAsync(Emplacement emplacement)
        {
            emplacements.Add(emplacement);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Emplacement emplacement)
        {
            var oldEmplacement = emplacements.FirstOrDefault(e => e.EmplacementId == emplacement.EmplacementId);
            if (oldEmplacement != null)
            {
                emplacements.Remove(oldEmplacement);
                emplacements.Add(emplacement);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldEmplacement = emplacements.FirstOrDefault(e => e.EmplacementId == id);
            if (oldEmplacement != null)
            {
                emplacements.Remove(oldEmplacement);
            }
            return await Task.FromResult(true);
        }

        public async Task<Emplacement> GetItemAsync(string id)
        {
            return await Task.FromResult(emplacements.FirstOrDefault(e => e.EmplacementId == id));
        }

        public async Task<IEnumerable<Emplacement>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(emplacements);
        }

        public IEnumerable<Emplacement> GetItems(bool forceRefresh = false)
        {
            return emplacements;
        }
    }
}
