using UnityEngine;
using System.Collections;

public class CityOnMap : MonoBehaviour 
{
	[Range(0, 10000)]
	[SerializeField] private int citizensCount = 2000;


	City cityLogic;


	void Start()
	{
		cityLogic = new City(citizensCount, this);
	}

	public NPCCharacterVisual PlaceOnMap(BaseCharacter character)
	{
		Rect rect = GetRect();
		NPCCharacterVisual characterVisual = CharactersVisualizationManager.Instance.CreateCharacter(character, rect);

		return characterVisual;
	}

	public Rect GetRect()
	{
		return new Rect(transform.position.x - transform.localScale.x / 2, transform.position.z - transform.localScale.z / 2,
			transform.localScale.x, transform.localScale.z);
	}

}
