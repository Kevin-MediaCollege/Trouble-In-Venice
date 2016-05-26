using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A throwable pickup, can be thrown to neighbouring nodes when
	/// when picked up by the player <see cref="Utils.Entity"/>.
	/// </summary>
	public class ThrowablePickup : Pickup
	{
		private List<GridNode> nodes;

		protected override void OnActivate()
		{
			nodes = new List<GridNode>(GridUtils.GetNeighbours8(node));

			HighlightNodes(Color.blue);
		}

		protected override void OnUpdate()
		{
			// Wait for mouse button press
			if(Input.GetMouseButtonDown(0))
			{
				GridNode node = GridUtils.GetNodeAtGUI(Input.mousePosition);
				if(node != null && nodes.Contains(node))
				{
					HighlightNodes(Color.white);
					node.GetComponentInChildren<Renderer>().material.color = Color.yellow;
					
					enabled = false;
				}
			}
		}

		private void HighlightNodes(Color _color)
		{
			foreach(GridNode node in nodes)
			{
				Renderer renderer = node.GetComponentInChildren<Renderer>();
				renderer.material.color = _color;
			}
		}
	}
}
