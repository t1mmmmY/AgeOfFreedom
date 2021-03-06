﻿using UnityEngine;
using System.Collections;

public class TestGameController : BaseSingleton<TestGameController> 
{
	public bool showFleets = true;
	public bool showCharacters = true;
	public bool speedUp = false;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void OnDestroy()
	{
		BrainStorage.EndGame();

		base.OnDestroy();
	}
}
