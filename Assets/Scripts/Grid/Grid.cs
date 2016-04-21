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

	[HideInInspector, SerializeField] private List<GridNode> nodes;
	private Dictionary<Vector3, GridNode> nodePositionCache;

	protected void Awake()
	{
		nodePositionCache = new Dictionary<Vector3, GridNode>();

		foreach(GridNode node in nodes)
		{
			nodePositionCache.Add(node.GridPosition, node);
		}
	}

	public void AddNode(GridNode node)
	{
		if(!nodes.Contains(node))
		{
			nodes.Add(node);
		}
	}

	public void RemoveNode(GridNode node)
	{
		nodes.Remove(node);
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