using UnityEngine;
using System.Collections;

public class NPCCharacterVisual : CharacterVisual 
{
	public BaseCharacter character { get; private set; }
	MeshRenderer meshRenderer;
	string characterVisualID;


	void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void OnDestroy()
	{
		character.brain.onChangeTeam -= OnChangeTeam;
	}

	public void Init(BaseCharacter npcCharacter)
	{
		character = npcCharacter;
		characterVisualID = character.characterID;

		//Subscribe events here
		character.brain.onChangeTeam += OnChangeTeam;
	}

	public void OnChangeTeam()
	{
		meshRenderer.material.color = character.team.teamColor;
	}
}
