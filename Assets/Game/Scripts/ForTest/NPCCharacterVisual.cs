using UnityEngine;
using System.Collections;

public class NPCCharacterVisual : CharacterVisual 
{
	[SerializeField] private CharacterAI _brain;
	MeshRenderer meshRenderer;

	public CharacterAI brain
	{
		get { return _brain; }
		private set { _brain = value; }
	}

	void Awake()
	{
		brain = new CharacterAI();
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
