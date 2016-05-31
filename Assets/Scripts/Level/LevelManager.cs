using System;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Level data.
	/// </summary>
	[Serializable]
	public struct LevelData
	{
		/// <summary>
		/// The name of the level.
		/// </summary>
		public string levelName;

		/// <summary>
		/// The image of the level.
		/// </summary>
		public Sprite levelImage;

		/// <summary>
		/// The maximum amount of stars the player can earn in this level.
		/// </summary>
		public int maxStars;
	}

	/// <summary>
	/// Level manager asset.
	/// </summary>
	public class LevelManager : ScriptableObjectSingleton<LevelManager>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem("Assets/Create/LevelManager")]
		private static void create()
		{
			CreateAsset("LevelManager", "LevelManager", "LevelManager");
		}
#endif

		/// <summary>
		/// Get all available levels.
		/// </summary>
		public static LevelData[] Levels
		{
			get
			{
				return Instance.levels;
			}
		}

		/// <summary>
		/// Get all available levels.
		/// </summary>
		public static int GetLevelIDFromName(string _levelName)
		{
			LevelData[] levels = Levels;
			int l = levels.Length;

			for(int i = 0; i < l; i++)
			{
				if(levels[i].levelName == _levelName)
				{
					return i;
				}
			}

			return 0;
		}

		/// <summary>
		/// Get level by name
		/// </summary>
		public static LevelData GetLevelFromName(string _levelName)
		{
			LevelData[] levels = Levels;
			int l = levels.Length;

			for(int i = 0; i < l; i++)
			{
				if(levels[i].levelName == _levelName)
				{
					return levels[i];
				}
			}
			return levels[0];
		}

		[SerializeField] private LevelData[] levels;
	}
}