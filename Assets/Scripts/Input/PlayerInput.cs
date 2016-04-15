using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private LayerMask gridNodeLayer;
	[SerializeField] private EntityController controller;

	protected void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			Debug.DrawRay(ray.origin, ray.direction * 100, Color.gray, 2);

			if(Physics.Raycast(ray, out hit, 100, gridNodeLayer))
			{
				GridNode node = hit.collider.GetComponent<GridNode>();

				if(node != null)
				{
					Direction? direction = controller.GetDirectionTo(node);

					if(direction != null)
					{
						controller.Move(direction.Value);
					}
				}
			}
		}
	}
}