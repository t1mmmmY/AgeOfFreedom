﻿using UnityEngine;
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

	public static bool KillCharacter(BaseCharacter character)
	{
		if (allCharacters.Contains(character))
		{
			allCharacters.Remove(character);
			return true;
		}
		else
		{
			return false;
		}
	}


	#region Global characters events

	static void OnBuyShip(BaseCharacter character, BaseShip ship)
	{
		if (!character.location.inCity)
		{
			Debug.LogError("This is strange");
		}

//		if (TestGameController.Instance.showFleets)
//		{
//			FleetsVisualizationManager.Instance.CreateFleet(ship, character.location.GetCity().GetRect());
//		}
	}

	static void OnChangeTeam(BaseCharacter character, Team team)
	{
	}

	#endregion
}
