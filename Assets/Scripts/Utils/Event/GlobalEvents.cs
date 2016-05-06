using System;

namespace Utils
{
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
		/// <param name="_type">The type of the event.</param>
		/// <param name="_handler">The handler.</param>
		public static void AddListener(Type _type, Action _handler)
		{
			eventDispacher.AddListener(_type, _handler);
		}

		/// <summary>
		/// Static implementation of <see cref="EventDispatcher.AddListener{T}(Action{T})"/>
		/// </summary>
		/// <typeparam name="T">The type of the event.</typeparam>
		/// <param name="_handler">The handler.</param>
		public static void AddListener<T>(Action<T> _handler) where T : IEvent
		{
			eventDispacher.AddListener(_handler);
		}

		/// <summary>
		/// Static implementation of <see cref="EventDispatcher.RemoveListener(Type, Action)"/>
		/// </summary>
		/// <param name="_type">The type of the event.</param>
		/// <param name="_handler">The handler.</param>
		public static void RemoveListener(Type _type, Action _handler)
		{
			eventDispacher.RemoveListener(_type, _handler);
		}

		/// <summary>
		/// Static implementation of <see cref="EventDispatcher.RemoveListener{T}(Action{T})"/>
		/// </summary>
		/// <typeparam name="T">he type of the event.</typeparam>
		/// <param name="_handler">The handler.</param>
		public static void RemoveListener<T>(Action<T> _handler) where T : IEvent
		{
			eventDispacher.RemoveListener(_handler);
		}

		/// <summary>
		/// Static implementation of <see cref="EventDispatcher.Invoke(Type, object)"/>
		/// </summary>
		/// <param name="_type">The type of the event.</param>
		/// <param name="_evt">The event.</param>
		public static void Invoke(Type _type, object _evt)
		{
			eventDispacher.Invoke(_type, _evt);
		}

		/// <summary>
		/// Static implementation of <see cref="EventDispatcher.Invoke{T}(T)"/>
		/// </summary>
		/// <typeparam name="T">The type of the event.</typeparam>
		/// <param name="_evt">The event.</param>
		public static void Invoke<T>(T _evt) where T : IEvent
		{
			eventDispacher.Invoke(_evt);
		}
	}
}
