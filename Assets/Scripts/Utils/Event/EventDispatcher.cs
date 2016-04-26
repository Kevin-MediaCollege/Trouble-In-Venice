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
	/// <param name="_type">The type of the event.</param>
	/// <param name="_handler">The handler.</param>
	public void AddListener(Type _type, Action _handler)
	{
		if(!eventCallbacks.ContainsKey(_type))
		{
			eventCallbacks.Add(_type, delegate { });
		}

		Action<object> handlerFunc = obj => { _handler(); };

		eventCallbacks[_type] += handlerFunc;
		delegateLookup[_handler] = handlerFunc;
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.AddListener{T}(Action{T})"/>
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="_handler">The handler.</param>
	public void AddListener<T>(Action<T> _handler) where T : IEvent
	{
		Type type = typeof(T);

		if(!eventCallbacks.ContainsKey(type))
		{
			eventCallbacks.Add(type, delegate { });
		}

		Action<object> handlerFunc = obj => { _handler((T)obj); };

		eventCallbacks[type] += handlerFunc;
		delegateLookup[_handler] = handlerFunc;
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.RemoveListener(Type, Action)"/>
	/// </summary>
	/// <param name="_type">The type of the event.</param>
	/// <param name="_handler">The handler.</param>
	public void RemoveListener(Type _type, Action _handler)
	{
		if(eventCallbacks.ContainsKey(_type) && delegateLookup.ContainsKey(_handler))
		{
			eventCallbacks[_type] -= delegateLookup[_handler];
			delegateLookup.Remove(_handler);

			if(eventCallbacks[_type].GetInvocationList().Length == 1)
			{
				eventCallbacks.Remove(_type);
			}
		}
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.RemoveListener{T}(Action{T})"/>
	/// </summary>
	/// <typeparam name="T">he type of the event.</typeparam>
	/// <param name="_handler">The handler.</param>
	public void RemoveListener<T>(Action<T> _handler) where T : IEvent
	{
		Type type = typeof(T);

		if(eventCallbacks.ContainsKey(type) && delegateLookup.ContainsKey(_handler))
		{
			eventCallbacks[type] -= delegateLookup[_handler];
			delegateLookup.Remove(_handler);

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
	/// <param name="_type">The type of the event.</param>
	/// <param name="_evt">The event.</param>
	public void Invoke(Type _type, object _evt)
	{
		Action<object> handler;

		if(eventCallbacks.TryGetValue(_type, out handler))
		{
			if(handler != null)
			{
				handler.Invoke(_evt);
			}
		}
	}

	/// <summary>
	/// Implementation of <see cref="IEventDispatcher.Invoke{T}(T)"/>
	/// </summary>
	/// <typeparam name="T">The type of the event.</typeparam>
	/// <param name="_evt">The event.</param>
	public void Invoke<T>(T _evt) where T : IEvent
	{
		Type type = typeof(T);
		Invoke(type, _evt);
	}
}
