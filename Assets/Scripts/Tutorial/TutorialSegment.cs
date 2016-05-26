using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// Base class for tutorial segments.
	/// </summary>
	public abstract class TutorialSegment : MonoBehaviour
	{
		/// <summary>
		/// The text of this segment.
		/// </summary>
		public string Text
		{
			get
			{
				return text;
			}
		}

		[SerializeField] private string text;

		/// <summary>
		/// Whether or not this segment has been completed.
		/// </summary>
		public abstract bool IsComplete { get; }

		/// <summary>
		/// Called when the segment should be started.
		/// </summary>
		public virtual void Start()
		{
		}

		/// <summary>
		/// Called when the segment should be stopped.
		/// </summary>
		public virtual void Stop()
		{
		}
	}
}
