using System;
using Harmony;
using RimWorld;
using Verse;

namespace SkillAndGrace
{
    /// <summary>
    /// Class to add prefix hook to the 'SkillRecord.Interval' method, which is the method that implements skill decay
    /// </summary>
    [HarmonyPriority(Priority.First)]
    [HarmonyPatch(typeof(SkillRecord))]
    [HarmonyPatch(nameof(SkillRecord.Interval))]
    internal static class SkillRecordInterval
    {
        internal static bool Prefix(SkillRecord __instance)
        {
            if (GraceSkillHelpers.IgnoreSkill(__instance))
            {
                return true;
            }

            var pawn = __instance.GetPawn();
            
            var grace = pawn.GetGrace();
            
            if (!GraceSkillHelpers.TryGetTimeSinceLastUse(grace, __instance, out var ticksSinceLastUse))
            {
                return true;
            }

            return !SkillIsWithinGracePeriod(__instance, pawn, ticksSinceLastUse);
        }

        private static bool SkillIsWithinGracePeriod(SkillRecord instance, Pawn pawn, int ticksSinceLastUse) 
            => ticksSinceLastUse < SkillAndGraceSettings.GracePeriodTicks * GraceMultiplier(pawn, instance);

        private static float GraceMultiplier(Pawn pawn, SkillRecord instance) 
            => PassionMultiplier(instance) + GreatMemoryMultiplier(pawn);

        private static float GreatMemoryMultiplier(Pawn pawn)
            => pawn.story.traits.HasTrait(TraitDefOf.GreatMemory)
                ? SkillAndGraceSettings.GreatMemoryGraceMultiplierPercent / 100f
                : 1.0f;

        private static float PassionMultiplier(SkillRecord instance)
        {
            switch (instance.passion)
            {
                case Passion.None:
                    return 1.0f;

                case Passion.Minor:
                    return SkillAndGraceSettings.PassionGraceMultiplierPercent / 100f;

                case Passion.Major:
                    return SkillAndGraceSettings.BurningPassionGraceMultiplierPercent / 100f;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
