using UnityEngine;
using System.Collections;

public partial class NPCBrain : BaseBrain
{
	int failedRecruiting = 0;

	public override void CreateTeam()
	{
		base.CreateTeam();

		if (character.location.inCity)
		{
			character.BuyShip(character.location.GetCity().shipyard.GetRandomShipOrCreate());
		}
	}

	void DoCaptainWork()
	{
		if (character.location.inCity)
		{
			//In the city
			bool doWorkInCity = false;

			doWorkInCity = RecruitTheTeam();
			doWorkInCity = BuySupplies();
			doWorkInCity = SellGoods();
			doWorkInCity = BuyNewShip();
			doWorkInCity = BuyGoods();
			doWorkInCity = BuySomeItems();
			doWorkInCity = PlayGames();
			doWorkInCity = TakeARest();
			doWorkInCity = Teambuilding();
			doWorkInCity = GetLastGossip(); //Last information
			doWorkInCity = RecruitOtherCaptains();

			//Go only if do all other work
			if (!doWorkInCity && DoIWantToGo())
			{
				OnTheBoad();
			}
		}
		else
		{
			//In the sea
		}
	}

	bool RecruitTheTeam()
	{
		if (!DoIWantToRecruitTheTeam())
		{
			return false;
		}

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
		character.team.OnTheBoad();

		return true;
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



	void MoveToOtherCity()
	{
		//Team should go on board firs

//		//Set destination
//		ShipTargetPoint targetPoint = new ShipTargetPoint();
//		targetPoint.SetTargetCity();
//
//		//Move
//		character.team.ship.MoveTo(targetPoint);
	}

}
