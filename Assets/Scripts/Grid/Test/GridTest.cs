using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GridTest : MonoBehaviour
{
	[Serializable]
	public struct Data
	{
		public Vector3 Position;
		public GridNodeType Type;
		public bool up;
		public bool left;
		public bool down;
		public bool right;
	}

	[SerializeField] private Data[] data;

	protected void Start()
	{
		Grid grid = Dependency.Get<Grid>();

		List<GridNodeData> nodeData = new List<GridNodeData>();
		foreach(Data d in data)
		{
			nodeData.Add(new GridNodeData(d.Position, d.Type, new bool[] { d.up, d.left, d.down, d.right }));
		}

		grid.Create(nodeData);
	}
}