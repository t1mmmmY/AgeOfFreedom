using UnityEngine;
using System.Collections;

public class City : MonoBehaviour 
{
	[Range(0, 10000)]
	[SerializeField] private int _citizensCount;
	[SerializeField] NPCCharacterVisual characterPrefab;


	float sailorsPercent = 0.02f;
	Tavern tavern;

	void Start()
	{
		tavern = new Tavern();
		//TO DEL
		for (int i = 0; i < _citizensCount * sailorsPercent; i++)
		{
			BaseCharacter sailor = CreateRandomSailor();
			tavern.AddCharacter(sailor);
		}
	}

	BaseCharacter CreateRandomSailor()
	{
		BaseCharacter npcCharacter = CharactersManager.CreateCharacter();
		PlaceOnMap(npcCharacter);
		return npcCharacter;
	}

	NPCCharacterVisual PlaceOnMap(BaseCharacter character)
	{
		GameObject go = GameObject.Instantiate<GameObject>(characterPrefab.gameObject);
		go.transform.parent = this.transform;
		go.transform.position = RandomPositionInRect();
		go.name = character.name;

		NPCCharacterVisual characterVisual = go.GetComponent<NPCCharacterVisual>();
		characterVisual.Init(character);

		return characterVisual;
	}

	Vector3 RandomPositionInRect()
	{
		return new Vector3(Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2),
			1,
			Random.Range(transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2));
	}
}
