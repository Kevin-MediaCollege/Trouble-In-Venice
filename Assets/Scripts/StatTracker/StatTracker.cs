using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Proeve
{
	/// <summary>
	/// In-game statistics tracker
	/// </summary>
	public class StatTracker : MonoBehaviour
	{
		private static List<ITracker> trackers;

		protected void Awake()
		{
			trackers = new List<ITracker>();
			trackers.Add(new TurnCountTracker());
		}

		protected void OnEnable()
		{
			foreach(ITracker tracker in trackers)
			{
				tracker.OnEnable();
			}
		}

		protected void OnDisable()
		{
			foreach(ITracker tracker in trackers)
			{
				tracker.OnDisable();
			}
		}

		/// <summary>
		/// Get the instance of a tracker.
		/// </summary>
		/// <typeparam name="T">The type of the tracker, must implement <see cref="ITracker"/>.</typeparam>
		/// <returns>The instance of <code>T</code>.</returns>
		public static T GetTracker<T>() where T : ITracker
		{
			foreach(ITracker tracker in trackers)
			{
				if(tracker.GetType() == typeof(T))
				{
					return (T)tracker;
				}
			}

			return default(T);
		}
	}
}
