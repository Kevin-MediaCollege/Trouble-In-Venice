using System;

/// <summary>
/// The EventDispatcher interface.
/// </summary>
public interface IEventDispatcher
{
	/// <summary>
	/// Add an event listener.
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="handler">The handler.</param>
	void AddListener(Type type, Action handler);

	/// <summary>
	/// Add an event listener.
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="handler">The handler.</param>
	void AddListener<T>(Action<T> handler) where T : IEvent;

	/// <summary>
	/// Remove an event listener.
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="handler">The handler.</param>
	void RemoveListener(Type type, Action handler);

	/// <summary>
	/// Remove an event listener.
	/// </summary>
	/// <typeparam name="T">he type of the event.</typeparam>
	/// <param name="handler">The handler.</param>
	void RemoveListener<T>(Action<T> handler) where T : IEvent;

	/// <summary>
	/// Invoke an event.
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="evt">The event.</param>
	void Invoke(Type type, object evt);

	/// <summary>
	/// Invoke an event.
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="evt">The event.</param>
	void Invoke<T>(T evt) where T : IEvent;
}
