using System;

/// <summary>
/// Use this for global event handling.
/// </summary>
public class GlobalEvents
{
	private static IEventDispatcher eventDispacher;

	static GlobalEvents()
	{
		eventDispacher = new EventDispatcher();
	}

	/// <summary>
	/// Static implementation of <see cref="EventDispatcher.AddListener(Type, Action)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="handler">The handler.</param>
	public static void AddListener(Type type, Action handler)
	{
		eventDispacher.AddListener(type, handler);
	}

	/// <summary>
	/// Static implementation of <see cref="EventDispatcher.AddListener{T}(Action{T})"/>
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="handler">The handler.</param>
	public static void AddListener<T>(Action<T> handler) where T : IEvent
	{
		eventDispacher.AddListener(handler);
	}

	/// <summary>
	/// Static implementation of <see cref="EventDispatcher.RemoveListener(Type, Action)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="handler">The handler.</param>
	public static void RemoveListener(Type type, Action handler)
	{
		eventDispacher.RemoveListener(type, handler);
	}

	/// <summary>
	/// Static implementation of <see cref="EventDispatcher.RemoveListener{T}(Action{T})"/>
	/// </summary>
	/// <typeparam name="T">he type of the event.</typeparam>
	/// <param name="handler">The handler.</param>
	public static void RemoveListener<T>(Action<T> handler) where T : IEvent
	{
		eventDispacher.RemoveListener(handler);
	}

	/// <summary>
	/// Static implementation of <see cref="EventDispatcher.Invoke(Type, object)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="evt">The event.</param>
	public static void Invoke(Type type, object evt)
	{
		eventDispacher.Invoke(type, evt);
	}

	/// <summary>
	/// Static implementation of <see cref="EventDispatcher.Invoke{T}(T)"/>
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="evt">The event.</param>
	public static void Invoke<T>(T evt) where T : IEvent
	{
		eventDispacher.Invoke(evt);
	}
}
