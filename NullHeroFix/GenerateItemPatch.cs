using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace NullHeroFix
{
    [HarmonyPatch(typeof(Crafting), "GenerateItem")]
    internal class GenerateItemPatch
    {
        public static bool Prefix(ref ItemObject itemObject, WeaponDesign weaponDesign)
        {
            //SubModule.holder = new NullHeroFixHolder();
            if (weaponDesign == null || weaponDesign.Template == null || weaponDesign.Template.ItemHolsters == null || itemObject.ItemHolsters == null)
            {
                itemObject = null;
                itemObject = new ItemObject(SubModule.holder.null_butter)
                {
                    StringId = "null_butter_" + SubModule.nullObjects
                };
                itemObject.Initialize();
                CraftingTemplate template = itemObject.WeaponDesign.Template;
                weaponDesign = new WeaponDesign(template, new TextObject(itemObject.StringId), SubModule.holder.elements);
            }
            SubModule.nullObjects++;
            return true;
        }
    }
}
