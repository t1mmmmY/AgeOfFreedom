using UnityEngine;
using System.Collections;

public class NPCCharacterVisual : CharacterVisual 
{
	[SerializeField] private NPCBrain _brain;
	MeshRenderer meshRenderer;

	public NPCBrain brain
	{
		get { return _brain; }
		private set { _brain = value; }
	}

	void Awake()
	{
		brain = new NPCBrain();
		brain.onChangeTeam += OnChangeTeam;
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void OnDestroy()
	{
		brain.onChangeTeam -= OnChangeTeam;
	}

	public void Init()
	{
		brain.InitRandomCharacter();
	}

	public void OnChangeTeam()
	{
		meshRenderer.material.color = brain.team.teamColor;
	}
}
