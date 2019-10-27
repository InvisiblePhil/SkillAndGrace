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
            Settings.DoSettingsWindowContents(rect);
        }
    }
}