using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Team
{
	public BaseCharacter captain { get; private set; }
	[SerializeField] int countSailors = 0;
	public List<BaseCharacter> characters { get; private set; }
	public BaseShip ship { get; private set; }
//	public Location location { get; private set; }

	//TO DEL
	[SerializeField] public Color teamColor;


	public Team(BaseCharacter teamCaptain)
	{
		characters = new List<BaseCharacter>();
		captain = teamCaptain;
		AddCharacter(captain);

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

	public bool SetShip(BaseShip ship)
	{
		//Set ship if this possible
		this.ship = ship;
		ship.ChangeTeam(this);
		return true;
	}

	public void OnTheBoad()
	{
//		location.LeaveTheCity();

		foreach (BaseCharacter character in characters)
		{
			character.OnTheBoard();
		}
	}

	void AddCharacter(BaseCharacter character)
	{
		characters.Add(character);
		countSailors++;
	}

	void RemoveCharacter(BaseCharacter character)
	{
		if (characters.Contains(character))
		{
			characters.Remove(character);
			countSailors--;
		}
	}

}
