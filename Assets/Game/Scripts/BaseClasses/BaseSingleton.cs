using UnityEngine;
using System.Collections;

public class BaseSingleton<T> : MonoBehaviour where T : BaseSingleton<T>
{
	public static T Instance{ get; private set; }
	
	protected virtual void Awake()
	{
		Instance = this as T;
	}
	
	protected virtual void OnDestroy()
	{
		Instance = null;
	}
}
