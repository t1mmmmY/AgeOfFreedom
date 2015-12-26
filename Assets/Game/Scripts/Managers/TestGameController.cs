using UnityEngine;
using System.Collections;

public class TestGameController : BaseSingleton<TestGameController> 
{
	public bool showShips = true;
	public bool showCharacters = true;

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
