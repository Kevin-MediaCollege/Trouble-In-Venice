using System.Collections.Generic;
using UnityEngine;

public static class GridUtils
{
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

	public static IEnumerable<GridNode> GetNeighbours(GridNode node)
	{
		List<GridNode> result = new List<GridNode>();

		result.Add(GetNodeAt(node.GridPosition + new Vector2( 0,  1))); // Up
		result.Add(GetNodeAt(node.GridPosition + new Vector2(-1,  1))); // Up-left
		result.Add(GetNodeAt(node.GridPosition + new Vector2(-1,  0))); // Left
		result.Add(GetNodeAt(node.GridPosition + new Vector2(-1, -1))); // Down-left
		result.Add(GetNodeAt(node.GridPosition + new Vector2( 0, -1))); // Down
		result.Add(GetNodeAt(node.GridPosition + new Vector2( 1, -1))); // Down-right
		result.Add(GetNodeAt(node.GridPosition + new Vector2( 1,  0))); // Right
		result.Add(GetNodeAt(node.GridPosition + new Vector2( 1,  1))); // Up-right

		// Remove all null-entries
		result.RemoveAll(element => element == null);
		return result;
	}

	public static GridNode GetNodeAt(Vector2 position)
	{
		return Grid.GetNodeAt(position);
	}

	public static GridNode GetNodeAtGUI(Vector2 position)
	{
		position = new Vector2(Mathf.Round(position.x / Grid.SIZE), Mathf.Round(position.y / Grid.SIZE));
		return GetNodeAt(position);
	}
}