using Harmony;
using RimWorld;

namespace SkillAndGrace
{
    [HarmonyPriority(Priority.HigherThanNormal)]
    [HarmonyPatch(typeof(SkillRecord))]
    [HarmonyPatch(nameof(SkillRecord.Learn))]
    internal static class SkillRecordLearn
    {
        internal static void Postfix(SkillRecord __instance, float xp, bool direct)
        {
            if (xp <= 0)
            {
                // Don't do anything with degrading skills
                return;
            }

            if (GraceSkillHelpers.IgnoreSkill(__instance))
            {
                // Ignore skills too low to degrade
                return;
            }

            var pawn = __instance.GetPawn();
            var grace = pawn.GetGrace();

            GraceSkillHelpers.SetSkillUsed(grace, __instance);
        }
    }
}