using System;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
	protected GridNode node;

	protected bool used;

	private EntityNodeTracker playerNodeTracker;
	private new Renderer renderer;
	private bool active;

	protected void OnEnable()
	{
		playerNodeTracker = EntityUtils.GetEntityWithTag("Player").GetComponent<EntityNodeTracker>();
		node = GetComponent<EntityNodeTracker>().CurrentNode;
		renderer = GetComponent<Renderer>();
	}

	protected void LateUpdate()
	{
		if(!active && playerNodeTracker.CurrentNode == node)
		{
			active = true;
			used = false;

			renderer.enabled = false;
			OnActivate();
		}
		else if(active && playerNodeTracker.CurrentNode != node)
		{
			
			active = false;
			renderer.enabled = true;
		}

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

	protected virtual void OnActivate()
	{
	}

	protected virtual void OnDeactivate()
	{
	}

	protected virtual void OnUpdate()
	{
	}
}