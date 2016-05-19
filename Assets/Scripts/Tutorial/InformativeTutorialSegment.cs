using System;
using System.Collections;
using UnityEngine;

namespace Proeve
{
	public class InformativeTutorialSegment : TutorialSegment
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
