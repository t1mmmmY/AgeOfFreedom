using UnityEngine;
using System.Collections;
using System.Text;

//[System.Serializable]
public class BaseBrain : Logic
{
	public CharacterStats stats { get; protected set; }

	public BaseCharacter character { get; protected set; }


	public System.Action onChangeTeam;


	public BaseBrain()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public void InitCharacter(BaseCharacter character)
	{
		this.character = character;
		this.character.InitBrain(this);

	}

	public virtual void CreateTeam()
	{
		character.BecomeACaptain();

		if (onChangeTeam != null)
		{
			onChangeTeam();
		}
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

	public bool IsCaptain()
	{
		return character.isCaptain;
//		return character.team.captain == this.character ? true : false;
	}

	public virtual bool WantToJoin(Team otherTeam)
	{
		return false;
	}

	public virtual void Recruit(Team otherTeam)
	{
		if (onChangeTeam != null)
		{
			onChangeTeam();
//			Loom.QueueOnMainThread(onChangeTeam);
		}
	}

	public virtual void OnGetDestination()
	{
	}

	public virtual void OnChangePosition()
	{
	}

	public virtual void OnFighting()
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
