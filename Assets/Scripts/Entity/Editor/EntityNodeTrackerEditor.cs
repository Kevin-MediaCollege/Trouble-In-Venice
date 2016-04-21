using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntityNodeTracker))]
public class EntityNodeTrackerEditor : Editor
{
	protected void OnSceneGUI()
	{
		if(!Application.isPlaying)
		{
			EntityNodeTracker ec = target as EntityNodeTracker;

			Grid grid = FindObjectOfType<Grid>();
			GridNode nearest = null;
			float nearestDistance = float.PositiveInfinity;

			foreach(GridNode node in grid.Nodes)
			{
				float distance = (node.transform.position - ec.transform.position).sqrMagnitude;

				if(distance < nearestDistance)
				{
					nearest = node;
					nearestDistance = distance;
				}
			}

			if(nearest != null)
			{
				ec.transform.position = new Vector3(nearest.transform.position.x, ec.transform.position.y, nearest.transform.position.z);
				ec.CurrentNode = nearest;
			}
		}
	}
}