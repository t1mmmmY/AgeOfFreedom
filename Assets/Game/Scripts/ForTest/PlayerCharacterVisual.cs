using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerCharacterVisual : CharacterVisual
{
	[SerializeField] private CharacterPlayer brain;

	bool showStats = false;
	string statsLog = string.Empty;

	void Awake()
	{
//		brain = new CharacterPlayer();
	}

	void Start()
	{
		brain.CreateTeam();
	}

	void OnGUI()
	{
		if (showStats)
		{
			GUI.color = Color.green;
			GUILayout.Label(statsLog);
		}
	}

	void Update()
	{
		CharacterAI otherCharacter = Raycast();
		if (otherCharacter != null)
		{
			ShowStats(otherCharacter);

			if (CrossPlatformInputManager.GetButtonDown("Action"))
			{
				MakeAction(otherCharacter);
			}
		}
		else
		{
			showStats = false;
		}
	}

	CharacterAI Raycast()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
		{
			CharacterAI otherCharacter = hit.collider.GetComponent<NPCCharacterVisual>().brain;
			return otherCharacter;
		}
		else
		{
			return null;
		}
	}

	void MakeAction(CharacterAI otherCharacter)
	{
		bool success = brain.RecruitToTheTeam(otherCharacter);
		if (!success)
		{
			Debug.Log("I don't want to join your team");
		}
	}

	void ShowStats(CharacterAI otherCharacter)
	{
		statsLog = otherCharacter.ToString();
		showStats = true;
	}


}
