using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace NullHeroFix
{
	// Token: 0x02000002 RID: 2
	public class SubModule : MBSubModuleBase
	{
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			InformationManager.DisplayMessage(new InformationMessage("Successfully loaded NullHeroFix.", SubModule.textColor));
		}

		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			base.OnGameStart(game, gameStarterObject);
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
