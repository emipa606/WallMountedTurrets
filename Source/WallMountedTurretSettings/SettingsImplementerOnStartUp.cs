using Verse;

namespace WallMountedTurretSettings;

[StaticConstructorOnStartup]
public static class SettingsImplementerOnStartUp
{
    static SettingsImplementerOnStartUp()
    {
        LoadedModManager.GetMod<WallMountedTurret>().ImplementSettings();
    }
}