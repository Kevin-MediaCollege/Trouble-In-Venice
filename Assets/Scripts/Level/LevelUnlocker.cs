using UnityEngine;
using System;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The level unlocker dependency, manages the unlocking of levels.
	/// </summary>
	public class LevelUnlocker : IDependency
	{
		private const string LEVEL_INDEX_PLAYERPREFS_KEY = "UnlockedLevelIndex";

		private int unlockedLevelIndex;

		/// <summary>
		/// Create the level unlocker.
		/// </summary>
		public LevelUnlocker()
		{
			if(Application.isPlaying)
			{
				unlockedLevelIndex = PlayerPrefs.GetInt(LEVEL_INDEX_PLAYERPREFS_KEY, 0);
				Debug.Log("Unlocked level " + unlockedLevelIndex);
			}
		}

		/// <summary>
		/// Reset the unlocked progress to 0.
		/// </summary>
		public void Reset()
		{
			// Save PlayerPrefs
			PlayerPrefs.SetInt(LEVEL_INDEX_PLAYERPREFS_KEY, 0);
			PlayerPrefs.Save();

			unlockedLevelIndex = 0;

			Debug.Log("Reset level progress");
		}

		/// <summary>
		/// Unlock a level.
		/// </summary>
		/// <remarks>
		/// If <paramref name="_unlockPrerequisites"/> is set to false,
		/// and <paramref name="_level"/> is higher than the current unlocked level + 1, it won't do anyting.
		/// </remarks>
		/// <param name="_level">The level index to unlock.</param>
		/// <param name="_unlockPrerequisites">Whether or not to unlock all levels before <paramref name="_level"/> as well.</param>
		/// <returns>Whether or not the level unlock was successful.</returns>
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

		/// <summary>
		/// Check whether or not the specified level has been unlocked.
		/// </summary>
		/// <param name="_level">The level index.</param>
		/// <returns>Whether or not the specified level has been unlocked.</returns>
		public bool IsUnlocked(int _level)
		{
			return _level <= unlockedLevelIndex;
		}
	}
}
