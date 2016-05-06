using System;
using UnityEngine;

namespace Utils
{
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
		/// <param name="_type">The type of the event.</param>
		/// <param name="_handler">The handler.</param>
		public void AddListener(Type _type, Action _handler)
		{
			EventDispatcher.AddListener(_type, _handler);
		}

		/// <summary>
		/// Implementation of <see cref="IEventDispatcher.AddListener{T}(Action{T})"/>
		/// </summary>
		/// <typeparam name="T">The type of the event.</typeparam>
		/// <param name="_handler">The handler.</param>
		public void AddListener<T>(Action<T> _handler) where T : IEvent
		{
			EventDispatcher.AddListener(_handler);
		}

		/// <summary>
		/// Implementation of <see cref="IEventDispatcher.RemoveListener(Type, Action)"/>
		/// </summary>
		/// <param name="_type">The type of the event.</param>
		/// <param name="_handler">The handler.</param>
		public void RemoveListener(Type _type, Action _handler)
		{
			EventDispatcher.RemoveListener(_type, _handler);
		}

		/// <summary>
		/// Implementation of <see cref="IEventDispatcher.RemoveListener{T}(Action{T})"/>
		/// </summary>
		/// <typeparam name="T">he type of the event.</typeparam>
		/// <param name="_handler">The handler.</param>
		public void RemoveListener<T>(Action<T> _handler) where T : IEvent
		{
			EventDispatcher.RemoveListener(_handler);
		}

		/// <summary>
		/// Implementation of <see cref="IEventDispatcher.Invoke(Type, object)"/>
		/// </summary>
		/// <param name="_type">The type of the event.</param>
		/// <param name="_evt">The event.</param>
		public void Invoke(Type _type, object _evt)
		{
			EventDispatcher.Invoke(_type, _evt);
		}

		/// <summary>
		/// Implementation of <see cref="IEventDispatcher.Invoke{T}(T)"/>
		/// </summary>
		/// <typeparam name="T">The type of the event.</typeparam>
		/// <param name="_evt">The event.</param>
		public void Invoke<T>(T _evt) where T : IEvent
		{
			EventDispatcher.Invoke(_evt);
		}
	}
}
