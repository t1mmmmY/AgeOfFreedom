using UnityEngine;
using System.Collections;

public class CharacterVisual : Visualisation 
{
	protected BaseCharacter character;

	public override void Init(Logic characterLogic)
	{
		base.Init(characterLogic);

		if (this.logic is BaseCharacter)
		{
			character = (BaseCharacter)this.logic;

			//Subscribe events here
			character.onChangeTeam += OnChangeTeam;
			character.onTheBoard += OnTheBoard;
			character.onShipChangeLocation += OnShipChangeLocation;
			character.onShipGetDestination += OnShipGetDestination;
			character.onKilled += OnKilled;
		}
		else
		{
			Debug.LogError("Logic is not a BaseCharacter logic!");
		}
	}

	void OnDestroy()
	{
		character.onChangeTeam -= OnChangeTeam;
		character.onTheBoard -= OnTheBoard;
		character.onShipChangeLocation -= OnShipChangeLocation;
		character.onShipGetDestination -= OnShipGetDestination;
		character.onKilled -= OnKilled;
	}

	protected virtual void OnChangeTeam(BaseCharacter character, Team team)
	{
	}

	protected virtual void OnTheBoard()
	{
	}

	protected virtual void OnShipChangeLocation()
	{
	}

	protected virtual void OnShipGetDestination()
	{
	}

	protected virtual void OnKilled()
	{
	}
}
