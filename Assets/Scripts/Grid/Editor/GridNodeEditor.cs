using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(GridNode))]
public class GridNodeEditor : Editor
{
	private ReorderableList neighbours;

	private SerializedProperty prop_type;
	private SerializedProperty prop_neighbours;

	private GridNode node;
	private bool selecting;

	protected void OnEnable()
	{
		prop_type = serializedObject.FindProperty("type");
		prop_neighbours = serializedObject.FindProperty("neighbours");

		neighbours = new ReorderableList(serializedObject, prop_neighbours, false, true, true, true);
		neighbours.drawHeaderCallback += OnDrawHeader;
		neighbours.drawElementCallback += OnDrawElement;
		neighbours.onAddCallback += OnAdd;
		neighbours.onRemoveCallback += OnRemove;

		node = target as GridNode;
	}

	protected void OnSceneGUI()
	{
		if(Event.current.type == EventType.MouseDown)
		{
			if(Event.current.button == 1)
			{
				if(selecting)
				{
					Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
					RaycastHit hit;

					if(Physics.Raycast(ray, out hit, 1000))
					{
						GridNode targetNode = hit.collider.GetComponent<GridNode>();

						if(targetNode != null && targetNode != node)
						{
							node.AddNeighbour(targetNode);
							targetNode.AddNeighbour(node);

							selecting = false;
							neighbours.displayAdd = true;
						}
					}
				}
			}
		}
		else if(Event.current.type == EventType.KeyDown)
		{
			if(selecting)
			{
				selecting = false;
				neighbours.displayAdd = true;
			}
		}
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.LabelField("Grid Position: " + (target as GridNode).GridPosition);
		EditorGUILayout.PropertyField(prop_type);
		neighbours.DoLayoutList();

		serializedObject.ApplyModifiedProperties();
	}

	private void OnDrawHeader(Rect position)
	{
		EditorGUI.LabelField(position, "Neighbours");
	}

	private void OnDrawElement(Rect position, int index, bool isActive, bool isFocused)
	{
		SerializedProperty element = prop_neighbours.GetArrayElementAtIndex(index);

		position.y += 3;
		position.height = EditorGUIUtility.singleLineHeight;

		string side = "Invalid";
		if(node.NeighbourUp == element.objectReferenceValue)
		{
			side = "Up";
		}
		else if(node.NeighbourLeft == element.objectReferenceValue)
		{
			side = "Left";
		}
		else if(node.NeighbourDown == element.objectReferenceValue)
		{
			side = "Down";
		}
		else if(node.NeighbourRight == element.objectReferenceValue)
		{
			side = "Right";
		}

		float width = position.width;
		position.width = 80;
		EditorGUI.LabelField(position, side);

		GUI.enabled = false;

		position.x += 80;
		position.width = width - 80;
		EditorGUI.PropertyField(position, element, GUIContent.none);

		GUI.enabled = true;
	}

	private void OnAdd(ReorderableList reorderableList)
	{
		if(!selecting)
		{
			selecting = true;
			neighbours.displayAdd = false;
		}
	}

	private void OnRemove(ReorderableList reorderableList)
	{
		GridNode targetNode = prop_neighbours.GetArrayElementAtIndex(reorderableList.index).objectReferenceValue as GridNode;

		if(targetNode != null)
		{
			targetNode.RemoveNeighbour(node);
		}

		node.RemoveNeighbour(targetNode);

		SceneView.RepaintAll();
	}
}