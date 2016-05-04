using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityNodeTracker))]
public class EntityMovement : MonoBehaviour
{
	public delegate void OnMove(GridNode _old, GridNode _new);
	public event OnMove onMoveEvent = delegate { };

	[SerializeField] private EntityNodeTracker nodeTracker;

	private Entity entity;

	protected void Awake()
	{
		entity = GetComponent<Entity>();
	}

	protected void Reset()
	{
		nodeTracker = GetComponent<EntityNodeTracker>();
	}

	protected void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		GizmosUtils.DrawArrowXZ(transform.position + Vector3.up, transform.forward / 2, 0.3f, 0.5f);
	}

	public void Move(Vector2 _direction)
	{
		LookAt(_direction);

		GridNode target = GridUtils.GetNodeAt(nodeTracker.CurrentNode.GridPosition + _direction);
		if(target != null && target.Active)
		{
			GridNode old = nodeTracker.CurrentNode;

			nodeTracker.CurrentNode = target;

			Vector2 nodePosition = nodeTracker.CurrentNode.Position;
			transform.position = new Vector3(nodePosition.x, transform.position.y, nodePosition.y);

			// (Un)register the entity
			old.RemoveEntity(entity);
			target.AddEntity(entity);

			onMoveEvent(old, target);
		}
	}

	public void LookAt(Vector2 _direction)
	{
		Quaternion rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.y));
		transform.rotation = rotation;
	}
}