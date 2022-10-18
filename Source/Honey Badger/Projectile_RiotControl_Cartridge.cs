using System;
using RimWorld;
using Verse;

namespace Honey_Badger;

public class Projectile_RiotControl_Cartridge : Bullet
{
    public ThingDef_RiotControl_HediffBullet Def => def as ThingDef_RiotControl_HediffBullet;

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        base.Impact(hitThing, blockedByShield);
        Pawn pawn;
        if (Def == null || hitThing == null || (pawn = hitThing as Pawn) == null)
        {
            return;
        }

        if (!pawn.RaceProps.IsFlesh)
        {
            return;
        }

        foreach (var hediffDef in Def.HediffsToAdd)
        {
            if (!(Rand.Value <= Def.AddHediffChance))
            {
                continue;
            }

            Hediff hediff;

            var health = pawn.health;
            if (health == null)
            {
                hediff = null;
            }
            else
            {
                var hediffSet = health.hediffSet;
                hediff = hediffSet?.GetFirstHediffOfDef(hediffDef);
            }

            var hediff2 = hediff;
            var num = Rand.Range(0.25f, 0.4285f) / (float)Math.Pow(pawn.RaceProps.baseBodySize, 1.5);
            if (hediff2 != null)
            {
                hediff2.Severity += num;
            }
            else
            {
                var hediff3 = HediffMaker.MakeHediff(hediffDef, pawn);
                hediff3.Severity = num;
                pawn.health.AddHediff(hediff3);
            }
        }
    }
}