using Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Proeve
{
	public class ChallengesManager : MonoBehaviour
	{
		public static ChallengesManager instance;
		public int stars = 0;
		public ChallengeBase[] challenges;

		void Awake () 
		{
			instance = this;
			GlobalEvents.AddListener<LevelCompletedEvent>(OnLevelComplete);
		}

		public void OnLevelComplete(LevelCompletedEvent _event)
		{
			stars = 0;
			for(int i = 0; i < challenges.Length && i < 3; i++)
			{
				stars += challenges[i].getStar() ? 1 : 0;
			}

			stars = Mathf.Clamp (stars, 0, 3);
			int levelID = Convert.ToInt32(SceneManager.GetActiveScene().name.Split('_')[1]);
			Dependency.Get<ChallengesSaver> ().SaveStars (levelID, stars);
		}
			
		protected void OnDisable()
		{
			instance = null;
		}
	}
}
