using UnityEngine;

/// <summary>
/// Helper for the gameboard grid, it'll simply draw gizmos of the current grid
/// </summary>
public class GridHelper : MonoBehaviour
{
	private Grid grid;

	protected void OnDrawGizmos()
	{
		foreach(GridNode node in grid.Nodes)
		{
			switch(node.Type)
			{
			case GridNodeType.Start:
				Gizmos.color = Color.green;
				break;
			case GridNodeType.End:
				Gizmos.color = Color.red;
				break;
			case GridNodeType.Normal:
				Gizmos.color = Color.gray;
				break;
			}

			Gizmos.DrawCube(node.WorldPosition, new Vector3(1, 0.3f, 1));

			Gizmos.color = Color.blue;
			foreach(GridNode neighbour in node.Neighbours)
			{
				Gizmos.DrawLine(node.WorldPosition, neighbour.WorldPosition);
			}
		}
	}

	/// <summary>
	/// Set the grid
	/// </summary>
	/// <param name="grid">The grid</param>
	public void SetGrid(Grid grid)
	{
		this.grid = grid;
	}
}