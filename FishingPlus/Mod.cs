using GDWeave;
using FishingPlus.Mods;

namespace FishingPlus;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.Logger.Information("FishingPlus has loaded!");

        if (Config.CurrentFishChance) modInterface.RegisterScriptMod(new FishChance());
        if (Config.LootTableView) modInterface.RegisterScriptMod(new LootTable());
        if (Config.NeedCertainFish) modInterface.RegisterScriptMod(new NeedFish(Config.FishIDs));
        if (Config.AutoSelectBait) modInterface.RegisterScriptMod(new AutoSelectBait());
        if (Config.AutoCollectBuddies) modInterface.RegisterScriptMod(new AutoCollectBuddies());
        if (Config.PatientLure) modInterface.RegisterScriptMod(new PatientLurePatch());


    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
