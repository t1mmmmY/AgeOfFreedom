using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterPlayer : BaseCharacter 
{

	public void CreateTeam()
	{
		base.CreateTeam();
	}

	public bool RecruitToTheTeam(CharacterAI otherCharacter)
	{
		return base.RecruitToTheTeam(otherCharacter);
	}

}
