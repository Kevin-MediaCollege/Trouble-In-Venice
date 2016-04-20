using UnityEngine;
using System.Collections;

public class EntityNodeTracker : MonoBehaviour
{
	public GridNode CurrentNode { set; get; }

	protected void Start()
	{
		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 5))
		{
			GridNode gridNode = hit.collider.GetComponent<GridNode>();

			if(gridNode != null)
			{
				CurrentNode = gridNode;
			}
		}
	}
}