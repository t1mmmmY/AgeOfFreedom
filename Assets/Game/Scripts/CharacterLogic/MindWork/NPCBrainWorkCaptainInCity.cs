using UnityEngine;
using System.Collections;

public partial class NPCBrain : BaseBrain
{
	int failedRecruiting = 0;


	void DoWorkInCity()
	{
		bool doWorkInCity = false;

		doWorkInCity = RecruitTheTeam() ? true : doWorkInCity;
		doWorkInCity = BuySupplies() ? true : doWorkInCity;
		doWorkInCity = SellGoods() ? true : doWorkInCity;
		doWorkInCity = RepairShip() ? true : doWorkInCity;
		doWorkInCity = DismissTeam() ? true : doWorkInCity;
		doWorkInCity = BuyNewShip() ? true : doWorkInCity;
		doWorkInCity = BuyGoods() ? true : doWorkInCity;
		doWorkInCity = BuySomeItems() ? true : doWorkInCity;
		doWorkInCity = PlayGames() ? true : doWorkInCity;
		doWorkInCity = TakeARest() ? true : doWorkInCity;
		doWorkInCity = Teambuilding() ? true : doWorkInCity;
		doWorkInCity = GetLastGossip() ? true : doWorkInCity; //Last information
		doWorkInCity = RecruitOtherCaptains() ? true : doWorkInCity;

		//Go only if do all other work
		if (!doWorkInCity && DoIWantToGo())
		{
			OnTheBoad();
		}
	}


	bool RecruitTheTeam()
	{
		if (!DoIWantToRecruitTheTeam())
		{
			return false;
		}
		//		_RecruitTheTeam();
		Loom.QueueOnMainThread(_RecruitTheTeam);

		return true;
	}

	void _RecruitTheTeam()
	{
		BaseCharacter someCharacter = GetRandomCharacterInTavern();
		if (someCharacter != null)
		{
			if (RecruitToTheTeam(someCharacter))
			{
				//			Debug.Log(someCharacter.name + " has joined team " + name);
			}
			else
			{
				failedRecruiting++;
			}
		}
		else
		{
			//Nobody to recruit
			failedRecruiting++;
		}
	}

	bool BuySupplies()
	{
		//TODO
		return false;
	}

	bool SellGoods()
	{
		//TODO
		return false;
	}

	bool RepairShip()
	{
		//TODO
		return false;
	}

	bool DismissTeam()
	{
		//TODO
		return false;
	}

	bool BuyNewShip()
	{
		//TODO
		return false;
	}

	bool BuyGoods()
	{
		//TODO
		return false;
	}

	bool BuySomeItems()
	{
		//TODO
		return false;
	}

	bool PlayGames()
	{
		//TODO
		return false;
	}

	bool TakeARest()
	{
		//TODO
		return false;
	}

	bool Teambuilding()
	{
		//TODO
		return false;
	}

	bool GetLastGossip()
	{
		//TODO
		return false;
	}

	bool RecruitOtherCaptains()
	{
		//TODO
		return false;
	}

	bool DoIWantToGo()
	{
		//TODO
		return true;
	}

	bool OnTheBoad()
	{
		Loom.QueueOnMainThread(_OnTheBoard);

		return true;
	}

	void _OnTheBoard()
	{
		character.team.OnTheBoad();
	}


	bool DoIWantToRecruitTheTeam()
	{
		//Tried more times that can handle
		if (failedRecruiting <= stats.permanence)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
