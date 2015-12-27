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
			City city = new City(cityData.citizensCount, cityData.cityVisualization);
			city.Init();
			cityData.cityVisualization.Init(city);
			allCities.Add(city);
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}
}
