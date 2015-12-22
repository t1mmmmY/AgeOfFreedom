using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CharactersManager 
{
	public static List<BaseCharacter> allCharacters { get; private set; }

	static CharactersManager()
	{
		allCharacters = new List<BaseCharacter>();
	}

	public static BaseCharacter CreateCharacter()
	{
		BaseCharacter character = new BaseCharacter();
		NPCBrain brain = BrainStorage.CreateBrain(character);
		allCharacters.Add(character);
		return character;
	}

}
