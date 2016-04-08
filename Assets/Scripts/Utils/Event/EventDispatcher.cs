using System;
using System.Collections.Generic;

/// <summary>
/// The event dispatcher.
/// </summary>
public class EventDispatcher : IEventDispatcher
{
	private Dictionary<Type, Action<object>> eventCallbacks;
	private Dictionary<object, Action<object>> delegateLookup;

	public EventDispatcher()
	{
		eventCallbacks = new Dictionary<Type, Action<object>>();
		delegateLookup = new Dictionary<object, Action<object>>();
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.AddListener(Type, Action)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="handler">The handler.</param>
	public void AddListener(Type type, Action handler)
	{
		if(!eventCallbacks.ContainsKey(type))
		{
			eventCallbacks.Add(type, delegate { });
		}

		Action<object> handlerFunc = obj => { handler(); };

		eventCallbacks[type] += handlerFunc;
		delegateLookup[handler] = handlerFunc;
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.AddListener{T}(Action{T})"/>
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="handler">The handler.</param>
	public void AddListener<T>(Action<T> handler) where T : IEvent
	{
		Type type = typeof(T);

		if(!eventCallbacks.ContainsKey(type))
		{
			eventCallbacks.Add(type, delegate { });
		}

		Action<object> handlerFunc = obj => { handler((T)obj); };

		eventCallbacks[type] += handlerFunc;
		delegateLookup[handler] = handlerFunc;
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.RemoveListener(Type, Action)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="handler">The handler.</param>
	public void RemoveListener(Type type, Action handler)
	{
		if(eventCallbacks.ContainsKey(type) && delegateLookup.ContainsKey(handler))
		{
			eventCallbacks[type] -= delegateLookup[handler];
			delegateLookup.Remove(handler);

			if(eventCallbacks[type].GetInvocationList().Length == 1)
			{
				eventCallbacks.Remove(type);
			}
		}
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.RemoveListener{T}(Action{T})"/>
	/// </summary>
	/// <typeparam name="T">he type of the event.</typeparam>
	/// <param name="handler">The handler.</param>
	public void RemoveListener<T>(Action<T> handler) where T : IEvent
	{
		Type type = typeof(T);

		if(eventCallbacks.ContainsKey(type) && delegateLookup.ContainsKey(handler))
		{
			eventCallbacks[type] -= delegateLookup[handler];
			delegateLookup.Remove(handler);

			if(eventCallbacks[type].GetInvocationList().Length == 1)
			{
				eventCallbacks.Remove(type);
			}
		}
	}

	/// <summary>
	/// Remove all registered event listeners.
	/// </summary>
	public void RemoveAllListeners()
	{
		eventCallbacks.Clear();
		delegateLookup.Clear();
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.Invoke(Type, object)"/>
	/// </summary>
	/// <param name="type">The type of the event.</param>
	/// <param name="evt">The event.</param>
	public void Invoke(Type type, object evt)
	{
		Action<object> handler;

		if(eventCallbacks.TryGetValue(type, out handler))
		{
			if(handler != null)
			{
				handler.Invoke(evt);
			}
		}
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.Invoke{T}(T)"/>
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="evt">The event.</param>
	public void Invoke<T>(T evt) where T : IEvent
	{
		Type type = typeof(T);
		Invoke(type, evt);
	}
}
