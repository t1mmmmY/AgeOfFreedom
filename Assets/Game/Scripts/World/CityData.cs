using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[System.Serializable]
public class CityData
{
	[SerializeField] CityOnMap _cityVisualization;

	[Range(0, 10000)]
	[SerializeField] int _citizensCount;

	[SerializeField] Vector2 _position;

	public CityOnMap cityVisualization
	{
		get { return _cityVisualization; }
	}

	public int citizensCount
	{
		get { return _citizensCount; }
	}

	public Vector2 position
	{
		get 
		{
			return _position;
		}
	}
}
