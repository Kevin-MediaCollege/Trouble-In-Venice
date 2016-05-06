using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntityNodeTracker))]
public class EntityNodeTrackerEditor : Editor
{
	private SerializedProperty prop_manualY;

	protected void OnEnable()
	{
		prop_manualY = serializedObject.FindProperty("manualY");
	}

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
				float y = ec.transform.position.y;
				if(!prop_manualY.boolValue)
				{
					y = nearest.transform.position.y + 1;
				}

				ec.transform.position = new Vector3(nearest.transform.position.x, y, nearest.transform.position.z);
				ec.CurrentNode = nearest;
			}
		}
	}
}