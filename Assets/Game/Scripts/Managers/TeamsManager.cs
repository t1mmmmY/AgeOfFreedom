using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamsManager : BaseSingleton<TeamsManager> 
{
	[SerializeField] private List<Team> allTeams;

	public bool AddTeam(Team newTeam)
	{
		if (!allTeams.Contains(newTeam))
		{
			allTeams.Add(newTeam);
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool RemoveTeam(Team team)
	{
		if (allTeams.Contains(team))
		{
			allTeams.Remove(team);
			return true;
		}
		else
		{
			return false;
		}
	}
}
