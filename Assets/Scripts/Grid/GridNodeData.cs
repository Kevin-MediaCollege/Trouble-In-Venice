using UnityEngine;

/// <summary>
/// Contains data to create a grid node
/// </summary>
public struct GridNodeData
{
	public Vector3 Position;
	public GridNodeType Type;

	public bool[] Neighbours;

	public GridNodeData(Vector3 position, GridNodeType type, bool[] neighbours)
	{
		Neighbours = neighbours;
		Position = position;
		Type = type;
	}
}