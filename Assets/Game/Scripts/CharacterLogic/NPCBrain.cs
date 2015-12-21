using UnityEngine;
using System.Collections;
using System.Linq;

[System.Serializable]
public class NPCBrain : BaseBrain
{
	bool alive = false;

	public System.Action onChangeTeam;

	public void InitRandomCharacter()
	{
		_stats = new CharacterStats();
		_stats.InitRandomCharacter();
		team = null;

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

	public bool WantToJoin(Team otherTeam)
	{
		//If it is a free character
		if (team == null)
		{
			return CheckIfWantToJoin(otherTeam);
		}
		//If character already in other team
		else
		{
			return CheckIfWantToChangeTeam(otherTeam);
		}
	}

	public void Recruit(Team otherTeam)
	{
		if (team != null)
		{
			team.Leave(this);
		}
		team = otherTeam;
		otherTeam.Recruit(this);

		if (onChangeTeam != null)
		{
			Loom.QueueOnMainThread(onChangeTeam);
		}
	}

	bool CheckIfWantToJoin(Team otherTeam)
	{
		int captainReputation = otherTeam.captain.stats.reputation;
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
		if (team.captain == this)
		{
			return false;
		}

		if (CheckIfWantToJoin(otherTeam))
		{
			int captainCharisma = otherTeam.captain.stats.charisma;
			if (captainCharisma > team.captain.stats.charisma)
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
		if (team == null)
		{
			DoFreelancerWork();

		}
		//In the team
		else
		{
			//I am a captain!
			if (team.captain == this)
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
		NPCBrain someCharacter = GetRandomCharacter();
		if (RecruitToTheTeam(someCharacter))
		{
//			Debug.Log(someCharacter.name + " has joined team " + name);
		}
	}

	NPCBrain GetRandomCharacter()
	{
		var availableCharacters = 
			from character in BrainStorage.allBrains
			where !team.characters.Contains(character)
			select character;
									
		if (availableCharacters.Count() > 0)
		{
			System.Random rand = new System.Random();
			NPCBrain randomCharacter = (NPCBrain)availableCharacters.ToArray()[rand.Next(0, availableCharacters.Count())];
			return randomCharacter;
		}
		else
		{
			return null;
		}
	}
}
