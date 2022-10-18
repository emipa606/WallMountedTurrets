using Verse;

public class PlaceWorker_NextToWall_Turret : PlaceWorker
{
    public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
        Thing thingToIgnore = null, Thing thing = null)

    {
        var c = loc - rot.FacingCell;

        var support = c.GetEdifice(map);
        if (support == null)
        {
            return "MessagePlacementAgainstSupport".Translate();
        }

        if (
            support.def?.graphicData == null
        )
        {
            return "MessagePlacementAgainstSupport".Translate();
        }

        return (support.def.graphicData.linkFlags & (LinkFlags.Rock | LinkFlags.Wall)) != 0
            ? AcceptanceReport.WasAccepted
            : "MessagePlacementAgainstSupport".Translate();
    }
}