using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerCharacterVisual : CharacterVisual
{
//	public BaseCharacter character;
//
////	[SerializeField] private PlayerBrain brain;
//
//	bool showStats = false;
//	string statsLog = string.Empty;
//
//	bool inTheBody = false;
//
//	void Awake()
//	{
//	}
//
//	void Start()
//	{
//		character.brain.CreateTeam();
//	}
//
//	void OnGUI()
//	{
//		if (inTheBody)
//		{
//			if (showStats)
//			{
//				GUI.color = Color.green;
//				GUILayout.Label(statsLog);
//			}
//		}
//	}
//
//	void Update()
//	{
//		if (inTheBody)
//		{
//			BaseCharacter otherCharacter = Raycast();
//			if (otherCharacter != null)
//			{
//				ShowStats(otherCharacter);
//
//				if (CrossPlatformInputManager.GetButtonDown("Action"))
//				{
//					MakeAction(otherCharacter);
//				}
//			}
//			else
//			{
//				showStats = false;
//			}
//		}
//	}
//
//	BaseCharacter Raycast()
//	{
//		RaycastHit hit;
//		if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
//		{
//			BaseCharacter otherCharacter = hit.collider.GetComponent<NPCCharacterVisual>().character;
//			return otherCharacter;
//		}
//		else
//		{
//			return null;
//		}
//	}
//
//	void MakeAction(BaseCharacter otherCharacter)
//	{
//		bool success = character.brain.RecruitToTheTeam(otherCharacter);
//		if (!success)
//		{
//			Debug.Log("I don't want to join your team");
//		}
//	}
//
//	void ShowStats(BaseCharacter otherCharacter)
//	{
//		statsLog = otherCharacter.brain.ToString();
//		showStats = true;
//	}


}
