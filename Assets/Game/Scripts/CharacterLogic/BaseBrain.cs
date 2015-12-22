﻿using UnityEngine;
using System.Collections;
using System.Text;

//[System.Serializable]
public class BaseBrain
{
	protected CharacterStats _stats;

	public BaseCharacter character { get; protected set; }

	public string brainID { get; private set; }

	public System.Action onChangeTeam;


	public CharacterStats stats
	{
		get { return _stats; }
	}

	public BaseBrain()
	{
		brainID = System.Guid.NewGuid().ToString();
	}

	public void InitCharacter(BaseCharacter character)
	{
		this.character = character;
		this.character.InitBrain(this);
	}

	public virtual void CreateTeam()
	{
		character.team = new Team(character);
	}

	public bool RecruitToTheTeam(BaseCharacter otherCharacter)
	{
		if (otherCharacter.brain.WantToJoin(character.team))
		{
			otherCharacter.brain.Recruit(character.team);
			return true;
		}
		else
		{
			return false;
		}
	}

	public virtual bool WantToJoin(Team otherTeam)
	{
		return false;
	}

	public virtual void Recruit(Team otherTeam)
	{
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(stats.ToString());

		if (character.team != null)
		{
			sb.AppendLine();
			if (character.team.captain != this.character)
			{
				sb.AppendLine("Captain");
				sb.Append(character.team.captain.brain.stats.ToString());
			}
			else
			{
				sb.AppendLine("I am a captain!");
			}
		}

		return sb.ToString();
	}


}
