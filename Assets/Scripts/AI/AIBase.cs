using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Proeve
{
	public class AIBase : GridNodeObject
	{
		private static Dictionary<Type, AICommand> commandTypeCache = new Dictionary<Type, AICommand>();

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
