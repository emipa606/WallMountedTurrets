using Verse;

namespace WallMountedTurretSettings;

public class WallMountedTurretSettings : ModSettings
{
    public bool MyTurretDefNameFlameIsVisible = true;
    public bool MyTurretDefNameIsVisible = true;
    public bool MyTurretDefNameMinigunIsVisible = true;
    public bool MyTurretDefNameRocketIsVisible = true;
    public bool WallFlameTurretBarrel = true;
    public bool WallMinigunTurretBarrel = true;
    public bool WallRocketTurretBarrel = true;
    public bool WallTurretBarrel = true;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref MyTurretDefNameIsVisible,
            "MyTurretDefNameIsVisible"); // This will save the enabled/disabled check that the player has given
        Scribe_Values.Look(ref MyTurretDefNameFlameIsVisible, "MyTurretDefNameFlameIsVisible");
        Scribe_Values.Look(ref MyTurretDefNameRocketIsVisible, "MyTurretDefNameRocketIsVisible");
        Scribe_Values.Look(ref MyTurretDefNameMinigunIsVisible, "MyTurretDefNameMinigunIsVisible");
        Scribe_Values.Look(ref WallTurretBarrel, "WallTurretBarrel");
        Scribe_Values.Look(ref WallFlameTurretBarrel, "WallFlameTurretBarrel");
        Scribe_Values.Look(ref WallRocketTurretBarrel, "WallRocketTurretBarrel");
        Scribe_Values.Look(ref WallMinigunTurretBarrel, "WallMinigunTurretBarrel");
    }
}