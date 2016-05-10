using System.Collections;
using UnityEditor;
using Utils;

namespace Proeve
{
	/// <summary>
	/// A collection of project specific editor utilities.
	/// </summary>
	public static class EditorUtils
	{
		[MenuItem("Tools/Reset Unlocked Levels")]
		private static void ResetUnlockedLevels()
		{
			Dependency.Get<LevelUnlocker>().Reset();
		}
	}
}
