using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	public int LevelIndex { private set; get; }

	public bool IsUnlocked
	{
		get
		{
			if(levelUnlocker == null)
			{
				levelUnlocker = Dependency.Get<LevelUnlocker>();
			}

			return levelUnlocker.IsUnlocked(LevelIndex);
		}
	}

	private LevelUnlocker levelUnlocker;

	protected void Awake()
	{
		string[] levelName = gameObject.name.Split('_');

		if(levelName.Length > 1)
		{
			LevelIndex = int.Parse(levelName[levelName.Length - 1]);
		}
	}
}
