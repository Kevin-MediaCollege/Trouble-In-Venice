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
}