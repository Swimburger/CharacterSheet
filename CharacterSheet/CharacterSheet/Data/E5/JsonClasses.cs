using Newtonsoft.Json;

namespace CharacterSheet.Data.E5
{
    public class ListResponse<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public T Results { get; set; }
    }

    public class Equipment
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class EquipmentDetails
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("equipment_category")]
        public EquipmentCategory EquipmentCategory { get; set; }
    }

    public class Weapon : EquipmentDetails
    {
        [JsonProperty("weapon_category")]
        public string WeaponCategory { get; set; }

        [JsonProperty("weapon_range")]
        public string WeaponRange { get; set; }

        [JsonProperty("category_range")]
        public string CategoryRange { get; set; }

        [JsonProperty("cost")]
        public Cost Cost { get; set; }

        [JsonProperty("damage")]
        public Damage Damage { get; set; }

        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("properties")]
        public Property[] Properties { get; set; }
    }

    public class Armor : EquipmentDetails
    {
        [JsonProperty("armor_category")]
        public string ArmorCategory { get; set; }

        [JsonProperty("armor_class")]
        public ArmorClass ArmorClass { get; set; }

        [JsonProperty("str_minimum")]
        public int StrMinimum { get; set; }

        [JsonProperty("stealth_disadvantage")]
        public bool StealthDisadvantage { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("cost")]
        public Cost Cost { get; set; }
    }

    public class AdventuringGear : EquipmentDetails
    {
        public GearCategory GearCategory { get; set; }

        [JsonProperty("cost")]
        public Cost Cost { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }
    }

    public class EquipmentCategory
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Cost
    {
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class Damage
    {
        [JsonProperty("damage_dice")]
        public string DamageDice { get; set; }

        [JsonProperty("damage_type")]
        public DamageType DamageType { get; set; }
    }

    public class DamageType
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Range
    {
        [JsonProperty("normal")]
        public int Normal { get; set; }

        [JsonProperty("long")]
        public object Long { get; set; }
    }

    public class Property
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ArmorClass
    {
        [JsonProperty("base")]
        public int Base { get; set; }

        [JsonProperty("dex_bonus")]
        public bool DexBonus { get; set; }

        [JsonProperty("max_bonus")]
        public object MaxBonus { get; set; }
    }

    public class GearCategory
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }


    public class EquipmentPack : EquipmentDetails
    {
        [JsonProperty("gear_category")]
        public GearCategory GearCategory { get; set; }

        [JsonProperty("cost")]
        public Cost Cost { get; set; }

        [JsonProperty("contents")]
        public EquipmentPackContent[] Contents { get; set; }
    }


    public class EquipmentPackContent
    {
        [JsonProperty("item")]
        public EquipmentPackContentItem Item { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }

    public class EquipmentPackContentItem
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
