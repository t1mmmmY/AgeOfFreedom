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
		}


	}

	void OnDestroy()
	{
		character.onChangeTeam -= OnChangeTeam;
	}

//	public virtual void Init(BaseCharacter character)
//	{
//		this.character = character;
//		characterVisualID = this.character.characterID;
//
//		//Subscribe events here
//		this.character.brain.onChangeTeam += OnChangeTeam;
//	}

	public virtual void OnChangeTeam()
	{
	}
}
