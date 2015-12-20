using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamsManager : BaseSingleton<TeamsManager> 
{
	[SerializeField] List<Team> allTeams;

	public void AddTeam(Team newTeam)
	{
		allTeams.Add(newTeam);
	}
}
