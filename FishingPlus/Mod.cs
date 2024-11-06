using GDWeave;
using FishingPlus.Mods;

namespace FishingPlus;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.Logger.Information("FishingPlus has loaded!");
        modInterface.RegisterScriptMod(new InjectConfig());
        modInterface.RegisterScriptMod(new InjectMenu());
        modInterface.RegisterScriptMod(new InjectPlayerAutism());
        modInterface.RegisterScriptMod(new FishChance());
        modInterface.RegisterScriptMod(new LootTable());
        modInterface.RegisterScriptMod(new NeedFish());
        modInterface.RegisterScriptMod(new AutoSelectBait());
        modInterface.RegisterScriptMod(new AutoCollectBuddies());
        modInterface.RegisterScriptMod(new PatientLurePatch());
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
