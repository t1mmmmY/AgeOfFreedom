using UnityEngine;
using System.Collections;
using System.Linq;

[System.Serializable]
public class NPCBrain : BaseBrain
{
	bool alive = false;
//	int brainTick = 500;

	public System.Action onChangeTeam;

	public void InitRandomCharacter()
	{
		_stats = new CharacterStats();
		_stats.InitRandomCharacter();
		_team = null;

		alive = true;
	}

	public void Think()
	{
		BrainWork();
//		if (!consciousness.IsAlive)
//		{
//			consciousness.Start();
//		}
	}

	public void StopThink()
	{
//		if (consciousness.IsAlive)
//		{
//			consciousness.Abort();
//		}
	}

	public bool WantToJoin(Team otherTeam)
	{
		//If it is a free character
		if (_team == null)
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
		if (_team != null)
		{
			_team.Leave(this);
		}
		_team = otherTeam;
		otherTeam.Recruit(this);

		//TO DEL
		if (onChangeTeam != null)
		{
			Loom.QueueOnMainThread(onChangeTeam);
//			onChangeTeam(team);
		}

//		SendMessage("ChangeTeam", _team, SendMessageOptions.DontRequireReceiver);
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
		if (_team.captain == this)
		{
			return false;
		}

		if (CheckIfWantToJoin(otherTeam))
		{
			int captainCharisma = otherTeam.captain.stats.charisma;
			if (captainCharisma > _team.captain.stats.charisma)
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
//		StopCoroutine("BrainWork");
	}


	void BrainWork()
	{
//		do
//		{
//			Thread.Sleep(brainTick);
//			yield return new WaitForSeconds(brainTick);

			//Freelancer
			if (_team == null)
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

//		} while (alive);
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
		bool characterInTheTeam = false;

		var availableCharacters = 
			from character in CharactersManager.allCharacters
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
