using System.Reflection;
using Harmony;
using UnityEngine;
using Verse;

namespace SkillAndGrace
{
    public class SkillAndGraceMod : Mod
    {
        public SkillAndGraceSettings Settings;

        public SkillAndGraceMod(ModContentPack content) : base(content)
        {
            var harmony = HarmonyInstance.Create("invisible.phil.skill_and_grace");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Settings = GetSettings<SkillAndGraceSettings>();
        }

        public override string SettingsCategory()
            => TranslatedStrings.SettingsCategory.Translate();

        public override void DoSettingsWindowContents(Rect rect)
        {
            var list = new Listing_Standard(GameFont.Small)
            {
                ColumnWidth = rect.width / 3
            };

            list.Begin(rect);

            list.Gap();
            
            LabelledInputBox(list, ref SkillAndGraceSettings.GracePeriodHours, 0, 1440, TranslatedStrings.GracePeriodLabel, TranslatedStrings.GracePeriodTip);

			list.Gap();
            
            LabelledInputBox(list, ref SkillAndGraceSettings.GreatMemoryGraceMultiplierPercent, 100, 10000, TranslatedStrings.GreatMemoryLabel, TranslatedStrings.GreatMemoryTip);
            
            list.Gap();

            LabelledInputBox(list, ref SkillAndGraceSettings.PassionGraceMultiplierPercent, 100, 10000, TranslatedStrings.PassionMultiplerLabel, TranslatedStrings.PassionMultiplerTip);
            
            list.Gap();

            LabelledInputBox(list, ref SkillAndGraceSettings.BurningPassionGraceMultiplierPercent, 100, 10000, TranslatedStrings.BurningPassionMultiplierLabel, TranslatedStrings.BurningPassionMultiplierTip);

			list.End();
        }

        private static void LabelledInputBox(Listing list, ref int value, int min, int max, string label, string tooltip)
        {
            var buffer = value.ToString();
            var rectLine = list.GetRect(Text.LineHeight * 2);
            var rectLeft = rectLine.LeftHalf().Rounded();
            var rectRight = rectLine.RightHalf().Rounded();

            Widgets.DrawHighlightIfMouseover(rectLine);
            TooltipHandler.TipRegion(rectLine, tooltip.Translate());

            var anchorBuffer = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(rectLeft, label.Translate());
            Text.Anchor = anchorBuffer;
            Widgets.TextFieldNumeric(rectRight, ref value, ref buffer, min, max);
        }
    }
}