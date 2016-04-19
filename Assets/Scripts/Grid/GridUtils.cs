using System;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtils
{
	public static GridNode GetNodeFromScreenPosition(Vector2 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit[] hits = Physics.RaycastAll(ray, 100);

		Debug.DrawRay(ray.origin, ray.direction * 100, Color.gray, 2);


		foreach(RaycastHit hit in hits)
		{
			if(hit.collider != null)
			{
				GridNode node = hit.collider.GetComponent<GridNode>();

				if(node != null)
				{
					return node;
				}
			}
		}

		return null;
	}
}