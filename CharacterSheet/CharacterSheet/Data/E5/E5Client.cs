using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CharacterSheet.Data.E5
{
    public class E5Client
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://www.dnd5eapi.co/api/")
        };

        public async Task<ListResponse<Equipment[]>> GetEquipmentsAsync()
        {
            var response = await httpClient.GetAsync("equipment");
            response.EnsureSuccessStatusCode();
            var equipments = JsonConvert.DeserializeObject<ListResponse<Equipment[]>>(
                await response.Content.ReadAsStringAsync()
            );
            return equipments;
        }

        public async Task<EquipmentDetails> GetEquipmentDetailsAsync(string index)
        {
            var response = await httpClient.GetAsync($"equipment/{index}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var equipmentDetails = JsonConvert.DeserializeObject<EquipmentDetails>(responseContent);
            return equipmentDetails;
        }

        public async Task<ListResponse<MagicItem[]>> GetMagicItemsAsync()
        {
            var response = await httpClient.GetAsync("magic-items");
            response.EnsureSuccessStatusCode();
            var magicItems = JsonConvert.DeserializeObject<ListResponse<MagicItem[]>>(
                await response.Content.ReadAsStringAsync()
            );
            return magicItems;
        }

        public async Task<MagicItemDetails> GetMagicItemDetailsAsync(string index)
        {
            var response = await httpClient.GetAsync($"magic-items/{index}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var magicItemDetails = JsonConvert.DeserializeObject<MagicItemDetails>(responseContent);
            return magicItemDetails;
        }
    }
}
