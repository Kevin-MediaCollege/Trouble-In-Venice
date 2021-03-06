﻿using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Log a screen to Google Analytics.
	/// </summary>
	public class GoogleAnalyticsScreen : MonoBehaviour
	{
		[SerializeField] protected string screenName;

		protected GoogleAnalytics googleAnalytics;

		protected virtual void Awake()
		{
			googleAnalytics = Dependency.Get<GoogleAnalytics>();
		}

		protected virtual void OnEnable()
		{
			googleAnalytics.LogScreen(new AppViewHitBuilder().SetScreenName(screenName));
		}
	}
}
