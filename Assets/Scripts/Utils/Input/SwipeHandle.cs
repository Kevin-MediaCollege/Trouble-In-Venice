using UnityEngine;
using System.Collections.Generic;

namespace Proeve
{
	/// <summary>
	/// A swipe handle.
	/// </summary>
	public class SwipeHandle
	{
		/// <summary>
		/// The start position of the swipe.
		/// </summary>
		public Vector2 StartPosition { get { return positions[0]; } }

		/// <summary>
		/// The start position of the swipe in the viewport [0, 1].
		/// </summary>
		public Vector2 StartPositionViewport { get { return new Vector2(StartPosition.x / Screen.width, StartPosition.y / Screen.height); } }

		/// <summary>
		/// The last position of the swipe.
		/// </summary>
		public Vector2 LastPosition { get { return positions[positions.Count - 1]; } }

		/// <summary>
		/// The last position of the swipe in the viewport [0, 1].
		/// </summary>
		public Vector2 LastPositionViewport { get { return new Vector2(LastPosition.x / Screen.width, LastPosition.y / Screen.height); } }

		/// <summary>
		/// The previous position of the swipe.
		/// </summary>
		public Vector2 PreviousPosition { get { return positions[Mathf.Max(0, positions.Count - 2)]; } }

		/// <summary>
		/// The previous position of the swipe in the viewport [0, 1].
		/// </summary>
		public Vector2 PreviousPositionViewport { get { return new Vector2(PreviousPosition.x / Screen.width, PreviousPosition.y / Screen.height); } }

		/// <summary>
		/// The current velocity of the swipe.
		/// </summary>
		public Vector2 Velocity { private set; get; }

		/// <summary>
		/// The duration in seconds of the swipe.
		/// </summary>
		public float Duration { get { return Time.time - startTime; } }

		/// <summary>
		/// Whether or not this swipe has been completed.
		/// </summary>
		public bool IsComplete { set; get; }

		/// <summary>
		/// Whether or not this swipe has been consumed.
		/// </summary>
		public bool IsConsumed { set; get; }

		private List<Vector2> positions;
		private float startTime;

		/// <summary>
		/// Create a new <see cref="SwipeHandle"/>.
		/// </summary>
		public SwipeHandle()
		{
			positions = new List<Vector2>();

			startTime = Time.time;
		}

		/// <summary>
		/// Add a position to the swipe.
		/// </summary>
		/// <param name="_position">The position of the swipe.</param>
		public void AddPosition(Vector2 _position)
		{
			positions.Add(_position);

			// Update velocity
			Velocity = LastPosition - PreviousPosition;
		}

		/// <summary>
		/// Check whether or not <paramref name="_handle"/> swipe is still valid.
		/// </summary>
		/// <remarks>
		/// Returns true if <paramref name="_handle"/> has not been marked as completed or consumed.
		/// </remarks>
		/// <param name="_handle">The swipe handle.</param>
		public static implicit operator bool(SwipeHandle _handle)
		{
			return !_handle.IsComplete && !_handle.IsConsumed;
		}
	}
}
