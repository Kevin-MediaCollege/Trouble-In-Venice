using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Initializer for the Google Analytics dependency.
	/// </summary>
	public class GoogleAnalyticsInitializer : MonoBehaviour
	{
		protected void Awake()
		{
			Dependency.Get<GoogleAnalytics>();
		}
	}
}
