using HarmonyLib;
using TaleWorlds.Core;
using System.Reflection;

namespace NullHeroFix
{
    internal class CraftingPatch
    {
        public static bool Prefix()
        {
            MethodInfo original = AccessTools.Method(typeof(Crafting), "GenerateItem");
            MethodInfo patch = AccessTools.Method(typeof(GenerateItemPatch), nameof(GenerateItemPatch.Prefix));
            HarmonyMethod method = new HarmonyMethod(patch);
            SubModule.harmony.Patch(original, prefix: method);
            return true;
        }
    }
}
