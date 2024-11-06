using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FishingPlus;

public class Config {
    [JsonInclude] public bool CurrentFishChance = true;
    [JsonInclude] public bool LootTableView = true;
    [JsonInclude] public bool NeedCertainFish = true;
    [JsonInclude] public string[] FishIDs = ["fish_lake_guppy"];
    [JsonInclude] public bool AutoSelectBait = true;
    [JsonInclude] public bool AutoCollectBuddies = false;
    [JsonInclude] public bool PatientLure = false;
}
