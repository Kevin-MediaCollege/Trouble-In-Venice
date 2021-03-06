﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The Google Analytics tracker for levels.
	/// </summary>
	public class GoogleAnalyticsLevel : MonoBehaviour
	{
		private List<GATracker> trackers;

		protected void Awake()
		{
			// Get the Google Analytics dependency
			GoogleAnalytics googleAnalytics = Dependency.Get<GoogleAnalytics>();

			// Get the level index
			string[] parts = SceneManager.GetActiveScene().name.Split('_');
			int levelIndex = int.Parse(parts[parts.Length - 1]);

			// Send a Google Analytics screen event
			googleAnalytics.LogScreen(new AppViewHitBuilder().SetScreenName(SceneManager.GetActiveScene().name));

			// Add trackers
			trackers = new List<GATracker>();
			trackers.Add(new GACompletionTracker(googleAnalytics, levelIndex));
			trackers.Add(new GALevelTracker(googleAnalytics, levelIndex));
			trackers.Add(new GAPickupTracker(googleAnalytics, levelIndex));
			trackers.Add(new GATurnCountTracker(googleAnalytics, levelIndex));
		}

		protected void OnEnable()
		{
			// Enable trackers
			foreach(GATracker tracker in trackers)
			{
				tracker.OnEnable();
			}
		}

		protected void OnDisable()
		{
			// Disable trackers
			foreach(GATracker tracker in trackers)
			{
				tracker.OnDisable();
			}
		}
	}
}
