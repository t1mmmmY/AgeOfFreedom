using UnityEngine;
using System.Collections;

public class TestGameController : MonoBehaviour 
{

	void OnDestroy()
	{
		BrainStorage.EndGame();
	}
}
