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
		if (character.location.inTavern)
		{
			if (DoIWantToRecruitTheTeam())
			{
				//Recruit the team
				Loom.QueueOnMainThread(RecruitTheTeam);
			}
			else
			{
				//Do something else
				MoveToOtherCity();
			}
		}
		//		RecruitTheTeam();
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

	void RecruitTheTeam()
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

	void MoveToOtherCity()
	{
	}

}
