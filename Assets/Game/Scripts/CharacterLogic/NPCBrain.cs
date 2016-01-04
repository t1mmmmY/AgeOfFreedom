using UnityEngine;
using System.Collections;
using System.Linq;

//[System.Serializable]
public partial class NPCBrain : BaseBrain
{
	bool active = false;

	public override void Init()
	{
		base.Init();
	}

	public void InitRandomBrain(BaseCharacter character)
	{
		stats = new CharacterStats();
		stats.InitRandomCharacter();
		InitCharacter(character);
		this.character.ChangeTeam(null);
//		this.character.team = null;

		active = true;
		failedRecruiting = 0;
	}

	public void Think()
	{
		if (!active)
		{
			return;
		}

		BrainWork();
	}

	public void StopThink()
	{
		active = false;
	}

	public void ResumeThink()
	{
//		Debug.Log("ResumeThink");
		active = true;
	}

	public override bool WantToJoin(Team otherTeam)
	{
		//If it is a free character
		if (character.team == null)
		{
			return CheckIfWantToJoin(otherTeam);
		}
		//If character already in other team
		else
		{
			return CheckIfWantToChangeTeam(otherTeam);
		}
	}

	public override void Recruit(Team otherTeam)
	{
		if (character.team != null)
		{
			character.team.Leave(this.character);
		}
		otherTeam.Recruit(this.character);

		base.Recruit(otherTeam);
	}

	bool CheckIfWantToJoin(Team otherTeam)
	{
		int captainReputation = otherTeam.captain.brain.stats.reputation;
		if (captainReputation >= stats.reputation - 3 && captainReputation <= stats.reputation + 3)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool CheckIfWantToChangeTeam(Team otherTeam)
	{
		if (IsCaptain())
		{
			return false;
		}

		if (CheckIfWantToJoin(otherTeam))
		{
			int captainCharisma = otherTeam.captain.brain.stats.charisma;
			BaseCharacter currentCaptain = character.team.captain;
			if (currentCaptain != null && captainCharisma > currentCaptain.brain.stats.charisma)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}


	void BrainWork()
	{
		//Freelancer
		if (character.team == null)
		{
			DoFreelancerWork();
		}
		//In the team
		else
		{
			//I am a captain!
			if (IsCaptain())
			{
				DoCaptainWork();
			}
			//I am a sailor
			else
			{
				DoSailorWork();
			}
		}
	}


	void DoNothing()
	{
	}


	void DoSailorWork()
	{
	}




	BaseCharacter GetRandomCharacterInTavern()
	{
		var availableCharacters = 
			from someCharacter in character.location.GetTavern().GetAllCharacters()
				where (someCharacter.team == null || !someCharacter.team.characters.Contains(character))
			select someCharacter;
									
		if (availableCharacters.Count() > 0)
		{
			System.Random rand = new System.Random();
			BaseCharacter randomCharacter = (BaseCharacter)availableCharacters.ToArray()[rand.Next(0, availableCharacters.Count())];
			return randomCharacter;
		}
		else
		{
			return null;
		}
	}
}
