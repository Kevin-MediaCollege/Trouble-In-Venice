using System.Collections.Generic;
using UnityEngine;

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

	public static IEnumerable<GridNode> GetNodesAround(GridNode node)
	{
		List<GridNode> result = new List<GridNode>();

		result.Add(Grid.GetNodeAt(node.GridPosition + new Vector3( 0, 0,  1))); // Up
		result.Add(Grid.GetNodeAt(node.GridPosition + new Vector3(-1, 0,  1))); // Up-left
		result.Add(Grid.GetNodeAt(node.GridPosition + new Vector3(-1, 0,  0))); // Left
		result.Add(Grid.GetNodeAt(node.GridPosition + new Vector3(-1, 0, -1))); // Down-left
		result.Add(Grid.GetNodeAt(node.GridPosition + new Vector3( 0, 0, -1))); // Down
		result.Add(Grid.GetNodeAt(node.GridPosition + new Vector3( 1, 0, -1))); // Down-right
		result.Add(Grid.GetNodeAt(node.GridPosition + new Vector3( 1, 0,  0))); // Right
		result.Add(Grid.GetNodeAt(node.GridPosition + new Vector3( 1, 0,  1))); // Up-right

		// Remove all null-entries
		result.RemoveAll(element => element == null);
		return result;
	}

	public static GridNode GetNodeFromScreenPosition(Vector2 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit[] hits = Physics.RaycastAll(ray, 100);

		Debug.DrawRay(ray.origin, ray.direction * 100, Color.gray, 2);


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