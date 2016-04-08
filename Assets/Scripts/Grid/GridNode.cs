using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A node in a grid
/// </summary>
public class GridNode
{
	/// <summary>
	/// The position of the node in the grid
	/// </summary>
	public Vector3 GridPosition { private set; get; }

	/// <summary>
	/// The position of the node in the world
	/// </summary>
	public Vector3 WorldPosition
	{
		get
		{
			return GridPosition * Grid.GRID_SIZE;
		}
	}

	/// <summary>
	/// The type of the node
	/// </summary>
	public GridNodeType Type { private set; get; }

	/// <summary>
	/// Get a list of all neighbours of this node
	/// </summary>
	public GridNode[] Neighbours
	{
		get
		{
			List<GridNode> result = new List<GridNode>();

			if(Up != null)
			{
				result.Add(Up);
			}

			if(Left != null)
			{
				result.Add(Left);
			}

			if(Down != null)
			{
				result.Add(Down);
			}

			if(Right != null)
			{
				result.Add(Right);
			}

			return result.ToArray();
		}
	}

	/// <summary>
	/// Get the z+ neighbour
	/// </summary>
	public GridNode Up { private set; get; }

	/// <summary>
	/// Get the X- neighbour
	/// </summary>
	public GridNode Left { private set; get; }

	/// <summary>
	/// Get the Z- neighbour
	/// </summary>
	public GridNode Down { private set; get; }

	/// <summary>
	/// Get the X+ neighbour
	/// </summary>
	public GridNode Right { private set; get; }
	
	public GridNode(Grid grid, GridNodeData data)
	{
		GridPosition = data.Position;
		Type = data.Type;

		if(data.Neighbours[0])
		{
			Up = grid.GetNodeAt(GridPosition + Vector3.forward);
		}

		if(data.Neighbours[1])
		{
			Left = grid.GetNodeAt(GridPosition + Vector3.left);
		}

		if(data.Neighbours[2])
		{
			Down = grid.GetNodeAt(GridPosition + Vector3.back);
		}

		if(data.Neighbours[3])
		{
			Right = grid.GetNodeAt(GridPosition + Vector3.right);
		}
	}
}