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
			//Create ship
			BaseShip ship = CreateShip();
			ship.Init();
			ShipsManager.AddShip(ship);
			return ship;
		}
		else
		{
			//Get random ship
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
