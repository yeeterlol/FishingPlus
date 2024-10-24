using System.Text.Json.Serialization;

namespace FishingPlus;

public class Config {
    [JsonInclude] public bool CurrentFishChance = true;
    [JsonInclude] public bool LootTableView = true;
}
