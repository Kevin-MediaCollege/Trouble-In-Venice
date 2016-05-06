using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	public static class GridUtils
	{
		/// <summary>
		/// 
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
		/// 
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

		/// <summary>
		/// 
		/// </summary>
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

		private static Grid grid;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static IEnumerable<GridNode> GetNeighbours(GridNode node)
		{
			List<GridNode> result = new List<GridNode>();

			result.Add(GetNodeAt(node.GridPosition + new Vector2(0, 1))); // Up
			result.Add(GetNodeAt(node.GridPosition + new Vector2(-1, 1))); // Up-left
			result.Add(GetNodeAt(node.GridPosition + new Vector2(-1, 0))); // Left
			result.Add(GetNodeAt(node.GridPosition + new Vector2(-1, -1))); // Down-left
			result.Add(GetNodeAt(node.GridPosition + new Vector2(0, -1))); // Down
			result.Add(GetNodeAt(node.GridPosition + new Vector2(1, -1))); // Down-right
			result.Add(GetNodeAt(node.GridPosition + new Vector2(1, 0))); // Right
			result.Add(GetNodeAt(node.GridPosition + new Vector2(1, 1))); // Up-right

			// Remove all null-entries
			result.RemoveAll(element => element == null);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public static GridNode GetNodeAt(Vector2 position)
		{
			return Grid.GetNodeAt(position);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public static GridNode GetNodeAtGUI(Vector2 position)
		{
			Ray ray = Camera.main.ScreenPointToRay(position);
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
	}
}
