using UnityEngine;
using System.Collections;

public class CityOnMap : Visualisation 
{
	City cityLogic;
	public Vector2 position { get; private set; }

	public override void Init(Logic logic)
	{
		cityLogic = (City)logic;
		position = new Vector2(transform.position.x, transform.position.z);

		base.Init(logic);
	}


	public NPCCharacterVisual PlaceCharacterOnMap(BaseCharacter character)
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
