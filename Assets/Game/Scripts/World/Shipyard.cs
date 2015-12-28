using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shipyard
{
	List<BaseShip> allShips;

	public Shipyard()
	{
		allShips = new List<BaseShip>();
	}

	public BaseShip GetRandomShipOrCreate()
	{
		if (allShips.Count == 0)
		{
			BaseShip ship = CreateShip();
			ship.Init();
			return ship;
		}
		else
		{
			System.Random rand = new System.Random();
			BaseShip ship = allShips[rand.Next(0, allShips.Count)];
			allShips.Remove(ship);
			return ship;
		}
	}

	BaseShip CreateShip()
	{
		return new BaseShip();
	}

}
