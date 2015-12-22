using UnityEngine;
using System.Collections;

public class TempCharacterGenerator : MonoBehaviour 
{
	[SerializeField] Rect mapRect;
	[SerializeField] int countCharacters = 50;
	public NPCCharacterVisual characterPrefab;

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
			BaseCharacter npcCharacter = CharactersManager.CreateCharacter();
			CreateCharacter(i, npcCharacter);
		}
	}

	NPCCharacterVisual CreateCharacter(int number, BaseCharacter character)
	{
		GameObject go = GameObject.Instantiate<GameObject>(characterPrefab.gameObject);
		go.transform.parent = this.transform;
		go.transform.position = RandomPositionInRect();
		go.name = "Sailor " + number.ToString();

		NPCCharacterVisual characterVisual = go.GetComponent<NPCCharacterVisual>();
		characterVisual.Init(character);

		return characterVisual;
	}

	Vector3 RandomPositionInRect()
	{
		return new Vector3(Random.Range(mapRect.xMin, mapRect.xMax),
							1,
							Random.Range(mapRect.yMin, mapRect.yMax));
	}
}
