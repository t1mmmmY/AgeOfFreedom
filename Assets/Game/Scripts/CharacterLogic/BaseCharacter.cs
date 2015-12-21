using UnityEngine;
using System.Collections;
using System.Text;

//[System.Serializable]
public class BaseCharacter 
{
	[SerializeField] protected CharacterStats _stats;
	protected Team _team;

	public CharacterStats stats
	{
		get { return _stats; }
	}

	public Team team
	{
		get { return _team; }
	}

	protected void CreateTeam()
	{
		_team = new Team(this);
	}

	protected bool RecruitToTheTeam(CharacterAI otherCharacter)
	{
		if (otherCharacter.WantToJoin(_team))
		{
			otherCharacter.Recruit(_team);
			return true;
		}
		else
		{
			return false;
		}
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(stats.ToString());

		if (team != null)
		{
			sb.AppendLine();
			if (team.captain != this)
			{
				sb.AppendLine("Captain");
				sb.Append(team.captain.stats.ToString());
			}
			else
			{
				sb.AppendLine("I am a captain!");
			}
		}

		return sb.ToString();
	}
}
