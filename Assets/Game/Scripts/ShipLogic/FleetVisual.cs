using UnityEngine;
using System.Collections;

public class FleetVisual : Visualisation 
{
	Fleet fleet;

	public override void Init(Logic logic)
	{
		base.Init(logic);

		fleet = (Fleet)logic;
		fleet.onChangeLocation += OnChangeLocation;
		fleet.onLostFleet += OnLostFleet;
	}

	void OnDestroy()
	{
		fleet.onChangeLocation -= OnChangeLocation;
		fleet.onLostFleet -= OnLostFleet;
	}

	void OnChangeLocation()
	{
		Vector3 position = transform.position;
		position = new Vector3(fleet.location.GetPosition().x, position.y, fleet.location.GetPosition().y);
		transform.position = position;
	}

	void OnLostFleet()
	{
		DestroySelf();
	}
}
