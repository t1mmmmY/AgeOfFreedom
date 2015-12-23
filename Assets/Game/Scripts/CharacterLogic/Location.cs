using UnityEngine;
using System.Collections;

//There is much work to do
public class Location
{
	Tavern tavern;

	public Location()
	{
		tavern = null;
	}

	public bool inTavern
	{
		get { return tavern == null ? false : true; }
	}

	public Tavern GetTavern()
	{
		return tavern;
	}

	public void EnterTheTavern(Tavern tavern)
	{
		this.tavern = tavern;
	}

	public void LeaveTheTavern()
	{
		this.tavern = null;
	}
}
