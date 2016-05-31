using System.Collections;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Base class for entity movement, allows entities to move to and look at neighbouring nodes.
	/// </summary>
	/// <remarks>
	/// Requires an <see cref="EntityNodeTracker"/> component to work.
	/// </remarks>
	[RequireComponent(typeof(EntityNodeTracker))]
	public class EntityMovement : MonoBehaviour
	{
		public delegate void OnMove(GridNode _old, GridNode _new);
		public event OnMove onMoveEvent = delegate { };

		/// <summary>
		/// Shorthand to <see cref="EntityNodeTracker.CurrentNode"/>.
		/// </summary>
		public GridNode CurrentNode
		{
			get
			{
				return nodeTracker.CurrentNode;
			}
		}

		[SerializeField] private EntityNodeTracker nodeTracker;

		private Coroutine coroutine;
		private Entity entity;

		protected void Awake()
		{
			entity = GetComponent<Entity>();
		}

		protected void Reset()
		{
			nodeTracker = GetComponent<EntityNodeTracker>();
		}

		/// <summary>
		/// Move the entity in the specified direction.
		/// If there is no connecting node in the specified direction, nothing will happen.
		/// </summary>
		/// <remarks>
		/// <paramref name="_direction"/> is a normalized Vector2.
		/// </remarks>
		/// <param name="_direction">The direction to move in, should be normalized.</param>
		public void Move(Vector2 _direction)
		{
			LookAt(_direction);

			_direction.Normalize();

			GridNode target;
			if(CanMove(_direction, out target))
			{
				GridNode old = nodeTracker.CurrentNode;
				nodeTracker.CurrentNode = target;

				Vector3 nodePosition = nodeTracker.CurrentNode.Position;
				transform.position = new Vector3(nodePosition.x, nodePosition.y, nodePosition.z);

				// register the entity
				old.RemoveEntity(entity);

				if(coroutine != null)
				{
					StopCoroutine(coroutine);
				}

				coroutine = StartCoroutine(MoveAnimation(old, target));
			}
		}

		/// <summary>
		/// Check whether or not the entity can move in the specified direction.
		/// </summary>
		/// <remarks>
		/// <paramref name="_direction"/> is a normalized Vector2.
		/// </remarks>
		/// <param name="_direction">The direction to check, should be normalized.</param>
		/// <returns></returns>
		public bool CanMove(Vector2 _direction)
		{
			GridNode destination;
			return CanMove(_direction, out destination);
		}

		/// <summary>
		/// Check whether or not the entity can move in the specified direction.
		/// If it is, <paramref name="_destination"/> is set.
		/// </summary>
		/// <remarks>
		/// <paramref name="_direction"/> is a normalized Vector2.
		/// </remarks>
		/// <param name="_direction">The direction to check, should be normalized.</param>
		/// <param name="_destination">The destination, will be set to <code>null</code>
		/// if it isn't possible to move in the specified direction.</param>
		/// <returns></returns>
		public bool CanMove(Vector2 _direction, out GridNode _destination)
		{
			_direction.Normalize();

			GridNode target = GridUtils.GetNodeAt(nodeTracker.CurrentNode.GridPosition + _direction);
			if(target != null)
			{
				if(target.Active && nodeTracker.CurrentNode.HasConnection(target))
				{
					_destination = target;
					return true;
				}
			}

			_destination = null;
			return false;
		}

		/// <summary>
		/// Look in the specified direction.
		/// </summary>
		/// <remarks>
		/// <paramref name="_direction"/> is a normalized Vector2.
		/// </remarks>
		/// <param name="_direction">The direction to look at, should be normalized.</param>
		public void LookAt(Vector2 _direction)
		{
			_direction.Normalize();

			Quaternion rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.y));
			transform.rotation = rotation;
		}

		private IEnumerator MoveAnimation(GridNode from, GridNode to)
		{
			float length = (from.Position - to.Position).sqrMagnitude / 2;
			float st = Time.time;
			float t = 0;
			
			while(t < 1)
			{
				t = ((Time.time - st) * 100) / (length * length);
				transform.position = Vector3.Lerp(from.Position, from.Position + ((to.Position - from.Position) / 2) + new Vector3(0, 0.5f, 0), t);

				yield return null;
			}

			st = Time.time;
			t = 0;

			while(t < 1)
			{
				t = ((Time.time - st) * 100) / (length * length);
				transform.position = Vector3.Lerp(from.Position + ((to.Position - from.Position) / 2) + new Vector3(0, 0.5f, 0), to.Position, t);

				yield return null;
			}
			
			onMoveEvent(from, to);

			if(entity.HasTag("Player"))
			{
				GlobalEvents.Invoke(new PlayerMovedEvent(from, to));
			}

			to.AddEntity(entity);
		}
	}
}
