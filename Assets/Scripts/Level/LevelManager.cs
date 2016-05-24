using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG;
using DG.Tweening;

public class LevelManager : Utils.ScriptableObjectSingleton<LevelManager> 
{
	#if UNITY_EDITOR
	[UnityEditor.MenuItem("Assets/Create/LevelManager")]
	private static void create()
	{
		CreateAsset ("LevelManager", "LevelManager", "LevelManager");
	}
	#endif

	public LevelData[] levels;

	public static LevelData[] getLevelList()
	{
		return Instance.levels;
	}
}

[System.Serializable]
public class LevelData
{
	public string levelName;
	public Sprite levelImage;
	public int maxStars;
}