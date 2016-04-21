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

		result.Add(GetNodeAt(node.GridPosition + new Vector3( 0, 0,  1))); // Up
		result.Add(GetNodeAt(node.GridPosition + new Vector3(-1, 0,  1))); // Up-left
		result.Add(GetNodeAt(node.GridPosition + new Vector3(-1, 0,  0))); // Left
		result.Add(GetNodeAt(node.GridPosition + new Vector3(-1, 0, -1))); // Down-left
		result.Add(GetNodeAt(node.GridPosition + new Vector3( 0, 0, -1))); // Down
		result.Add(GetNodeAt(node.GridPosition + new Vector3( 1, 0, -1))); // Down-right
		result.Add(GetNodeAt(node.GridPosition + new Vector3( 1, 0,  0))); // Right
		result.Add(GetNodeAt(node.GridPosition + new Vector3( 1, 0,  1))); // Up-right

		// Remove all null-entries
		result.RemoveAll(element => element == null);
		return result;
	}

	public static GridNode GetNodeAt(Vector3 position)
	{
		foreach(GridNode node in Grid.Nodes)
		{
			Bounds bounds = node.GetComponent<Collider>().bounds;
			if(bounds.Contains(position))
			{
				return node;
			}
		}

		return null;
	}

	public static GridNode GetStart()
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

	public static GridNode GetEnd()
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