using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A collection of <see cref="Grid"/> utilities.
	/// </summary>
	public static class GridUtils
	{
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
		/// Get neighbours of a <see cref="GridNode"/>.
		/// </summary>
		/// <remarks>
		///   +
		/// + o +
		///   +
		/// </remarks>
		/// <param name="_node">The target node.</param>
		/// <returns>The neighbours of <paramref name="_node"/>.</returns>
		public static IEnumerable<GridNode> GetNeighbours4(GridNode _node)
		{
			List<GridNode> result = new List<GridNode>();

			result.Add(GetNodeAt(_node.GridPosition + new Vector2(0, 1))); // Up
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(-1, 0))); // Left
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(0, -1))); // Down
			result.Add(GetNodeAt(_node.GridPosition + new Vector2(1, 0))); // Right

			// Remove all null-entries
			result.RemoveAll(element => element == null);
			return result;
		}

		/// <summary>
		/// Get neighbours of a <see cref="GridNode"/>.
		/// </summary>
		/// <remarks>
		/// + + +
		/// + o +
		/// + + +
		/// </remarks>
		/// <param name="_node">The target node.</param>
		/// <returns>The neighbours of <paramref name="_node"/>.</returns>
		public static IEnumerable<GridNode> GetNeighbours8(GridNode _node)
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

		/// <summary>
		/// Get the connection of a <see cref="GridNode"/> in the specified <paramref name="_direction"/>.
		/// </summary>
		/// <param name="_origin">The origin node.</param>
		/// <param name="_direction">The direction to get the connection in.</param>
		/// <returns>The connection in the specified <paramref name="_direction"/>, or null.</returns>
		public static GridNode GetConnectionInDirection(GridNode _origin, GridDirection _direction)
		{
			return GetConnectionInDirection(_origin, GetDirectionVector(_direction));
		}

		/// <summary>
		/// Get the connection of a <see cref="GridNode"/> in the specified <paramref name="_direction"/>.
		/// </summary>
		/// <param name="_origin">The origin node.</param>
		/// <param name="_direction">The direction to get the connection in.</param>
		/// <returns>The connection in the specified <paramref name="_direction"/>, or null.</returns>
		public static GridNode GetConnectionInDirection(GridNode _origin, Vector2 _direction)
		{
			if(_origin == null)
			{
				return null;
			}

			GridNode node = GetNodeAt(_origin.GridPosition + _direction);

			if(node != null && _origin.HasConnection(node))
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

		/// <summary>
		/// Convert a <see cref="GridDirection"/> to a Vector2.
		/// </summary>
		/// <param name="_direction">The direction to convert.</param>
		/// <returns>The specified <paramref name="_direction"/> in a Vector2 representation.</returns>
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
