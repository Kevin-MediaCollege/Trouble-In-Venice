using Utils;

namespace Proeve
{
	/// <summary>
	/// Rose pickup tracker, tracks whether or not the player has picked up the rose in a level.
	/// </summary>
	public class RosePickupTracker : ITracker<bool>
	{
		private bool hasRose;

		public void OnEnable()
		{
			GlobalEvents.AddListener<RosePickedUpEvent>(OnRosePickedUpEvent);
		}

		public void OnDisable()
		{
			GlobalEvents.RemoveListener<RosePickedUpEvent>(OnRosePickedUpEvent);
		}

		public bool GetValue()
		{
			return hasRose;
		}

		object ITracker.GetValue()
		{
			return GetValue();
		}

		private void OnRosePickedUpEvent(RosePickedUpEvent evt)
		{
			hasRose = true;
		}
	}
}
