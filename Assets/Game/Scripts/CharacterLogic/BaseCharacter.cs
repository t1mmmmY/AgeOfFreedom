using UnityEngine;
using System.Collections;

//BaseCharacter hande human logic. Contain brain and inventory. 
//[System.Serializable]
public class BaseCharacter 
{
	BaseBrain _brain;
	public string name { get; private set; }

	public BaseBrain brain
	{
		get { return _brain; }
		private set { _brain = value; }
	}

	public string characterID { get; private set; }

	public Team team { get; set; }
	public Location location { get; private set; }

	public BaseCharacter()
	{
		name = "Sailor " + System.DateTime.UtcNow.Millisecond.ToString();
		location = new Location();
	}

	public void InitBrain(BaseBrain brain)
	{
		this.brain = brain;
		this.characterID = brain.brainID;
	}

	public void EnterTheTavern(Tavern tavern)
	{
		location.EnterTheTavern(tavern);
	}

	public void LeaveTheTavern()
	{
		location.LeaveTheTavern();
	}


//	public static bool operator ==(BaseCharacter character1, BaseCharacter character2)
//	{
//		if (character1 == null && character2 == null)
//		{
//			return true;
//		}
//		else if (character1 == null || character2 == null)
//		{
//			return false;
//		}
//		else
//		{
//			return character1.characterID == character2.characterID;
//		}
//	}
//
//	public static bool operator !=(BaseCharacter character1, BaseCharacter character2)
//	{
//		return !(character1 == character2);
//	}
}
