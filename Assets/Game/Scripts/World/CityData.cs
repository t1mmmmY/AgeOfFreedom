using UnityEngine;
using System.Collections;

[System.Serializable]
public class CityData
{
	[SerializeField] CityOnMap _cityVisualization;

	[Range(0, 10000)]
	[SerializeField] int _citizensCount;

	public CityOnMap cityVisualization
	{
		get { return _cityVisualization; }
	}

	public int citizensCount
	{
		get { return _citizensCount; }
	}
}
