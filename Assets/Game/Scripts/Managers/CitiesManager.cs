using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CitiesManager : BaseSingleton<CitiesManager> 
{
	[SerializeField] CityData[] allCitiesData;
	List<City> allCities;

	protected override void Awake()
	{
		allCities = new List<City>();
		base.Awake();
	}

	void Start()
	{
		foreach (CityData cityData in allCitiesData)
		{
			City city = new City(cityData);
			city.Init();
			cityData.cityVisualization.Init(city);
			allCities.Add(city);
		}
	}

	public City GetRandomCity(City currentCity = null)
	{
		bool available = false;
		System.Random rand = new System.Random();
		int number = 0;
		do 
		{
			number = rand.Next(0, allCities.Count);

			if (allCities[number] != currentCity)
			{
				available = true;
			}
				
		} while (!available);

		return allCities[number];
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}
}
