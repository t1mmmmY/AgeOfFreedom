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


	public override void OnChangeTeam(BaseCharacter character, Team team)
	{
		meshRenderer.material.color = team.teamColor;

		base.OnChangeTeam(character, team);
	}
}
