using UnityEngine;
using System.Collections;

public partial class NPCBrain : BaseBrain
{
	int failedRecruiting = 0;
	protected bool isMoving = false;

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

			doWorkInCity = RecruitTheTeam() ? true : doWorkInCity;
			doWorkInCity = BuySupplies() ? true : doWorkInCity;
			doWorkInCity = SellGoods() ? true : doWorkInCity;
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
		else
		{
			//TODO			
			//In the sea

			if (!isMoving)
			{
				MoveToOtherCity();
			}
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



	void MoveToOtherCity()
	{
		isMoving = true;
		Loom.QueueOnMainThread(_MoveToOtherCity);
	}

	void _MoveToOtherCity()
	{
		ShipTargetPoint targetPoint = new ShipTargetPoint();
		targetPoint.SetTargetCity(CitiesManager.Instance.GetRandomCity(character.location.GetLastCity()));

		character.team.ship.MoveTo(targetPoint);
	}


	public override void OnGetDestination()
	{
		isMoving = false;
		failedRecruiting = 0;
		
		base.OnGetDestination();
	}

}
