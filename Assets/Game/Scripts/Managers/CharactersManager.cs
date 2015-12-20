using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CharactersManager
{
	public static List<CharacterAI> allCharacters { get; private set; }


	static CharactersManager()
	{
		allCharacters = new List<CharacterAI>();
	}

	public static void AddCharacter(CharacterAI character)
	{
		allCharacters.Add(character);
	}
}
