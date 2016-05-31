using System;
using System.Collections.Generic;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Event to enable and disable user input.
	/// </summary>
	public class SetInputEvent : IEvent
	{
		/// <summary>
		/// Whether or not input should be enabled.
		/// </summary>
		public bool Enabled { private set; get; }

		/// <summary>
		/// Create a new instance of this event.
		/// </summary>
		/// <param name="enabled">Whether or not input should be enabled.</param>
		public SetInputEvent(bool enabled)
		{
			Enabled = enabled;
		}
	}
}