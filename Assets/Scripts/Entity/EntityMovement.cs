using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityNodeTracker))]
public class EntityMovement : MonoBehaviour
{
	[SerializeField] private EntityNodeTracker nodeTracker;

	protected void Reset()
	{
		nodeTracker = GetComponent<EntityNodeTracker>();
	}

	protected void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		GizmosUtils.DrawArrowXZ(transform.position + Vector3.up, transform.forward / 2, 0.3f, 0.5f);
	}

	public void Move(Vector2 direction)
	{
		LookAt(direction);

		GridNode target = GridUtils.GetNodeAt(nodeTracker.CurrentNode.GridPosition + direction);
		if(target != null)
		{
			nodeTracker.CurrentNode = target;

			Vector3 position = nodeTracker.CurrentNode.transform.position;
			transform.position = new Vector3(position.x, transform.position.y, position.z);
		}
	}

	public void LookAt(Vector2 direction)
	{
		Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
		transform.rotation = rotation;
	}
}