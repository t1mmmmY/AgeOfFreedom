using UnityEngine;
using System.Collections;

public class TempCharacterGenerator : MonoBehaviour 
{
	[SerializeField] Rect mapRect;
	[SerializeField] int countCharacters = 50;
	[SerializeField] NPCCharacterVisual characterPrefab;

	void Start()
	{
		CreateAllCharacters();
	}

	void OnDestroy()
	{
		BrainStorage.EndGame();
	}

	void CreateAllCharacters()
	{
		for (int i = 0; i < countCharacters; i++)
		{
			NPCBrain npcBrain = BrainStorage.CreateBrain();
//			CreateCharacter(i, npcBrain);
//			BrainStorage.AddCharacter(npcBrain);
		}
	}

	NPCCharacterVisual CreateCharacter(int number, NPCBrain brain)
	{
		GameObject go = GameObject.Instantiate<GameObject>(characterPrefab.gameObject);
		go.transform.parent = this.transform;
		go.transform.position = RandomPositionInRect();
		go.name = "Sailor " + number.ToString();

		NPCCharacterVisual character = go.GetComponent<NPCCharacterVisual>();
		character.Init(brain);

		return character;
	}

	Vector3 RandomPositionInRect()
	{
		return new Vector3(Random.Range(mapRect.xMin, mapRect.xMax),
							1,
							Random.Range(mapRect.yMin, mapRect.yMax));
	}
}
