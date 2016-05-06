using System.Collections;
using UnityEditor;

public static class EditorUtils
{
	[MenuItem("Tools/Reset Unlocked Levels")]
	private static void ResetUnlockedLevels()
	{
		Dependency.Get<LevelUnlocker>().Reset();
	}
}