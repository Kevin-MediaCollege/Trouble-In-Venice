using System;
using UnityEngine;

/// <summary>
/// Use this for local event handling.
/// </summary>
public class LocalEvents : MonoBehaviour, IEventDispatcher
{
	private IEventDispatcher eventDispatcher;
	private IEventDispatcher EventDispatcher
	{
		get
		{
			if(eventDispatcher == null)
			{
				eventDispatcher = new EventDispatcher();
			}

			return eventDispatcher;
		}
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.AddListener(Type, Action)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="handler">The handler.</param>
	public void AddListener(Type type, Action handler)
	{
		EventDispatcher.AddListener(type, handler);
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.AddListener{T}(Action{T})"/>
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="handler">The handler.</param>
	public void AddListener<T>(Action<T> handler) where T : IEvent
	{
		EventDispatcher.AddListener(handler);
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.RemoveListener(Type, Action)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="handler">The handler.</param>
	public void RemoveListener(Type type, Action handler)
	{
		EventDispatcher.RemoveListener(type, handler);
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.RemoveListener{T}(Action{T})"/>
	/// </summary>
	/// <typeparam name="T">he type of the event.</typeparam>
	/// <param name="handler">The handler.</param>
	public void RemoveListener<T>(Action<T> handler) where T : IEvent
	{
		EventDispatcher.RemoveListener(handler);
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.Invoke(Type, object)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="evt">The event.</param>
	public void Invoke(Type type, object evt)
	{
		EventDispatcher.Invoke(type, evt);
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.Invoke{T}(T)"/>
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="evt">The event.</param>
	public void Invoke<T>(T evt) where T : IEvent
	{
		EventDispatcher.Invoke(evt);
	}
}
