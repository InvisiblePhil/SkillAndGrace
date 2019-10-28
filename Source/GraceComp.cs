using Verse;

namespace SkillAndGrace
{
    /// <summary>
    /// Stores a record of skills over level 10, and the number of ticks since their last use.
    /// 
    /// This is updated on a RareTicks interval, so this value could be incorrect by up to 249 ticks.
    /// I consider this fine as it feels wasteful and overkill to be accurate to the exact tick.
    /// </summary>
    public class GraceComp : ThingComp
    {
        public GraceProperties Properties => (GraceProperties) props;

        public override void Initialize(CompProperties props)
        {
            // hack: I gave up on trying to define XML for an empty list...
            base.Initialize(new GraceProperties());
        }

        public override void CompTickRare()
        {
            base.CompTickRare();

            if (!(parent is Pawn pawn))
            {
                // Shouldn't happen but whatever
                return;
            }

            if (pawn.Dead)
            {
                return;
            }

            if (IsInStasis(pawn))
            {
                return;
            }

            IncrementAllPeriodsByOneRareTick();
        }

        private static bool IsInStasis(Thing pawn)
        {
            var holder = pawn.ParentHolder;

            return holder != null
                   && ThingOwnerUtility.ContentsSuspended(holder);
        }

        private void IncrementAllPeriodsByOneRareTick()
        {
            foreach (var skill in Properties.SkillPeriods)
            {
                skill.TicksSinceLastUse += TickConstants.TicksPerRareTick;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Collections.Look(ref Properties.SkillPeriods, GraceSkillHelpers.SerialisationLabel(nameof(GraceProperties.SkillPeriods)), LookMode.Deep);
        }
    }
}