using UnityEngine;
using System.Collections.Generic;
using System;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Base class for any AI.
	/// </summary>
	public class AIBase : GridNodeObject
	{
		private static Dictionary<Type, AICommand> commandTypeCache = new Dictionary<Type, AICommand>();

		/// <summary>
		/// Get the <see cref="Entity"/> of the AI.
		/// </summary>
		public Entity Entity
		{
			get
			{
				return GetComponent<Entity>();
			}
		}

		/// <summary>
		/// The <see cref="EntityMovement"/> of the AI.
		/// </summary>
		public EntityMovement Movement
		{
			get
			{
				return movement;
			}
		}

		[SerializeField] private EntityMovement movement;

		protected void Reset()
		{
			movement = GetComponent<EntityMovement>();
		}

		/// <summary>
		/// Execute an <see cref="AICommand"/>.
		/// </summary>
		/// <typeparam name="T">The type of the command, <code>T</code> must inherit from <see cref="AICommand"/>.</typeparam>
		protected void ExecuteCommand<T>() where T : AICommand, new()
		{
			Type type = typeof(T);

			if(!commandTypeCache.ContainsKey(type))
			{
				AICommand command = new T();
				commandTypeCache.Add(type, command);
			}

			commandTypeCache[type].Execute(this);
		}
	}
}
