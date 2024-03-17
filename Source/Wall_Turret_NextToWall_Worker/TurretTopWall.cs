using RimWorld;
using UnityEngine;
using Verse;

public class TurretTopWall(Building_Turret ParentTurret)
{
    private const float IdleTurnDegreesPerTick = 0.26f;

    private const int IdleTurnDuration = 140;

    private const int IdleTurnIntervalMin = 150;

    private const int IdleTurnIntervalMax = 350;

    private float curRotationInt;

    private bool idleTurnClockwise;

    private int idleTurnTicksLeft;

    private int ticksUntilIdleTurn;

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
        var currentTarget = ParentTurret.CurrentTarget;
        if (currentTarget.IsValid)
        {
            CurRotation = (currentTarget.Cell.ToVector3Shifted() - ParentTurret.DrawPos).AngleFlat();
            ticksUntilIdleTurn = Rand.RangeInclusive(IdleTurnIntervalMin, IdleTurnIntervalMax);
        }
        else if (ticksUntilIdleTurn > 0)
        {
            ticksUntilIdleTurn--;
            if (ticksUntilIdleTurn != 0)
            {
                return;
            }

            idleTurnClockwise = Rand.Value < 0.5f;

            idleTurnTicksLeft = IdleTurnDuration;
        }
        else
        {
            if (idleTurnClockwise)
            {
                CurRotation += IdleTurnDegreesPerTick;
            }
            else
            {
                CurRotation -= IdleTurnDegreesPerTick;
            }

            idleTurnTicksLeft--;
            if (idleTurnTicksLeft <= 0)
            {
                ticksUntilIdleTurn = Rand.RangeInclusive(IdleTurnIntervalMin, IdleTurnIntervalMax);
            }
        }
    }

    public void DrawTurret()
    {
        var b = new Vector3(ParentTurret.def.building.turretTopOffset.x, 0f,
            ParentTurret.def.building.turretTopOffset.y);
        var turretTopDrawSize = ParentTurret.def.building.turretTopDrawSize;
        var matrix = default(Matrix4x4);
        matrix.SetTRS(ParentTurret.DrawPos + Altitudes.AltIncVect + b, CurRotation.ToQuat(),
            new Vector3(turretTopDrawSize, 1f, turretTopDrawSize));
        Graphics.DrawMesh(MeshPool.plane10, matrix, ParentTurret.def.building.turretTopMat, 0);
    }
}