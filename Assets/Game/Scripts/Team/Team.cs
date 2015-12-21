using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Team
{
	[SerializeField] BaseBrain _captain;
	[SerializeField] int countSailors = 0;
	[SerializeField] List<BaseBrain> _characters;

	//TO DEL
	[SerializeField] public Color teamColor;

	public BaseBrain captain
	{
		get { return _captain; }
	}

	public List<BaseBrain> characters
	{
		get { return _characters; }
	}

	public Team(BaseBrain teamCaptain)
	{
		_characters = new List<BaseBrain>();
		_captain = teamCaptain;
		AddCharacter(_captain);

		TeamsManager.Instance.AddTeam(this);

		//TO DEL
		System.Random rand = new System.Random();

		teamColor = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
	}

	public void Recruit(NPCBrain character)
	{
		AddCharacter(character);
	}

	public void Leave(NPCBrain character)
	{
		RemoveCharacter(character);
	}


	void AddCharacter(BaseBrain character)
	{
		_characters.Add(character);
		countSailors++;
	}

	void RemoveCharacter(BaseBrain character)
	{
		if (_characters.Contains(character))
		{
			_characters.Remove(character);
			countSailors--;
		}
	}


}
