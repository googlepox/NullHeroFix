using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace NullHeroFix
{
	// Token: 0x02000002 RID: 2
	public class SubModule : MBSubModuleBase
    {
        public static int nullObjects = 0;
        public static Harmony harmony = new Harmony("NullHeroFix");
        internal static NullHeroFixHolder holder;

		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
            base.OnBeforeInitialModuleScreenSetAsRoot();
			//harmony.PatchAll();
            InformationManager.DisplayMessage(new InformationMessage("Successfully loaded NullHeroFix.", SubModule.textColor));
		}

		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			base.OnGameStart(game, gameStarterObject);
            holder = new NullHeroFixHolder(game);
            MethodInfo original = AccessTools.Method(typeof(CraftingCampaignBehavior), "InitializeCraftingElements");
            MethodInfo patch = AccessTools.Method(typeof(CraftingPatch), nameof(CraftingPatch.Prefix));
            HarmonyMethod method = new HarmonyMethod(patch);
            SubModule.harmony.Patch(original, prefix: method);
            if (game.GameType is Campaign)
			{
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarterObject;
				try
                {
                    campaignGameStarter.AddBehavior(new NullHeroFixBehavior());
				}
				catch (Exception ex)
				{
					InformationManager.DisplayMessage(new InformationMessage("Error while initialising NullHeroFix:\n\n " + ex.Message + " \n\n " + ex.StackTrace, SubModule.textColor));
				}
			}
		}

		public static readonly Color textColor = Color.FromUint(6750401U);
	}
}
