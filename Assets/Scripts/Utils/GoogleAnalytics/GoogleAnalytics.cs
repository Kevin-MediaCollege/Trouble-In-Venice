using UnityEngine;

namespace Utils
{
	/// <summary>
	/// The Google Analytics dependency.
	/// </summary>
    public class GoogleAnalytics : IDependency
    {
#if !UNITY_EDITOR
		private GoogleAnalyticsV4 ga;
#endif

		public GoogleAnalytics()
        {
#if !UNITY_EDITOR
			GameObject obj = new GameObject("Google Analytics");

			ga = obj.AddComponent<GoogleAnalyticsV4>();
			ga.androidTrackingCode = GoogleAnalyticsData.AndroidTrackingCode;
			ga.IOSTrackingCode = GoogleAnalyticsData.iOSTrackingCode;
  			ga.otherTrackingCode = GoogleAnalyticsData.OtherTrackingCode;
			ga.productName = GoogleAnalyticsData.ProductName;
			ga.bundleIdentifier = GoogleAnalyticsData.BundleIdentifier;
			ga.bundleVersion = GoogleAnalyticsData.BundleVersion;
			ga.dispatchPeriod = GoogleAnalyticsData.DispatchPeriod;
			ga.sampleFrequency = GoogleAnalyticsData.SampleFrequency;
			ga.logLevel = GoogleAnalyticsData.LogLevel;
			ga.anonymizeIP = GoogleAnalyticsData.AnonymizeIP;
  			ga.UncaughtExceptionReporting = GoogleAnalyticsData.UncaughtExceptionReporting;
			ga.sendLaunchEvent = GoogleAnalyticsData.SendLaunchEvent;
			ga.dryRun = GoogleAnalyticsData.DryRun;
			ga.sessionTimeout = GoogleAnalyticsData.SessionTimeout;
			ga.enableAdId = GoogleAnalyticsData.EnableAdID;
			ga.Initialize();
#endif
		}

		public void Dispose()
        {
#if !UNITY_EDITOR
			Object.Destroy(ga.gameObject);
#endif
		}

		/// <summary>
		/// Log a Google Analytics event.
		/// <see cref="GoogleAnalyticsV4.LogEvent(EventHitBuilder)"/>
		/// </summary>
		/// <param name="builder">The event hit builder.</param>
		public void LogEvent(EventHitBuilder builder)
        {
			Debug.Log("[GoogleAnalytics] Log Event: Category(" + builder.GetEventCategory() + ") Action(" + builder.GetEventAction() + ") Value(" + builder.GetEventValue() + ")");

#if !UNITY_EDITOR
			ga.LogEvent(builder);
#endif
		}

		/// <summary>
		/// Log a screen.
		/// <see cref="GoogleAnalyticsV4.LogScreen(AppViewHitBuilder)"/>
		/// </summary>
		/// <param name="builder">The app view hit builder.</param>
		public void LogScreen(AppViewHitBuilder builder)
		{
			Debug.Log("[GoogleAnalytics] Log Screen: ScreenName(" + builder.GetScreenName() + ")");

#if !UNITY_EDITOR
			ga.LogScreen(builder);
#endif
		}
	}
}