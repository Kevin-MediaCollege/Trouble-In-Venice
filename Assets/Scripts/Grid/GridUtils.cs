using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A collection of <see cref="Grid"/> utilities.
	/// </summary>
	public static class GridUtils
	{
		/// <summary>
		/// Get the starting node of the <see cref="Grid"/>.
		/// </summary>
		public static GridNode Start
		{
			get
			{
				foreach(GridNode node in Grid.Nodes)
				{
					if(node.IsStart)
					{
						return node;
					}
				}

				return null;
			}
		}

		/// <summary>
		/// Get the end node of the <see cref="Grid"/>.
		/// </summary>
		public static GridNode End
		{
			get
			{
				foreach(GridNode node in Grid.Nodes)
				{
					if(node.IsEnd)
					{
						return node;
					}
				}

				return null;
			}
		}

		private static Grid grid;
		private static Grid Grid
		{
			get
			{
				if(grid == null)
				{
					grid = Object.FindObjectOfType<Grid>();
				}

				return grid;
			}
		}

		/// <summary>
		/// Get all neighbours of a <see cref="GridNode"/>.
		/// </summary>
		/// <param name="_node">The target node.</param>
		/// <returns>All neighbours of <paramref name="_node"/>.</returns>
		public static IEnumerable<GridNode> GetNeighbours(GridNode _node)
		{
			List<GridNode> result = new List<GridNode>();

			result.Add(GetNodeAt(_node.GridPosition + new Vector2(0, 1))); // Up
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(-1, 1))); // Up-left
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(-1, 0))); // Left
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(-1, -1))); // Down-left
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(0, -1))); // Down
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(1, -1))); // Down-right
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(1, 0))); // Right
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(1, 1))); // Up-right

			// Remove all null-entries
			result.RemoveAll(element => element == null);
			return result;
		}

		/// <summary>
		/// Get the <see cref="GridNode"/> at the specified position.
		/// </summary>
		/// <remarks>
		/// <paramref name="_position"/> represents a position on the grid, not transform.position.
		/// </remarks>
		/// <param name="_position">The position of the node.</param>
		/// <returns>The <see cref="GridNode"/> at the specified position, or null if there is none.</returns>
		/// <returns></returns>
		public static GridNode GetNodeAt(Vector2 _position)
		{
			return Grid.GetNodeAt(_position);
		}

		public static GridNode GetConnectionInDirection(GridNode origin, Vector2 direction)
		{
			GridNode node = GetNodeAt(origin.GridPosition + direction);

			if(node != null && origin.HasConnection(node))
			{
				return node;
			}

			return null;
		}

		/// <summary>
		/// Get the <see cref="GridNode"/> at the specified position.
		/// </summary>
		/// <remarks>
		/// <paramref name="_position"/> represents a screen space coordinate.
		/// </remarks>
		/// <param name="_position">The position of the node.</param>
		/// <returns>The <see cref="GridNode"/> at the specified position.</returns>
		public static GridNode GetNodeAtGUI(Vector2 _position)
		{
			Ray ray = Camera.main.ScreenPointToRay(_position);
			RaycastHit[] hits = Physics.RaycastAll(ray);

			foreach(RaycastHit hit in hits)
			{
				if(hit.collider != null)
				{
					GridNode node = hit.collider.GetComponent<GridNode>();

					if(node != null)
					{
						return node;
					}
				}
			}

			return null;
		}

		public static Vector2 GetDirectionVector(GridDirection _direction)
		{
			switch(_direction)
			{
			case GridDirection.Up:
				return Vector2.up;
			case GridDirection.Left:
				return Vector2.left;
			case GridDirection.Down:
				return Vector2.down;
			case GridDirection.Right:
				return Vector2.right;
			default:
				return Vector2.zero;
			}
		}
	}
}
