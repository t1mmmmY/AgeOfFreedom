using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerBrain : BaseBrain 
{

	public new void CreateTeam()
	{
		base.CreateTeam();
	}


	public new bool RecruitToTheTeam(NPCBrain otherCharacter)
	{
		return base.RecruitToTheTeam(otherCharacter);
	}

}
