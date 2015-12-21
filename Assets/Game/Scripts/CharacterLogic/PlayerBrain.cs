using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerBrain : BaseBrain 
{

	public void CreateTeam()
	{
		base.CreateTeam();
	}

	public bool RecruitToTheTeam(NPCBrain otherCharacter)
	{
		return base.RecruitToTheTeam(otherCharacter);
	}

}
