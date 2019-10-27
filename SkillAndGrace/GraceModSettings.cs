using UnityEngine;
using Verse;

namespace SkillAndGrace
{
    // todo: not sure if this will be needed
    //[HarmonyPatch(typeof(DefGenerator))]
    //[HarmonyPatch(nameof(DefGenerator.GenerateImpliedDefs_PostResolve))]
    //public class GraceModSettingsJockey
    //{
    //    internal static void Postfix()
    //    {

    //    }
    //}


    /// <summary>
    /// Global settings
    /// </summary>
    public class SkillAndGraceSettings : ModSettings
    {
        /// <summary>
        /// Number of hours since increasing a skill before skill decay takes place
        /// </summary>
        public static int GracePeriodHours;

        /// <summary>
        /// Number of ticks 
        /// </summary>
        public static int GracePeriodTicks => GracePeriodHours * TickConstants.TicksPerHour;

        /// <summary>
        /// Multiplier to the grace period for colonists with a passion in a skill
        /// </summary>
        public static int PassionGraceMultiplierPercent = 100;
        
        /// <summary>
        /// Multiplier to the grace period for colonists with a burning passion in a skill
        /// </summary>
        public static int BurningPassionGraceMultiplierPercent = 100;

        /// <summary>
        /// Extra multiplier to the grace period for colonists with great memory
        /// </summary>
        public static int GreatMemoryGraceMultiplierPercent = 100;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref GracePeriodHours, GraceSkillHelpers.SerialisationLabel(nameof(GracePeriodHours)));
            Scribe_Values.Look(ref PassionGraceMultiplierPercent, GraceSkillHelpers.SerialisationLabel(nameof(PassionGraceMultiplierPercent)));
            Scribe_Values.Look(ref BurningPassionGraceMultiplierPercent, GraceSkillHelpers.SerialisationLabel(nameof(BurningPassionGraceMultiplierPercent)));
            Scribe_Values.Look(ref GreatMemoryGraceMultiplierPercent, GraceSkillHelpers.SerialisationLabel(nameof(GreatMemoryGraceMultiplierPercent)));
        }

        public void DoSettingsWindowContents(Rect rect)
        {
            var list = new Listing_Standard(GameFont.Small)
            {
                ColumnWidth = rect.width / 3
            };

            list.Begin(rect);

            list.Gap();

        }
    }
}