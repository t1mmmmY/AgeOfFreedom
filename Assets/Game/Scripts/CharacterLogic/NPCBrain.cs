using UnityEngine;
using System.Collections;
using System.Linq;

//[System.Serializable]
public class NPCBrain : BaseBrain
{
	bool alive = false;

	public void InitRandomBrain(BaseCharacter character)
	{
		_stats = new CharacterStats();
		_stats.InitRandomCharacter();
		InitCharacter(character);
		this.character.team = null;

		alive = true;
	}

	public void Think()
	{
		if (!alive)
		{
			return;
		}

		BrainWork();
	}

	public void StopThink()
	{
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
		character.team = otherTeam;
		otherTeam.Recruit(this.character);

		if (onChangeTeam != null)
		{
			Loom.QueueOnMainThread(onChangeTeam);
		}
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
		if (character.team.captain == this.character)
		{
			return false;
		}

		if (CheckIfWantToJoin(otherTeam))
		{
			int captainCharisma = otherTeam.captain.brain.stats.charisma;
			if (captainCharisma > character.team.captain.brain.stats.charisma)
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


	void OnDestroy()
	{
		alive = false;
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
			if (character.team.captain == this.character)
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

	void DoFreelancerWork()
	{
		if (DoIWantToCreateMyOwnTeam())
		{
//			Debug.Log("I decided to create my own team! " + name);
			CreateTeam();
		}
		else
		{
			DoNothing();
		}
	}

	void DoNothing()
	{
	}

	void DoCaptainWork()
	{
		Loom.QueueOnMainThread(RecruitTheTeam);
//		RecruitTheTeam();
	}

	void DoSailorWork()
	{
	}



	bool DoIWantToCreateMyOwnTeam()
	{
		System.Random rand = new System.Random();

		float randomValue = (float)((rand.NextDouble() / 2f + 0.5f) * (rand.NextDouble() * 100 + 1) * 10);

		if (randomValue < stats.charisma)
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
		BaseCharacter someCharacter = GetRandomCharacter();
		if (RecruitToTheTeam(someCharacter))
		{
//			Debug.Log(someCharacter.name + " has joined team " + name);
		}
	}

	BaseCharacter GetRandomCharacter()
	{
		var availableCharacters = 
			from someCharacter in CharactersManager.allCharacters
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
