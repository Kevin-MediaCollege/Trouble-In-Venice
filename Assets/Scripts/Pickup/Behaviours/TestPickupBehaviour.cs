using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickupBehaviour : PickupBehaviour
{
	protected override IEnumerator Update()
	{
		// Wait for mouse button press
		while(!Input.GetMouseButtonDown(0))
		{
			yield return null;
		}

		GridNode node = GridUtils.GetNodeFromScreenPosition(Input.mousePosition);
		if(node != null)
		{
			Debug.Log("Threw pickup to: " + node.GridPosition);
			Stop();
		}
	}
}