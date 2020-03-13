using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace WallMountedTurretSettings
{
    public class WallMountedTurret : Mod
    {
        private readonly WallMountedTurretSettings settings;

        public WallMountedTurret(ModContentPack content) : base(content)
        {
            settings = GetSettings<WallMountedTurretSettings>();
        }
        // This adds the 'window' page and what options there are
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("Enable Wall Turret?", ref settings.MyTurretDefNameIsVisible, "This enables or disables the wall turret");
            listingStandard.CheckboxLabeled("Enable Flame Turret?", ref settings.MyTurretDefNameFlameIsVisible, "This enables or disables the flame turret");
            listingStandard.CheckboxLabeled("Enable Rocket Turret?", ref settings.MyTurretDefNameRocketIsVisible, "This enables or disables the rocket turret");
            listingStandard.CheckboxLabeled("Enable Minigun Turret?", ref settings.MyTurretDefNameMinigunIsVisible, "This enables or disables the minigun turret");
            listingStandard.CheckboxLabeled("Enable replacing barrels for the wall turret?", ref settings.WallTurretBarrel, "This enables or disables refueling the normal wall turret");
            listingStandard.CheckboxLabeled("Enable refueling the flame turret?", ref settings.WallFlameTurretBarrel, "This enables or disables refueling the flame turret");
            listingStandard.CheckboxLabeled("Enable replacing rockets for the rocket turret?", ref settings.WallRocketTurretBarrel, "This enables or disables refueling the rocket turret");
            listingStandard.CheckboxLabeled("Enable replacing barrels for the minigun turret?", ref settings.WallMinigunTurretBarrel, "This enables or disables refueling the minigun turret");
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory() => "Wall Mounted Turret";

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

            if (settings.WallTurretBarrel) //What this does is check to see if the 'WallTurretBarrel' checkbox has been enabled or not, if it's false, it removes the need to refuel the barrels from the XML
            {
                DefDatabase<ThingDef>.GetNamed("WallTurret").comps.RemoveAll(x => x is CompProperties_Refuelable);
            }

            if (settings.WallFlameTurretBarrel)
            {
                DefDatabase<ThingDef>.GetNamed("WallFlameTurret").comps.RemoveAll(x => x is CompProperties_Refuelable);
            }

            if (settings.WallRocketTurretBarrel)
            {
                DefDatabase<ThingDef>.GetNamed("WallRocketTurret").comps.RemoveAll(x => x is CompProperties_Refuelable);
            }

            if (settings.WallMinigunTurretBarrel)
            {
                DefDatabase<ThingDef>.GetNamed("WallTurretMiniGun").comps.RemoveAll(x => x is CompProperties_Refuelable);
            }
        } 

    }


    public class WallMountedTurretSettings : ModSettings
    {
        public bool MyTurretDefNameIsVisible = true;
        public bool MyTurretDefNameFlameIsVisible = true;
        public bool MyTurretDefNameRocketIsVisible = true;
        public bool MyTurretDefNameMinigunIsVisible = true;
        public bool WallTurretBarrel = true;
        public bool WallFlameTurretBarrel = true;
        public bool WallRocketTurretBarrel = true;
        public bool WallMinigunTurretBarrel = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref MyTurretDefNameIsVisible, "MyTurretDefNameIsVisible"); // This will save the enabled/disabled check that the player has given
            Scribe_Values.Look(ref MyTurretDefNameFlameIsVisible, "MyTurretDefNameFlameIsVisible");
            Scribe_Values.Look(ref MyTurretDefNameRocketIsVisible, "MyTurretDefNameRocketIsVisible");
            Scribe_Values.Look(ref MyTurretDefNameMinigunIsVisible, "MyTurretDefNameMinigunIsVisible");
            Scribe_Values.Look(ref WallTurretBarrel, "WallTurretBarrel");
            Scribe_Values.Look(ref WallFlameTurretBarrel, "WallFlameTurretBarrel");
            Scribe_Values.Look(ref WallRocketTurretBarrel, "WallRocketTurretBarrel");
            Scribe_Values.Look(ref WallMinigunTurretBarrel, "WallMinigunTurretBarrel");
        }
    }

    [StaticConstructorOnStartup]
    public static class SettingsImplementerOnStartUp
    {
        static SettingsImplementerOnStartUp()
        {
            LoadedModManager.GetMod<WallMountedTurret>().ImplementSettings();
        }
    }
}
