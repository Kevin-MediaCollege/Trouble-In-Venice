using UnityEngine;
using UnityEditor;

namespace Proeve
{
	/// <summary>
	/// Editor add-on for <see cref="EntityNodeTracker"/>.
	/// </summary>
	[CustomEditor(typeof(EntityNodeTracker))]
	public class EntityNodeTrackerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUI.enabled = false;

			EditorGUILayout.ObjectField(new GUIContent("Current Node"), (target as EntityNodeTracker).CurrentNode, typeof(GridNode), true);

			GUI.enabled = true;
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
					if(node == null)
					{
						continue;
					}

					float distance = (node.transform.position - ec.transform.position).sqrMagnitude;

					if(distance < nearestDistance)
					{
						nearest = node;
						nearestDistance = distance;
					}
				}

				if(nearest != null)
				{
					ec.transform.position = nearest.transform.position;
					ec.CurrentNode = nearest;
				}
			}
		}
	}
}
