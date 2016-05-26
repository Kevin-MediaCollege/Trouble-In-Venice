using System;
using System.Collections;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// Tutorial segments which appears for a certain duration before being completed.
	/// </summary>
	public class TimedTutorialSegment : TutorialSegment
	{
		public override bool IsComplete
		{
			get
			{
				return Time.time - startTime >= duration;
			}
		}

		[SerializeField] private float duration;

		private float startTime;

		public override void Start()
		{
			startTime = Time.time;
		}
	}
}
