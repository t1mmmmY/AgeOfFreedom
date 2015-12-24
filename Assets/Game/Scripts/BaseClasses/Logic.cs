using UnityEngine;
using System.Collections;

public abstract class Logic
{
	public string ID { get; protected set; }

//	public System.Action onChangeTeam;


	public virtual void Init()
	{
		ID = System.Guid.NewGuid().ToString();
	}

	public virtual void Init(string id)
	{
		ID = id;
	}

//	public virtual void InitVisualization(Visualisation visualization)
//	{
//	}
}
