using UnityEngine;
using System.Collections;

public class NPCCharacterVisual : CharacterVisual 
{
	private BaseCharacter _character;
	MeshRenderer meshRenderer;
	string characterVisualID;

	public BaseCharacter character
	{
		get { return _character; }
		private set { _character = value; }
	}

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
