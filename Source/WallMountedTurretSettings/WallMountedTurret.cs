using System.Collections.Generic;
using Mlie;
using RimWorld;
using UnityEngine;
using Verse;

namespace WallMountedTurretSettings;

public class WallMountedTurret : Mod
{
    private static string currentVersion;
    private readonly WallMountedTurretSettings settings;

    public WallMountedTurret(ModContentPack content) : base(content)
    {
        settings = GetSettings<WallMountedTurretSettings>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(
                ModLister.GetActiveModWithIdentifier("Mlie.WallMountedTurrets"));
    }

    // This adds the 'window' page and what options there are
    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        listingStandard.CheckboxLabeled("WMT.enablewt".Translate(), ref settings.MyTurretDefNameIsVisible,
            "WMT.enablewt.tooltip".Translate());
        listingStandard.CheckboxLabeled("WMT.enableft".Translate(), ref settings.MyTurretDefNameFlameIsVisible,
            "WMT.enableft.tooltip".Translate());
        listingStandard.CheckboxLabeled("WMT.enablert".Translate(), ref settings.MyTurretDefNameRocketIsVisible,
            "WMT.enablert.tooltip".Translate());
        listingStandard.CheckboxLabeled("WMT.enablemg".Translate(), ref settings.MyTurretDefNameMinigunIsVisible,
            "WMT.enablemg.tooltip".Translate());
        listingStandard.CheckboxLabeled("WMT.refuelingwt".Translate(),
            ref settings.WallTurretBarrel, "WMT.refuelingwt.tooltip".Translate());
        listingStandard.CheckboxLabeled("WMT.refuelingft".Translate(), ref settings.WallFlameTurretBarrel,
            "WMT.refuelingft.tooltip".Translate());
        listingStandard.CheckboxLabeled("WMT.refuelingrt".Translate(),
            ref settings.WallRocketTurretBarrel, "WMT.refuelingrt.tooltip".Translate());
        listingStandard.CheckboxLabeled("WMT.refuelingmg".Translate(),
            ref settings.WallMinigunTurretBarrel, "WMT.refuelingmg.tooltip".Translate());
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("WMT.CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Wall Mounted Turret";
    }

    public override void WriteSettings()
    {
        ImplementSettings();
        base.WriteSettings();
    }

    // This searches for the 'MyTurretDefNameIsVisible' to see if it's enabled/disabled
    public void ImplementSettings()
    {
        if (settings.MyTurretDefNameIsVisible == false)
        {
            var myTurret = DefDatabase<ThingDef>.GetNamed("WallTurret");
            if (myTurret.buildingPrerequisites == null)
            {
                myTurret.buildingPrerequisites = new List<ThingDef>();
            }

            myTurret.buildingPrerequisites.Add(myTurret);
        }

        if (settings.MyTurretDefNameFlameIsVisible == false)
        {
            var myTurret = DefDatabase<ThingDef>.GetNamed("WallFlameTurret");
            if (myTurret.buildingPrerequisites == null)
            {
                myTurret.buildingPrerequisites = new List<ThingDef>();
            }

            myTurret.buildingPrerequisites.Add(myTurret);
        }

        if (settings.MyTurretDefNameRocketIsVisible == false)
        {
            var myTurret = DefDatabase<ThingDef>.GetNamed("WallRocketTurret");
            if (myTurret.buildingPrerequisites == null)
            {
                myTurret.buildingPrerequisites = new List<ThingDef>();
            }

            myTurret.buildingPrerequisites.Add(myTurret);
        }

        if (settings.MyTurretDefNameMinigunIsVisible == false)
        {
            var myTurret = DefDatabase<ThingDef>.GetNamed("WallTurretMiniGun");
            if (myTurret.buildingPrerequisites == null)
            {
                myTurret.buildingPrerequisites = new List<ThingDef>();
            }

            myTurret.buildingPrerequisites.Add(myTurret);
        }

        if (
            !settings.WallTurretBarrel) //What this does is check to see if the 'WallTurretBarrel' checkbox has been enabled or not, if it's false, it removes the need to refuel the barrels from the XML
        {
            DefDatabase<ThingDef>.GetNamed("WallTurret").comps.RemoveAll(x => x is CompProperties_Refuelable);
        }

        if (!settings.WallFlameTurretBarrel)
        {
            DefDatabase<ThingDef>.GetNamed("WallFlameTurret").comps.RemoveAll(x => x is CompProperties_Refuelable);
        }

        if (!settings.WallRocketTurretBarrel)
        {
            DefDatabase<ThingDef>.GetNamed("WallRocketTurret").comps.RemoveAll(x => x is CompProperties_Refuelable);
        }

        if (!settings.WallMinigunTurretBarrel)
        {
            DefDatabase<ThingDef>.GetNamed("WallTurretMiniGun").comps
                .RemoveAll(x => x is CompProperties_Refuelable);
        }
    }
}