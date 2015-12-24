using UnityEngine;
using System.Collections;

public abstract class Visualisation : MonoBehaviour 
{
	public Logic logic { get; private set; }
	string visualID;


	public virtual void Init(Logic logic)
	{
		this.logic = logic;
		visualID = this.logic.ID;
	}

}
