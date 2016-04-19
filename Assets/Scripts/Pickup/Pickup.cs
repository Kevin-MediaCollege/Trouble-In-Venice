using UnityEngine;
using System.Collections;
using System;

public class Pickup : MonoBehaviour
{
	[SerializeField, TypeDropdown(typeof(PickupBehaviour))] private string behaviour;

	private EntityController playerController;
	private EntityController entityController;

	protected void OnEnable()
	{
		Entity player = EntityUtils.GetEntityWithTag("Player");
		playerController = player.GetComponent<EntityController>();

		entityController = GetComponent<EntityController>();
	}

	protected void Update()
	{
		if(playerController.CurrentNode == entityController.CurrentNode)
		{
			Type type = Type.GetType(behaviour);

			if(type != null)
			{
				PickupBehaviour instance = Activator.CreateInstance(type) as PickupBehaviour;
				GlobalEvents.Invoke(new PickupStartEvent(instance));
			}
			
			Destroy(gameObject);
		}
	}
}