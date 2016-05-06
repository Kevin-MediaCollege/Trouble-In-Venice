using UnityEngine;
using System.Collections.Generic;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class SwipeHandle
	{
		/// <summary>
		/// 
		/// </summary>
		public Vector2 StartPosition { get { return positions[0]; } }

		/// <summary>
		/// 
		/// </summary>
		public Vector2 StartPositionViewport { get { return new Vector2(StartPosition.x / Screen.width, StartPosition.y / Screen.height); } }

		/// <summary>
		/// 
		/// </summary>
		public Vector2 LastPosition { get { return positions[positions.Count - 1]; } }

		/// <summary>
		/// 
		/// </summary>
		public Vector2 LastPositionViewport { get { return new Vector2(LastPosition.x / Screen.width, LastPosition.y / Screen.height); } }

		public Vector2 PreviousPosition { get { return positions[Mathf.Max(0, positions.Count - 2)]; } }

		/// <summary>
		/// 
		/// </summary>
		public Vector2 PreviousPositionViewport { get { return new Vector2(PreviousPosition.x / Screen.width, PreviousPosition.y / Screen.height); } }

		/// <summary>
		/// 
		/// </summary>
		public Vector2 Velocity { private set; get; }

		/// <summary>
		/// 
		/// </summary>
		public float Duration { get { return Time.time - startTime; } }

		/// <summary>
		/// 
		/// </summary>
		public bool IsComplete { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public bool IsConsumed { set; get; }

		private List<Vector2> positions;
		private float startTime;

		/// <summary>
		/// 
		/// </summary>
		public SwipeHandle()
		{
			positions = new List<Vector2>();

			startTime = Time.time;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_position"></param>
		public void AddPosition(Vector2 _position)
		{
			positions.Add(_position);

			// Update velocity
			Velocity = LastPosition - PreviousPosition;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_handle"></param>
		public static implicit operator bool(SwipeHandle _handle)
		{
			return !_handle.IsComplete && !_handle.IsConsumed;
		}
	}
}
