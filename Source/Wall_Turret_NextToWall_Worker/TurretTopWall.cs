using RimWorld;
using UnityEngine;
using Verse;

public class TurretTopWall
{
    private const float IdleTurnDegreesPerTick = 0.26f;

    private const int IdleTurnDuration = 140;

    private const int IdleTurnIntervalMin = 150;

    private const int IdleTurnIntervalMax = 350;
    private readonly Building_Turret parentTurret;

    private float curRotationInt;

    private bool idleTurnClockwise;

    private int idleTurnTicksLeft;

    private int ticksUntilIdleTurn;

    public TurretTopWall(Building_Turret ParentTurret)
    {
        parentTurret = ParentTurret;
    }

    private float CurRotation
    {
        get => curRotationInt;
        set
        {
            curRotationInt = value;
            if (curRotationInt > 360f)
            {
                curRotationInt -= 360f;
            }

            if (curRotationInt < 0f)
            {
                curRotationInt += 360f;
            }
        }
    }

    public void TurretTopTick()
    {
        var currentTarget = parentTurret.CurrentTarget;
        if (currentTarget.IsValid)
        {
            CurRotation = (currentTarget.Cell.ToVector3Shifted() - parentTurret.DrawPos).AngleFlat();
            ticksUntilIdleTurn = Rand.RangeInclusive(150, 350);
        }
        else if (ticksUntilIdleTurn > 0)
        {
            ticksUntilIdleTurn--;
            if (ticksUntilIdleTurn != 0)
            {
                return;
            }

            idleTurnClockwise = Rand.Value < 0.5f;

            idleTurnTicksLeft = 140;
        }
        else
        {
            if (idleTurnClockwise)
            {
                CurRotation += 0.26f;
            }
            else
            {
                CurRotation -= 0.26f;
            }

            idleTurnTicksLeft--;
            if (idleTurnTicksLeft <= 0)
            {
                ticksUntilIdleTurn = Rand.RangeInclusive(150, 350);
            }
        }
    }

    public void DrawTurret()
    {
        var b = new Vector3(parentTurret.def.building.turretTopOffset.x, 0f,
            parentTurret.def.building.turretTopOffset.y);
        var turretTopDrawSize = parentTurret.def.building.turretTopDrawSize;
        var matrix = default(Matrix4x4);
        matrix.SetTRS(parentTurret.DrawPos + Altitudes.AltIncVect + b, CurRotation.ToQuat(),
            new Vector3(turretTopDrawSize, 1f, turretTopDrawSize));
        Graphics.DrawMesh(MeshPool.plane10, matrix, parentTurret.def.building.turretTopMat, 0);
    }
}