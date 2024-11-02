using System.Text.Json.Serialization;

namespace FishingPlus;

public class Config {
    [JsonInclude] public bool CurrentFishChance = true;
    [JsonInclude] public bool LootTableView = true;
    [JsonInclude] public bool NeedCertainFish = true;
    [JsonInclude] public string FishID = "fish_ocean_clownfish";
    [JsonInclude] public bool AutoSelectBait = true;
}
