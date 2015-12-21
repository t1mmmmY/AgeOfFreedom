using UnityEngine;
using System.Collections;
using System.Text;

//[System.Serializable]
public class BaseBrain
{
	[SerializeField] protected CharacterStats _stats;
	public Team team { get; protected set; }

	public CharacterStats stats
	{
		get { return _stats; }
	}

	protected virtual void CreateTeam()
	{
		team = new Team(this);
	}

	protected bool RecruitToTheTeam(NPCBrain otherCharacter)
	{
		if (otherCharacter.WantToJoin(team))
		{
			otherCharacter.Recruit(team);
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
