using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Proeve
{
	public class TutorialController : MonoBehaviour
	{
		[SerializeField] private Canvas canvas;
		[SerializeField] private Text text;

		[SerializeField] private TutorialSegment[] segments;

		private int currentSegmentIndex;

		protected void Start()
		{
			currentSegmentIndex = -1;
			NextSegment();
		}

		protected void Update()
		{
			if(currentSegmentIndex >= 0 && currentSegmentIndex < segments.Length)
			{
				if(segments[currentSegmentIndex].IsComplete)
				{
					NextSegment();
				}
			}
		}

		private void NextSegment()
		{
			if(currentSegmentIndex >= 0 && currentSegmentIndex < segments.Length)
			{
				segments[currentSegmentIndex].Stop();
			}

			if(++currentSegmentIndex >= segments.Length)
			{
				canvas.enabled = false;
				return;
			}
			
			segments[currentSegmentIndex].Start();

			text.text = segments[currentSegmentIndex].Text;
		}
	}
}
