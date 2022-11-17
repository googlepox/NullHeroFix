using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace NullHeroFix
{
	internal class NullHeroFixBehavior : CampaignBehaviorBase
	{
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.RemoveNullHeroes));
		}

		public override void SyncData(IDataStore dataStore)
		{
		}

		private void RemoveNullHeroes()
		{
			var neutralClan = Clan.All.Where(c => c.StringId == "neutral").First();
            var neutralCulture = Clan.All.Where(c => c.Culture.StringId == "neutral_culture").First().Culture;

            foreach (Hero heroObj in Hero.AllAliveHeroes)
            {
                if (heroObj == null)
                    heroObj.Init();

                if (heroObj.Clan == null && !heroObj.IsNotable && !heroObj.IsWanderer)
                {
                    heroObj.Clan = neutralClan;
                }
                if (heroObj.Culture == null && !heroObj.IsNotable && !heroObj.IsWanderer)
                {
                    heroObj.Culture = neutralCulture;
                }
                FixExistingNotablesAndWanderers(heroObj);
            }

        }
        private void FixExistingNotablesAndWanderers(Hero hero)
        {
            if (hero != null && hero.Clan != null)
            {
                if ((hero.IsNotable || hero.IsWanderer) && hero.Clan.StringId == "neutral")
                {
                    hero.Clan = null;
                }
            }
        }
    }
}
