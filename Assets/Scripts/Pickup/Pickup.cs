using UnityEngine;

public class Pickup : MonoBehaviour
{
	[SerializeField] private PickupBehaviour behaviour;

	private EntityNodeTracker playerController;
	private EntityNodeTracker entityController;

	protected void OnEnable()
	{
		Entity player = EntityUtils.GetEntityWithTag("Player");
		playerController = player.GetComponent<EntityNodeTracker>();
		entityController = GetComponent<EntityNodeTracker>();
	}

	protected void Update()
	{
		if(playerController.CurrentNode == entityController.CurrentNode)
		{
			behaviour.Activate();

			Renderer[] renderers = GetComponentsInChildren<Renderer>();
			foreach(Renderer renderer in renderers)
			{
				renderer.enabled = false;
			}

			enabled = false;
		}
	}

	protected void Reset()
	{
		behaviour = GetComponent<PickupBehaviour>();
	}
}