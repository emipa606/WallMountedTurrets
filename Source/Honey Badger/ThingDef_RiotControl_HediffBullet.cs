using System.Collections.Generic;
using Verse;

namespace Honey_Badger;

public class ThingDef_RiotControl_HediffBullet : ThingDef
{
    public float AddHediffChance;

    public List<HediffDef> HediffsToAdd;
}