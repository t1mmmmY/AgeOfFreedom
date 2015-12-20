using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterAI))]
public class TestCharacterVisual : MonoBehaviour 
{
	CharacterAI character;
	MeshRenderer meshRenderer;

	void Awake()
	{
		character = GetComponent<CharacterAI>();
		if (character == null)
		{
			Debug.LogError("Can't find character!");
		}
		meshRenderer = GetComponent<MeshRenderer>();
	}

	public void Init()
	{
		character.InitRandomCharacter();
	}

	public void ChangeTeam(Team team)
	{
		meshRenderer.material.color = team.teamColor;
	}
}
