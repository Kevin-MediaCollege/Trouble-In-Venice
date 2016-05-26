using System;
using System.Collections.Generic;

namespace Proeve
{
	/// <summary>
	/// Non-generic interface for stat trackers.
	/// </summary>
	public interface ITracker
	{
		/// <summary>
		/// Called when the tracker should be enabled.
		/// </summary>
		void OnEnable();

		/// <summary>
		/// Called when the tracker should be disabled.
		/// </summary>
		void OnDisable();

		/// <summary>
		/// Get the value of the tracker.
		/// </summary>
		/// <returns></returns>
		object GetValue();
	}

	/// <summary>
	/// Generic interface for stat trackers.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ITracker<T> : ITracker
	{
		/// <summary>
		/// Get the value of the tracker.
		/// </summary>
		/// <returns></returns>
		new T GetValue();
	}
}
