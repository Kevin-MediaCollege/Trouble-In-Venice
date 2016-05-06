using System.Collections;
using UnityEngine;
using System;

public class LevelUnlocker : IDependency
{
	private const string LEVEL_INDEX_PLAYERPREFS_KEY = "UnlockedLevelIndex";

	private int unlockedLevelIndex;

	public LevelUnlocker()
	{
		if(Application.isPlaying)
		{
			unlockedLevelIndex = PlayerPrefs.GetInt(LEVEL_INDEX_PLAYERPREFS_KEY, 0);
			Debug.Log("Unlocked level " + unlockedLevelIndex);
		}
	}

	public void Reset()
	{
		// Save PlayerPrefs
		PlayerPrefs.SetInt(LEVEL_INDEX_PLAYERPREFS_KEY, 0);
		PlayerPrefs.Save();

		unlockedLevelIndex = 0;

		Debug.Log("Reset level progress");
	}

	public bool Unlock(int _level, bool _unlockPrerequisites = false)
	{
		if(_level < 0)
		{
			throw new ArgumentException("The parameter \"_level\" cannot be less than zero");
		}

		if(!_unlockPrerequisites && !IsUnlocked(_level - 1))
		{
			Debug.LogWarning("Prerequisite for level " + _level + " has not been unlocked yet");
			return false;
		}

		if(IsUnlocked(_level))
		{
			Debug.LogWarning("Level " + _level + " has already been unlocked");
			return false;
		}

		Debug.Log("Unlocked level " + _level);

		// Save PlayerPrefs
		PlayerPrefs.SetInt(LEVEL_INDEX_PLAYERPREFS_KEY, _level);
		PlayerPrefs.Save();

		unlockedLevelIndex = _level;
		return true;
	}

	public bool IsUnlocked(int level)
	{
		return level <= unlockedLevelIndex;
	}
}