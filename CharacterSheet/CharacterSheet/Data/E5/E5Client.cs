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

        public async Task<ListResponse<Equipment[]>> GetEquipments()
        {
            var response = await httpClient.GetAsync("equipment");
            response.EnsureSuccessStatusCode();
            var equipments = JsonConvert.DeserializeObject<ListResponse<Equipment[]>>(
                await response.Content.ReadAsStringAsync()
            );
            return equipments;
        }

        public async Task<EquipmentDetails> GetEquipment(string index)
        {
            var response = await httpClient.GetAsync($"equipment/{index}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var type = DetectEquipmentType(responseContent);
            var equipment = (EquipmentDetails)JsonConvert.DeserializeObject(responseContent, type);
            return equipment;
        }

        private readonly Dictionary<string, Type> EquipmentCategoryToTypeMap = new Dictionary<string, Type>
        {
            {
                "\"equipment_category\":{\"index\":\"weapon\",\"name\":\"Weapon\",\"url\":\"/api/equipment-categories/weapon\"}",
                typeof(Weapon)
            },
            {
                "\"equipment_category\":{\"index\":\"armor\",\"name\":\"Armor\",\"url\":\"/api/equipment-categories/armor\"}",
                typeof(Armor)
            },
            {
                "\"gear_category\":{\"index\":\"equipment-packs\",\"name\":\"Equipment Packs\",\"url\":\"/api/equipment-categories/equipment-packs\"}",
                typeof(EquipmentPack)
            },
            {
                "\"equipment_category\":{\"index\":\"adventuring-gear\",\"name\":\"Adventuring Gear\",\"url\":\"/api/equipment-categories/adventuring-gear\"}",
                typeof(AdventuringGear)
            }
        };

        private Type DetectEquipmentType(string json)
        {
            foreach (var detectionString in EquipmentCategoryToTypeMap.Keys)
            {
                if (json.Contains(detectionString))
                {
                    return EquipmentCategoryToTypeMap[detectionString];
                }
            }

            return typeof(EquipmentDetails);
        }
    }
}
