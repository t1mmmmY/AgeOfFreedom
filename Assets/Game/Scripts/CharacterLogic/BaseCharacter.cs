using UnityEngine;
using System.Collections;

//BaseCharacter hande human logic. Contain brain and inventory. 
//[System.Serializable]
public class BaseCharacter 
{
	BaseBrain _brain;

	public BaseBrain brain
	{
		get { return _brain; }
		private set { _brain = value; }
	}

//	public BaseBrain brain { get; private set; }
	public string characterID { get; private set; }

	public Team team { get; set; }


	public void InitBrain(BaseBrain brain)
	{
		this.brain = brain;
		this.characterID = brain.brainID;
//		this.brain.InitCharacter(this);
	}

	public static bool operator ==(BaseCharacter character1, BaseCharacter character2)
	{
		return character1.characterID == character2.characterID;
	}

	public static bool operator !=(BaseCharacter character1, BaseCharacter character2)
	{
		return !(character1 == character2);
	}
}
