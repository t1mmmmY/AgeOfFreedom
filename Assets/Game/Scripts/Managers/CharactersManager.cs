using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CharactersManager
{
	public static List<NPCBrain> allCharacters { get; private set; }

	private static int brainTick = 500;
	private static bool needToThink = true;

	static CharactersManager()
	{
		allCharacters = new List<NPCBrain>();
		Loom.RunAsync(ConsciousnessLoop);
	}

	public static void AddCharacter(NPCBrain character)
	{
		allCharacters.Add(character);
	}

	public static void EndGame()
	{
		needToThink = false;
	}

	private static void ConsciousnessLoop()
	{
		do
		{
			if (allCharacters != null && allCharacters.Count > 0)
			{
				int count = allCharacters.Count;
				try
				{
					foreach (NPCBrain character in allCharacters)
					{
						character.Think();
						System.Threading.Thread.Sleep(brainTick / allCharacters.Count);

						//Need to break. Otherwise I will get exception
						if (count != allCharacters.Count)
						{
							break;
						}
					}
				}
				catch (System.Exception ex)
				{
					Debug.LogException(ex);
				}
			}

		} while (needToThink);
	}
}
