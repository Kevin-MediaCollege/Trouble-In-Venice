using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Proeve
{
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
