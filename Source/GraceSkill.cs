using Verse;

namespace SkillAndGrace
{
    public class GraceSkill : IExposable
    {
        public string SkillDef;
        public int TicksSinceLastUse;

        public void ExposeData()
        {
            Scribe_Values.Look(ref SkillDef, nameof(GraceSkill) + nameof(SkillDef));
            Scribe_Values.Look(ref TicksSinceLastUse, nameof(GraceSkill) + nameof(TicksSinceLastUse));
        }
    }
}