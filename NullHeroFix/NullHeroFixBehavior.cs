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

                if (heroObj.Clan == null && !heroObj.IsNotable)
                {
                    heroObj.Clan = neutralClan;
                }
                if (heroObj.Culture == null && !heroObj.IsNotable)
                {
                    heroObj.Culture = neutralCulture;
                }
                FixExistingNotables(heroObj);
            }

        }
        private void FixExistingNotables(Hero hero)
        {
            if (hero != null && hero.Clan != null)
            {
                if (hero.IsNotable && hero.Clan.StringId == "neutral")
                {
                    hero.Clan = null;
                }
            }
        }
    }
}
