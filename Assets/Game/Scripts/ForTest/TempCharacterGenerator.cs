using UnityEngine;
using System.Collections;

public class TempCharacterGenerator : MonoBehaviour 
{
	[SerializeField] Rect mapRect;
	[SerializeField] int countCharacters = 50;
	[SerializeField] TestCharacterVisual characterPrefab;

	void Start()
	{
		CreateAllCharacters();
	}

	void CreateAllCharacters()
	{
		for (int i = 0; i < countCharacters; i++)
		{
			CharacterAI character = CreateCharacter(i);
			CharactersManager.AddCharacter(character);
		}
	}

	CharacterAI CreateCharacter(int number)
	{
		GameObject go = GameObject.Instantiate<GameObject>(characterPrefab.gameObject);
		go.transform.parent = this.transform;
		go.transform.position = RandomPositionInRect();
		go.name = "Sailor " + number.ToString();

		go.GetComponent<TestCharacterVisual>().Init();

		return go.GetComponent<CharacterAI>();
	}

	Vector3 RandomPositionInRect()
	{
		return new Vector3(Random.Range(mapRect.xMin, mapRect.xMax),
							1,
							Random.Range(mapRect.yMin, mapRect.yMax));
	}
}
