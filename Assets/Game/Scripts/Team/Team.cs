using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Team
{
	BaseCharacter _captain;
	[SerializeField] int countSailors = 0;
	List<BaseCharacter> _characters;


	//TO DEL
	[SerializeField] public Color teamColor;

	public BaseCharacter captain
	{
		get { return _captain; }
	}

	public List<BaseCharacter> characters
	{
		get { return _characters; }
	}

	public Team(BaseCharacter teamCaptain)
	{
		_characters = new List<BaseCharacter>();
		_captain = teamCaptain;
		AddCharacter(_captain);

		TeamsManager.Instance.AddTeam(this);

		//TO DEL
		System.Random rand = new System.Random();

		teamColor = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
	}

	public void Recruit(BaseCharacter character)
	{
		AddCharacter(character);
	}

	public void Leave(BaseCharacter character)
	{
		RemoveCharacter(character);
	}


	void AddCharacter(BaseCharacter character)
	{
		_characters.Add(character);
		countSailors++;
	}

	void RemoveCharacter(BaseCharacter character)
	{
		if (_characters.Contains(character))
		{
			_characters.Remove(character);
			countSailors--;
		}
	}


}
