using UnityEngine;
using System.Collections.Generic;

namespace Proeve
{
	public class SwipeHandle
	{
		public Vector2 StartPosition { get { return positions[0]; } }
		public Vector2 StartPositionViewport { get { return new Vector2(StartPosition.x / Screen.width, StartPosition.y / Screen.height); } }

		public Vector2 LastPosition { get { return positions[positions.Count - 1]; } }
		public Vector2 LastPositionViewport { get { return new Vector2(LastPosition.x / Screen.width, LastPosition.y / Screen.height); } }

		public Vector2 PreviousPosition { get { return positions[Mathf.Max(0, positions.Count - 2)]; } }
		public Vector2 PreviousPositionViewport { get { return new Vector2(PreviousPosition.x / Screen.width, PreviousPosition.y / Screen.height); } }

		public Vector2 Velocity { private set; get; }

		public float Duration { get { return Time.time - startTime; } }

		public bool IsComplete { set; get; }

		public bool IsConsumed { set; get; }

		private List<Vector2> positions;
		private float startTime;

		public SwipeHandle()
		{
			positions = new List<Vector2>();

			startTime = Time.time;
		}

		public void AddPosition(Vector2 _position)
		{
			positions.Add(_position);

			// Update velocity
			Velocity = LastPosition - PreviousPosition;
		}

		public static implicit operator bool(SwipeHandle _handle)
		{
			return !_handle.IsComplete && !_handle.IsConsumed;
		}
	}
}
