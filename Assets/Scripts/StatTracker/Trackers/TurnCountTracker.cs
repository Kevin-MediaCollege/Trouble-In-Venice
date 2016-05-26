using System;
using System.Collections.Generic;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Turn count tracker, tracks the number of turns the player needs.
	/// </summary>
	public class TurnCountTracker : ITracker<int>
	{
		private int turnCount;

		public void OnEnable()
		{
			GlobalEvents.AddListener<PlayerMovedEvent>(OnPlayerMovedEvent);
		}

		public void OnDisable()
		{
			GlobalEvents.RemoveListener<PlayerMovedEvent>(OnPlayerMovedEvent);
		}

		public int GetValue()
		{
			return turnCount;
		}

		object ITracker.GetValue()
		{
			return GetValue();
		}

		private void OnPlayerMovedEvent(PlayerMovedEvent evt)
		{
			turnCount++;
		}
	}		
}
