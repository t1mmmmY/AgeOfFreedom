using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tavern 
{
	List<BaseCharacter> characters;

	public Tavern()
	{
		characters = new List<BaseCharacter>();
	}

	public void AddCharacter(BaseCharacter character)
	{
		characters.Add(character);
		character.EnterTheTavern(this);
	}

	public void RemoveCharacter(BaseCharacter character)
	{
		characters.Remove(character);
		character.LeaveTheTavern();
	}

	public List<BaseCharacter> GetAllCharacters()
	{
		return characters;
	}
}
