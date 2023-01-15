using System;
using RimWorld;
using Verse;
using UnityEngine;

namespace IdeologyDrugMod;

public class Thought_Situational_Precept_Drug : Thought_Situational
{
    protected override float BaseMoodOffset
    {
        get
        {
            if (ThoughtUtility.ThoughtNullified(pawn, def))
            {
                return 0f;
            }

            float x = (float)(Find.TickManager.TicksGame - pawn.mindState.lastTakeRecreationalDrugTick) / 60000f;
            return (float)Math.Round(ThoughtWorker_Precept_Drug.MoodOffsetFromDaysSinceLastDrugCurve.Evaluate(x));
        }
    }
}