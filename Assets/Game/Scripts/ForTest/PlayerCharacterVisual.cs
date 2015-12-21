using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerCharacterVisual : CharacterVisual
{
	[SerializeField] private PlayerBrain brain;

	bool showStats = false;
	string statsLog = string.Empty;

	void Awake()
	{
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
		NPCBrain otherCharacter = Raycast();
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

	NPCBrain Raycast()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
		{
			NPCBrain otherCharacter = hit.collider.GetComponent<NPCCharacterVisual>().brain;
			return otherCharacter;
		}
		else
		{
			return null;
		}
	}

	void MakeAction(NPCBrain otherCharacter)
	{
		bool success = brain.RecruitToTheTeam(otherCharacter);
		if (!success)
		{
			Debug.Log("I don't want to join your team");
		}
	}

	void ShowStats(NPCBrain otherCharacter)
	{
		statsLog = otherCharacter.ToString();
		showStats = true;
	}


}
