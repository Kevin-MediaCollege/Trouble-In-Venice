using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The grid of a board, there can only be one grid at a time
/// </summary>
public class Grid : IDependency
{
	public const int GRID_SIZE = 3;

	/// <summary>
	/// Get all nodes in the grid
	/// </summary>
	public IEnumerable<GridNode> Nodes
	{
		get
		{
			return nodes;
		}
	}

	/// <summary>
	/// Get the starting node in the grid
	/// </summary>
	public GridNode Start
	{
		get
		{
			foreach(GridNode node in nodes)
			{
				if(node.Type == GridNodeType.Start)
				{
					return node;
				}
			}

			return null;
		}
	}

	/// <summary>
	/// Get the end point in the grid
	/// </summary>
	public GridNode End
	{
		get
		{
			foreach(GridNode node in nodes)
			{
				if(node.Type == GridNodeType.End)
				{
					return node;
				}
			}

			return null;
		}
	}

	private HashSet<GridNode> nodes;
	private Dictionary<Vector3, GridNode> nodePositionCache;

	private GameObject gridHelper;

	public Grid()
	{
		gridHelper = new GameObject("Grid Helper");
		GridHelper helper = gridHelper.AddComponent<GridHelper>();
		helper.SetGrid(this);

		nodes = new HashSet<GridNode>();
		nodePositionCache = new Dictionary<Vector3, GridNode>();
	}

	/// <summary>
	/// Create a new grid
	/// </summary>
	/// <param name="nodeData">The node data of the grid</param>
	public void Create(IEnumerable<GridNodeData> nodeData)
	{
		if(nodes.Count > 0 || nodePositionCache.Count > 0)
		{
			Debug.LogWarning("Grid has not been cleaned up before creating a new grid");
			Destroy();
		}		

		foreach(GridNodeData node in nodeData)
		{
			GridNode current = new GridNode(this, node);

			nodes.Add(current);
			nodePositionCache.Add(node.Position, current);
		}
	}

	/// <summary>
	/// Destroy the current grid
	/// </summary>
	public void Destroy()
	{
		nodes.Clear();
		nodePositionCache.Clear();
	}

	/// <summary>
	/// Attempt to get the node at the specified position
	/// </summary>
	/// <param name="position">The position of the node</param>
	/// <returns></returns>
	public GridNode GetNodeAt(Vector3 position)
	{
		if(!nodePositionCache.ContainsKey(position))
		{
			return null;
		}

		return nodePositionCache[position];
	}
}