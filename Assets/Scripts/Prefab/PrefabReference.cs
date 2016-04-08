using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabReference : ScriptableObjectSingleton<PrefabReference>
{
	[Serializable]
	public struct Prefab
	{
		[SerializeField] public GameObject prefab;
		[SerializeField] public int id;
	}
	
	public static IEnumerable<Prefab> Prefabs
	{
		get
		{
			return Instance.prefabs;
		}
	}

	public static int NumPrefabs
	{
		get
		{
			return Instance.prefabs.Length;
		}
	}

	[SerializeField] private Prefab[] prefabs;

	public static Prefab GetPrefabById(int id)
	{
		foreach(Prefab prefab in Instance.prefabs)
		{
			if(prefab.id == id)
			{
				return prefab;
			}
		}

		return default(Prefab);
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Assets/Create/Prefab Reference")]
	private static void Create()
	{
		CreateAsset("Create Prefab Reference", "Prefab Reference", "Create a new Prefab Reference");
	}
#endif
}