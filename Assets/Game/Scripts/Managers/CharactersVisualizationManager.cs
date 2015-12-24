using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharactersVisualizationManager : BaseSingleton<CharactersVisualizationManager> 
{
	[SerializeField] NPCCharacterVisual characterPrefab;

	List<NPCCharacterVisual> allCharacters;

	protected override void Awake()
	{
		allCharacters = new List<NPCCharacterVisual>();

		base.Awake();
	}

	public NPCCharacterVisual CreateCharacter(BaseCharacter character, Rect rect)
	{
		GameObject go = GameObject.Instantiate<GameObject>(characterPrefab.gameObject);

		go.transform.parent = this.transform;
		go.transform.position = RandomPositionInRect(rect);
		go.name = character.name;

		NPCCharacterVisual characterVisual = go.GetComponent<NPCCharacterVisual>();
		characterVisual.Init(character);

		allCharacters.Add(characterVisual);

		return characterVisual;
	}

	Vector3 RandomPositionInRect(Rect rect)
	{
		return new Vector3(Random.Range(rect.xMin, rect.xMax),
			1,
			Random.Range(rect.yMin, rect.yMax));
	}
}
