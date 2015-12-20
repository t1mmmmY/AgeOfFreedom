using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Team
{
	[SerializeField] BaseCharacter _captain;
	[SerializeField] int countSailors = 0;
	[SerializeField] List<BaseCharacter> _characters;

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
		teamColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}

	public void Recruit(CharacterAI character)
	{
		AddCharacter(character);
	}

	public void Leave(CharacterAI character)
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


	public void Update()
	{
	}

}
