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
		character.Init();

		character.onBuyShip += OnBuyShip;
		character.onChangeTeam += OnChangeTeam;

		NPCBrain brain = BrainStorage.CreateBrain(character);
		allCharacters.Add(character);
		return character;
	}


	#region Global characters events

	static void OnBuyShip(BaseCharacter character, BaseShip ship)
	{
		if (!character.location.inCity)
		{
			Debug.LogError("This is strange");
		}
		ShipsVisualizationManager.Instance.CreateShip(ship, character.location.GetCity().GetRect());
	}

	static void OnChangeTeam(BaseCharacter character, Team team)
	{
	}

	#endregion
}
