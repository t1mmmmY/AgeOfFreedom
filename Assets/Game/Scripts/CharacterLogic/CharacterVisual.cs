using UnityEngine;
using System.Collections;

public class CharacterVisual : MonoBehaviour 
{
	public BaseCharacter character { get; private set; }
	string characterVisualID;


	void OnDestroy()
	{
		character.brain.onChangeTeam -= OnChangeTeam;
	}

	public virtual void Init(BaseCharacter character)
	{
		this.character = character;
		characterVisualID = this.character.characterID;

		//Subscribe events here
		this.character.brain.onChangeTeam += OnChangeTeam;
	}

	public virtual void OnChangeTeam()
	{
	}
}
