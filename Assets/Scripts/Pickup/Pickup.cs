using System;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class Pickup : MonoBehaviour
	{
		protected GridNode node;

		protected bool used;

		private new Renderer renderer;
		private bool active;

		protected void OnEnable()
		{
			node = GetComponent<EntityNodeTracker>().CurrentNode;
			renderer = GetComponent<Renderer>();

			node.onEntityEnteredEvent += OnEntityEntered;
			node.onEntityLeftEvent += OnEntityLeft;
		}

		private void OnDisable()
		{
			node.onEntityEnteredEvent -= OnEntityEntered;
			node.onEntityLeftEvent -= OnEntityLeft;
		}

		protected void LateUpdate()
		{
			if(active && !used)
			{
				OnUpdate();

				if(used)
				{
					OnDeactivate();

					if(!enabled)
					{
						renderer.enabled = false;
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_entity"></param>
		private void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				active = true;
				used = false;

				renderer.enabled = false;
				OnActivate();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_entity"></param>
		private void OnEntityLeft(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				active = false;
				renderer.enabled = true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void OnActivate()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void OnDeactivate()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void OnUpdate()
		{
		}
	}
}
