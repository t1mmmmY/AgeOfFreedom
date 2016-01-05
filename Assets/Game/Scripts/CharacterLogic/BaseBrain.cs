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

	//If I want to recruit some other character
	protected bool RecruitToTheTeam(BaseCharacter otherCharacter)
	{
		if (otherCharacter.brain.WantToJoin(character.team))
		{
			otherCharacter.brain.BeingRecruited(character.team);
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

	protected virtual bool WantToJoin(Team otherTeam)
	{
		return false;
	}

	public virtual bool DoIWantMercy(BaseCharacter enemyCaptain)
	{
		return true;
	}

	protected virtual void BeingRecruited(Team otherTeam)
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

	public virtual void OnFinishFighting(BattleResult result)
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
