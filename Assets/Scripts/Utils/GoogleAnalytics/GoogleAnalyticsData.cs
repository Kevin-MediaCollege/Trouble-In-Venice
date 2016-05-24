using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Utils
{
    public class GoogleAnalyticsData : ScriptableObjectSingleton<GoogleAnalyticsData>
    {
        [Tooltip("The tracking code to be used for Android. Example value: UAXXXXY.")]
        [SerializeField] private string androidTrackingCode;

        [Tooltip("The tracking code to be used for iOS. Example value: UAXXXXY.")]
        [SerializeField] private string IOSTrackingCode;

        [Tooltip("The tracking code to be used for platforms other than Android and iOS. Example value: UAXXXXY.")]
        [SerializeField] private string otherTrackingCode;

        [Tooltip("The application name. This value should be modified in the Unity Player Settings.")]
        [SerializeField] private string productName;

        [Tooltip("The application identifier. Example value: com.company.app.")]
        [SerializeField] private string bundleIdentifier;

        [Tooltip("The application version. Example value: 1.2")]
        [SerializeField] private string bundleVersion;

        [RangedTooltip("The dispatch period in seconds. Only required for Android and iOS.", 0, 3600)]
        [SerializeField] private int dispatchPeriod = 5;

        [RangedTooltip("The sample rate to use. Only required for Android and iOS.", 0, 100)]
        [SerializeField] private int sampleFrequency = 100;

        [Tooltip("The log level. Default is WARNING.")]
        [SerializeField] private GoogleAnalyticsV4.DebugMode logLevel = GoogleAnalyticsV4.DebugMode.WARNING;

        [Tooltip("If checked, the IP address of the sender will be anonymized.")]
        [SerializeField] private bool anonymizeIP = false;

        [Tooltip("Automatically report uncaught exceptions.")]
        [SerializeField] private bool uncaughtExceptionReporting = false;

        [Tooltip("Automatically send a launch event when the game starts up.")]
        [SerializeField] private bool sendLaunchEvent = false;

        [Tooltip("If checked, hits will not be dispatched. Use for testing.")]
        [SerializeField] private bool dryRun = false;

        [Tooltip("The amount of time in seconds your application can stay in the background before the session is ended. Default is 30 minutes (1800 seconds). A value of 1 will disable session management.")]
        [SerializeField] private int sessionTimeout = 1800;

		[AdvertiserOptIn]
		[SerializeField] private bool enableAdId = false;

		/// <summary>
		/// Get the Android tracking code.
		/// </summary>
		public static string AndroidTrackingCode
        {
            get
            {
                return Instance.androidTrackingCode;
            }
        }

        /// <summary>
        /// Get the iOS tracking code.
        /// </summary>
        public static string iOSTrackingCode
        {
            get
            {
                return Instance.IOSTrackingCode;
            }
        }

        /// <summary>
        /// Get the other tracking code.
        /// </summary>
        public static string OtherTrackingCode
        {
            get
            {
                return Instance.otherTrackingCode;
            }
        }

        /// <summary>
        /// Get the product name.
        /// </summary>
        public static string ProductName
        {
            get
            {
                return Instance.productName;
            }
        }

        /// <summary>
        /// Get the bundle indentifier.
        /// </summary>
        public static string BundleIdentifier
        {
            get
            {
                return Instance.bundleIdentifier;
            }
        }

        /// <summary>
        /// Get the bundle version.
        /// </summary>
        public static string BundleVersion
        {
			set
			{
				Instance.bundleVersion = value;

#if UNITY_EDITOR
				EditorUtility.SetDirty(Instance);
#endif
			}
            get
            {
                return Instance.bundleVersion;
            }
        }

        /// <summary>
        /// Get the dispatch period.
        /// </summary>
        public static int DispatchPeriod
        {
            get
            {
                return Instance.dispatchPeriod;
            }
        }

        /// <summary>
        /// Get the sample frequency.
        /// </summary>
        public static int SampleFrequency
        {
            get
            {
                return Instance.sampleFrequency;
            }
        }

        /// <summary>
        /// Get the logging level.
        /// </summary>
        public static GoogleAnalyticsV4.DebugMode LogLevel
        {
            get
            {
                return Instance.logLevel;
            }
        }

        /// <summary>
        /// Whether or not to anonymize the users IP address.
        /// </summary>
        public static bool AnonymizeIP
        {
            get
            {
                return Instance.anonymizeIP;
            }
        }

        /// <summary>
        /// Wheter or not to report uncaught exceptions.
        /// </summary>
        public static bool UncaughtExceptionReporting
        {
            get
            {
                return Instance.uncaughtExceptionReporting;
            }
        }

        /// <summary>
        /// Whether or not to send a launch event.
        /// </summary>
        public static bool SendLaunchEvent
        {
            get
            {
                return Instance.sendLaunchEvent;
            }
        }

        /// <summary>
        /// Whether or not this is a dry run.
        /// </summary>
        public static bool DryRun
        {
            get
            {
                return Instance.dryRun;
            }
        }

        /// <summary>
        /// The session timeout in seconds.
        /// </summary>
        public static int SessionTimeout
        {
            get
            {
                return Instance.sessionTimeout;
            }
        }

		/// <summary>
		/// Whether or not to enable the ad ID.
		/// </summary>
		public static bool EnableAdID
		{
			get
			{
				return Instance.enableAdId;
			}
		}

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Google Analytics Data")]
        private static void CreateAsset()
        {
            CreateAsset("Create GoogleAnalyticsData", "GoogleAnalyticsData", "Create a new GoogleAnalyticsData asset.");
        }
#endif
	}
}
