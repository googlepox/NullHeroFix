using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace NullHeroFix
{
	internal class NullHeroFixBehavior : CampaignBehaviorBase
	{

		private bool _isNull;
		private bool _isClanNull;
		private bool _isCultureNull;
		private bool _isNameNull;
		private bool _isFirstNameNull;
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.RemoveNullHeroes));
		}

		public override void SyncData(IDataStore dataStore)
		{
		}

		private void RemoveNullHeroes()
		{
			InformationManager.DisplayMessage(new InformationMessage("Checking for Null Heroes..."));
			foreach (Hero heroObj in Hero.AllAliveHeroes)
			{
				_isNull = IsNull(heroObj);
				_isClanNull = IsClanNull(heroObj);
				_isCultureNull= IsCultureNull(heroObj);
				_isNameNull = IsFirstNameNull(heroObj);
                _isFirstNameNull = IsFirstNameNull(heroObj);
                FixNullTraits(heroObj);
			}
		}

        private void FixNullTraits(Hero hero)
        {
            if (this._isNull)
            {
                InformationManager.DisplayMessage(new InformationMessage("A hero object is NULL! This is very bad and WILL cause crashes!"));
                InformationManager.DisplayMessage(new InformationMessage("Attempting to initialize the null hero..."));
                hero.Init();
            }
            if (this._isClanNull)
            {
                InformationManager.DisplayMessage(new InformationMessage("A hero has a null clan. Setting clan..."));
                foreach (Clan clanObj in Clan.All)
                {
                    if (clanObj.StringId == "neutral")
                    {
                        hero.Clan = clanObj;
                        CampaignEventDispatcher.Instance.OnHeroChangedClan(hero, clanObj);
                    }
                }
            }
            if (this._isCultureNull)
            {
                InformationManager.DisplayMessage(new InformationMessage("A hero has a null culture. Setting culture..."));
                foreach (Clan clanObj in Clan.All)
                {
                    if (clanObj.Culture.StringId == "neutral_culture")
                    {
                        hero.Culture = clanObj.Culture;
                    }
                }
            }
            if (this._isNameNull || this._isFirstNameNull)
            {
                InformationManager.DisplayMessage(new InformationMessage("A hero has a null name. Setting name..."));
                hero.SetName(new TextObject("NULL_HERO_NAME"), new TextObject("NULL_HERO_FIRST_NAME"));

            }
        }

		private bool IsNull(Hero hero)
		{
			if (hero == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        private bool IsClanNull(Hero hero)
        {
            if (hero.Clan == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsCultureNull(Hero hero)
        {
            if (hero.Culture == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsNameNull(Hero hero)
        {
            if (hero.Name == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsFirstNameNull(Hero hero)
        {
            if (hero.FirstName == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
