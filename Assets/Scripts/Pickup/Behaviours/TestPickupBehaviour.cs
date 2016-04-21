using System.Collections.Generic;
using UnityEngine;

public class TestPickupBehaviour : PickupBehaviour
{
	private IEnumerable<GridNode> nodes;

	protected override void OnActivate()
	{
		nodes = GridUtils.GetNodesAround(nodeTracker.CurrentNode);
	}

	protected override void OnUpdate()
	{
		HighlightNodes(Color.blue);

		// Wait for mouse button press
		if(Input.GetMouseButtonDown(0))
		{
			GridNode node = GridUtils.GetNodeFromScreenPosition(Input.mousePosition);
			if(node != null)
			{
				HighlightNodes(Color.white);
				node.GetComponentInChildren<Renderer>().material.color = Color.yellow;

				Deactivate();
			}
		}
	}

	private void HighlightNodes(Color color)
	{
		foreach(GridNode node in nodes)
		{
			Renderer renderer = node.GetComponentInChildren<Renderer>();
			renderer.material.color = color;
		}
	}
}