using UnityEngine;
using System.Collections;

public class NPCCharacterVisual : CharacterVisual 
{
	[SerializeField] private NPCBrain _brain;
	MeshRenderer meshRenderer;
	string characterID;

	public NPCBrain brain
	{
		get { return _brain; }
		private set { _brain = value; }
	}

	void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void OnDestroy()
	{
		brain.onChangeTeam -= OnChangeTeam;
	}

	public void Init(NPCBrain npcBrain)
	{
		brain = npcBrain;
		characterID = brain.brainID;

		//Subscribe events here
		brain.onChangeTeam += OnChangeTeam;

		brain.InitRandomCharacter();
	}

	public void OnChangeTeam()
	{
		meshRenderer.material.color = brain.team.teamColor;
	}
}
