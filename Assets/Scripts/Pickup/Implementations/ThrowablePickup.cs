using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ThrowablePickup : Pickup
	{
		private List<GridNode> nodes;

		/// <summary>
		/// 
		/// </summary>
		protected override void OnActivate()
		{
			nodes = new List<GridNode>(GridUtils.GetNeighbours(node));

			HighlightNodes(Color.blue);
		}

		/// <summary>
		/// 
		/// </summary>
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

					used = true;
					enabled = false;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_color"></param>
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
