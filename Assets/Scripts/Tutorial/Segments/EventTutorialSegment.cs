using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Tutorial segment which waits for a specific event before being completed.
	/// </summary>
	public class EventTutorialSegment : TutorialSegment
	{
		public override bool IsComplete
		{
			get
			{
				return complete;
			}
		}

		[TypeDropdown(typeof(IEvent)), SerializeField] private string type;

		private bool complete;

		public override void Start()
		{
			GlobalEvents.AddListener(Type.GetType(type), OnEvent);
		}

		public override void Stop()
		{
			GlobalEvents.RemoveListener(Type.GetType(type), OnEvent);
		}

		private void OnEvent()
		{
			complete = true;
		}
	}
}
