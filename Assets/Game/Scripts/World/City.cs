﻿using UnityEngine;
using System.Collections;

public class City : Logic
{
	private int citizensCount;

	float sailorsPercent = 0.02f;


	public Tavern tavern { get; private set; }
	public Shipyard shipyard { get; private set; }

	private CityOnMap cityOnMap;


	public City(int citizensCount, CityOnMap cityOnMap = null)
	{
		this.citizensCount = citizensCount;
		tavern = new Tavern();
		shipyard = new Shipyard();

		this.cityOnMap = cityOnMap;

		//TO DEL
		for (int i = 0; i < citizensCount * sailorsPercent; i++)
		{
			CreateRandomSailor();
		}
	}

	public override void Init()
	{
		
		base.Init();
	}


	BaseCharacter CreateRandomSailor()
	{
		BaseCharacter npcCharacter = CharactersManager.CreateCharacter();
		npcCharacter.EnterTheCity(this);
		tavern.AddCharacter(npcCharacter);

		if (cityOnMap != null)
		{
			if (TestGameController.Instance.showCharacters)
			{
				cityOnMap.PlaceCharacterOnMap(npcCharacter);
			}
		}

		return npcCharacter;
	}

	public Rect GetRect()
	{
		if (cityOnMap != null)
		{
			return cityOnMap.GetRect();
		}
		else 
		{
			return new Rect();
		}
	}
}
