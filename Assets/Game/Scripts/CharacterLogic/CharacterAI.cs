using UnityEngine;
using System.Collections;
using System.Linq;

public class CharacterAI : BaseCharacter
{
	bool alive = false;
	float brainTick = 0.5f;

	public void InitRandomCharacter()
	{
		_stats = new CharacterStats();
		_stats.InitRandomCharacter();
		_team = null;

		alive = true;
		StartCoroutine("BrainWork");
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
		SendMessage("ChangeTeam", _team, SendMessageOptions.DontRequireReceiver);
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
		StopCoroutine("BrainWork");
	}


	IEnumerator BrainWork()
	{
		do
		{
			yield return new WaitForSeconds(brainTick);

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

		} while (alive);
	}

	void DoFreelancerWork()
	{
		if (DoIWantToCreateMyOwnTeam())
		{
			Debug.Log("I decided to create my own team! " + name);
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
		RecruitTheTeam();
	}

	void DoSailorWork()
	{
	}



	bool DoIWantToCreateMyOwnTeam()
	{
		float randomValue = Random.Range(0.5f, 1f) * Random.Range(1f, 100f) * 10;

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
		CharacterAI someCharacter = GetRandomCharacter();
		if (RecruitToTheTeam(someCharacter))
		{
			Debug.Log(someCharacter.name + " has joined team " + name);
		}
	}

	CharacterAI GetRandomCharacter()
	{
		bool characterInTheTeam = false;

		var availableCharacters = 
			from character in CharactersManager.allCharacters
			where !team.characters.Contains(character)
			select character;
									
		if (availableCharacters.Count() > 0)
		{
			CharacterAI randomCharacter = (CharacterAI)availableCharacters.ToArray()[Random.Range(0, availableCharacters.Count())];
			return randomCharacter;
		}
		else
		{
			return null;
		}
	}
}
