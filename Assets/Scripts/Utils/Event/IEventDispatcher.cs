using System;

namespace Utils
{
	/// <summary>
	/// The EventDispatcher interface.
	/// </summary>
	public interface IEventDispatcher
	{
		/// <summary>
		/// Add an event listener.
		/// </summary>
		/// <param name="_type">The type of the event.</param>
		/// <param name="_handler">The handler.</param>
		void AddListener(Type _type, Action _handler);

		/// <summary>
		/// Add an event listener.
		/// </summary>
		/// <typeparam name="T">The type of the event.</typeparam>
		/// <param name="_handler">The handler.</param>
		void AddListener<T>(Action<T> _handler) where T : IEvent;

		/// <summary>
		/// Remove an event listener.
		/// </summary>
		/// <param name="_type">The type of the event.</param>
		/// <param name="_handler">The handler.</param>
		void RemoveListener(Type _type, Action _handler);

		/// <summary>
		/// Remove an event listener.
		/// </summary>
		/// <typeparam name="T">he type of the event.</typeparam>
		/// <param name="_handler">The handler.</param>
		void RemoveListener<T>(Action<T> _handler) where T : IEvent;

		/// <summary>
		/// Invoke an event.
		/// </summary>
		/// <param name="_type">The type of the event.</param>
		/// <param name="_evt">The event.</param>
		void Invoke(Type _type, object _evt);

		/// <summary>
		/// Invoke an event.
		/// </summary>
		/// <typeparam name="T">The type of the event.</typeparam>
		/// <param name="_evt">The event.</param>
		void Invoke<T>(T _evt) where T : IEvent;
	}
}
