using System;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
	protected GridNode node;

	protected bool used;

	private EntityNodeTracker playerNodeTracker;
	private bool active;

	protected void OnEnable()
	{
		playerNodeTracker = EntityUtils.GetEntityWithTag("Player").GetComponent<EntityNodeTracker>();
		node = GetComponent<EntityNodeTracker>().CurrentNode; 
	}

	protected void LateUpdate()
	{
		if(!active && playerNodeTracker.CurrentNode == node)
		{
			active = true;
			used = false;

			SetRenderers(false);
			OnActivate();
		}
		else if(active && playerNodeTracker.CurrentNode != node)
		{
			
			active = false;
			SetRenderers(true);
		}

		if(active && !used)
		{
			OnUpdate();

			if(used)
			{
				OnDeactivate();

				if(!enabled)
				{
					SetRenderers(false);
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

	private void SetRenderers(bool _enabled)
	{
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in renderers)
		{
			renderer.enabled = _enabled;
		}
	}
}