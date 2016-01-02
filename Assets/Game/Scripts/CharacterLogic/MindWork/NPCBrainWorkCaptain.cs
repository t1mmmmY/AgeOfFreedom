using UnityEngine;
using System.Collections;

public partial class NPCBrain : BaseBrain
{

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
			DoWorkInCity();

		}
		else
		{
			//In the sea
			DoWorkInSea();
		}
	}


}
