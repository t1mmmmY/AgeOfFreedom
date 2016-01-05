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


	protected override void OnChangeTeam(BaseCharacter character, Team team)
	{
		meshRenderer.material.color = team.teamColor;

		base.OnChangeTeam(character, team);
	}

	protected override void OnTheBoard()
	{
		ChangePosition();
		
		base.OnTheBoard();
	}

	protected override void OnShipChangeLocation()
	{
		ChangePosition();
		base.OnShipChangeLocation();
	}

	protected override void OnShipGetDestination()
	{
		if (character.location.inCity)
		{
			transform.position = character.location.GetCity().GetRandomPositionInCity();
		}

		base.OnShipGetDestination();
	}

	void ChangePosition()
	{
		Vector2 position2d = character.location.GetPosition();
		transform.position = new Vector3(position2d.x, transform.position.y, position2d.y);
	}

	protected override void OnKilled()
	{
		Destroy(this.gameObject);
		base.OnKilled();
	}
}
