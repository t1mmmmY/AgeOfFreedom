using UnityEngine;
using System.Collections;

public class NPCCharacterVisual : CharacterVisual 
{
	MeshRenderer meshRenderer;


	void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	public override void Init(Logic characterLogic)
	{
		base.Init(characterLogic);
	}


	public override void OnChangeTeam()
	{
		meshRenderer.material.color = character.team.teamColor;

		base.OnChangeTeam();
	}
}
