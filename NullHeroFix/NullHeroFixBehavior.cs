using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;

namespace NullHeroFix
{
	internal class NullHeroFixBehavior : CampaignBehaviorBase
	{
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, (RemoveNullHeroes));
            CampaignEvents.OnGameEarlyLoadedEvent.AddNonSerializedListener(this, (RemoveNullItemsTroopsLoad));
        }

		public override void SyncData(IDataStore dataStore)
		{
		}

        private void RemoveNullItemsTroopsLoad(CampaignGameStarter obj)
        {
            SubModule.nullObjects = 0;
            foreach (MobileParty party in MobileParty.All)
            {
                foreach (TroopRosterElement troop in party.MemberRoster.GetTroopRoster())
                {
                    if (troop.Character.Culture == null)
                    {
                        party.MemberRoster.RemoveTroop(troop.Character, troop.Number);
                    }
                }

                foreach (ItemRosterElement item in party.ItemRoster)
                {
                    if (item.EquipmentElement.Item == null || item.EquipmentElement.Item.Name.Contains("null_butter_"))
                    {
                        party.ItemRoster.Remove(item);
                    }
                }
            }
        }

		private void RemoveNullHeroes()
		{
			Clan neutralClan = Clan.All.Where(c => c.StringId == "neutral").First();
            CultureObject neutralCulture = Clan.All.Where(c => c.Culture.StringId == "neutral_culture").First().Culture;

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
