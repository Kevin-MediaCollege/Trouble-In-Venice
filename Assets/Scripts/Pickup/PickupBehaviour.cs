using System.Collections;
using UnityEngine;

public abstract class PickupBehaviour : MonoBehaviour
{
	[SerializeField] protected EntityNodeTracker nodeTracker;

	private bool activated;

	protected void OnEnable()
	{
		nodeTracker = GetComponent<EntityNodeTracker>();
	}

	protected void LateUpdate()
	{
		if(activated)
		{
			OnUpdate();
		}
	}

	protected void Reset()
	{
		nodeTracker = GetComponent<EntityNodeTracker>();
	}

	public void Activate()
	{
		GlobalEvents.Invoke(new PickupStartEvent());

		activated = true;
		OnActivate();
	}

	public void Deactivate()
	{
		GlobalEvents.Invoke(new PickupStopEvent());

		activated = false;
		OnDeactivate();
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