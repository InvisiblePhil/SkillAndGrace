using System.Linq;
using System.Reflection;
using Harmony;
using RimWorld;
using Verse;

namespace SkillAndGrace
{
    internal static class GraceSkillHelpers
    {
        private static readonly FieldInfo PawnField = AccessTools.Field(typeof(SkillRecord), "pawn");

        public static Pawn GetPawn(this SkillRecord instance) => PawnField.GetValue(instance) as Pawn;

        public static GraceComp GetGrace(this Pawn pawn) => pawn.GetComp<GraceComp>();

        private static GraceSkill Find(GraceComp grace, SkillRecord record)
        {
            var id = SkillUniqueId(record);
            return grace.Properties.SkillPeriods.FirstOrDefault(s => s.SkillDef == id);
        }

        public static bool TryGetTimeSinceLastUse(GraceComp grace, SkillRecord record, out int ticksSinceLastUse)
        {
            var skill = Find(grace, record);
            if (skill == null)
            {
                ticksSinceLastUse = -1;
                return false;
            }

            ticksSinceLastUse = skill.TicksSinceLastUse;
            return true;
        }

        public static void SetSkillUsed(GraceComp grace, SkillRecord record)
        {
            var skill = Find(grace, record);

            if (skill == null)
            {
                skill = new GraceSkill
                {
                    SkillDef = SkillUniqueId(record)
                };
                grace.Properties.SkillPeriods.Add(skill);
            }

            skill.TicksSinceLastUse = 0;
        }

        private static string SkillUniqueId(SkillRecord record)
            => record.def.defName;

        public static string SerialisationLabel(string name) => "Skill_And_Grace_Mod_" + name;

        public static void LogNullGrace()
        {
            Log.ErrorOnce("SkillAndGrace incorrectly configured - Pawn does not have GraceComp", 999999999);
        }

        public static bool IgnoreSkill(SkillRecord skillRecord)
            => skillRecord.Level < 10;
    }
}