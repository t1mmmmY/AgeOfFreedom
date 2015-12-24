using UnityEngine;
using System.Collections;

public class CityOnMap : MonoBehaviour 
{
	[Range(0, 10000)]
	[SerializeField] private int citizensCount = 2000;


	City cityLogic;
	[SerializeField] NPCCharacterVisual characterPrefab;


	void Start()
	{
		cityLogic = new City(citizensCount, this);
	}

	public NPCCharacterVisual PlaceOnMap(BaseCharacter character)
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
