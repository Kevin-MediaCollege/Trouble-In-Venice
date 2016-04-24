using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// The grid of a board, there can only be one grid at a time
/// </summary
public class Grid : MonoBehaviour
{
	public const int WIDTH = 50;
	public const int HEIGHT = 50;

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
	private Dictionary<Vector2, GridNode> nodeCache;

	protected void Awake()
	{
		nodeCache = new Dictionary<Vector2, GridNode>();

		foreach(GridNode node in nodes)
		{
			nodeCache.Add(node.GridPosition, node);
		}
	}

	protected void OnDrawGizmosSelected()
	{
		DrawGizmos();
	}

	public void DrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.matrix = transform.localToWorldMatrix;

		for(int x = -WIDTH; x < WIDTH + 1; x++)
		{
			Vector3 start = new Vector3(x, 0, -HEIGHT) * SIZE;
			Vector3 end = new Vector3(x, 0, HEIGHT) * SIZE;

			Gizmos.DrawLine(start, end);
		}
		
		for(int z = -HEIGHT; z < HEIGHT + 1; z++)
		{
			Vector3 start = new Vector3(-WIDTH, 0, z) * SIZE;
			Vector3 end = new Vector3(WIDTH, 0, z) * SIZE;

			Gizmos.DrawLine(start, end);
		}

		Gizmos.matrix = Matrix4x4.identity;
		foreach(GridNode node in nodes)
		{
			node.DrawGizmos();
		}
	}

	public bool AddNode(GridNode node)
	{
		if(!nodes.Contains(node))
		{
			nodes.Add(node);
			return true;
		}

		return false;
	}

	public void RemoveNode(GridNode node)
	{
		nodes.Remove(node);
	}

	public GridNode GetNodeAt(Vector2 position)
	{
		if(nodeCache == null)
		{
			foreach(GridNode node in nodes)
			{
				if(node.GridPosition == position)
				{
					return node;
				}
			}
		}
		else
		{
			if(nodeCache.ContainsKey(position))
			{
				return nodeCache[position];
			}
		}

		return null;
	}
}