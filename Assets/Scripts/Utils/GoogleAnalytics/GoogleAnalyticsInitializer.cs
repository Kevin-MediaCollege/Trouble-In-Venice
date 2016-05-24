using UnityEngine;

namespace Utils
{
	public class GoogleAnalyticsInitializer : MonoBehaviour
	{
		protected void Awake()
		{
			Dependency.Get<GoogleAnalytics>();
		}
	}
}
