using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// The grid of a board, there can only be one grid at a time
/// </summary
public class Grid : MonoBehaviour
{
	public const int SIZE = 3;

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

	private HashSet<GridNode> nodes;
	private Dictionary<Vector3, GridNode> nodePositionCache;

	protected void Awake()
	{
		nodes = new HashSet<GridNode>();
		nodePositionCache = new Dictionary<Vector3, GridNode>();
	}

	public void AddNode(GridNode node)
	{
		nodes.Add(node);
		nodePositionCache.Add(node.Position, node);
	}

	public GridNode GetNodeAt(Vector3 position)
	{
		if(!nodePositionCache.ContainsKey(position))
		{
			return null;
		}

		return nodePositionCache[position];
	}

	public GridNode GetStart()
	{
		foreach(GridNode node in Nodes)
		{
			if(node.IsStart)
			{
				return node;
			}
		}

		return null;
	}

	public GridNode GetEnd()
	{
		foreach(GridNode node in Nodes)
		{
			if(node.IsEnd)
			{
				return node;
			}
		}

		return null;
	}
}