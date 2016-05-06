using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Proeve
{
	/// <summary>
	/// The grid of a board, there can only be one grid at a time
	/// </summary>
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

		/// <summary>
		/// 
		/// </summary>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_node"></param>
		/// <returns></returns>
		public bool AddNode(GridNode _node)
		{
			if(!nodes.Contains(_node))
			{
				nodes.Add(_node);
				return true;
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_node"></param>
		public void RemoveNode(GridNode _node)
		{
			nodes.Remove(_node);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_position"></param>
		/// <returns></returns>
		public GridNode GetNodeAt(Vector2 _position)
		{
			if(nodeCache == null)
			{
				foreach(GridNode node in nodes)
				{
					if(node.GridPosition == _position)
					{
						return node;
					}
				}
			}
			else
			{
				if(nodeCache.ContainsKey(_position))
				{
					return nodeCache[_position];
				}
			}

			return null;
		}
	}
}
