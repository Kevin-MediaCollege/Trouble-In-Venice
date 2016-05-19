using UnityEngine;
using UnityEngine.UI;

namespace Proeve
{
	public abstract class TutorialSegment : MonoBehaviour
	{
		public string Text
		{
			get
			{
				return text;
			}
		}

		[SerializeField] private string text;

		public abstract bool IsComplete { get; }

		public virtual void Start()
		{
		}

		public virtual void Stop()
		{
		}
	}
}
