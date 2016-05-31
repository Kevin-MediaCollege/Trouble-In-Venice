using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Proeve
{
	public class ChallengesSaver : IDependency
	{
		public void SaveStars(int levelId, int numStars)
		{
			PlayerPrefs.SetInt("Level_" + levelId + "_Stars", numStars);
		}

		public int GetNumStars(int levelId)
		{
			return PlayerPrefs.GetInt("Level_" + levelId + "_Stars");
		}
	}
}
