using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(GridNode))]
public class GridNodeEditor : Editor
{
	private ReorderableList connections;

	private SerializedProperty prop_type;
	private SerializedProperty prop_connections;

	protected void OnEnable()
	{
		prop_type = serializedObject.FindProperty("type");
		prop_connections = serializedObject.FindProperty("connections");

		connections = new ReorderableList(serializedObject, prop_connections, false, true, true, true);
		connections.drawHeaderCallback += OnDrawHeader;
		connections.drawElementCallback += OnDrawElement;
		connections.onAddDropdownCallback += OnAddDropdown;
		connections.onRemoveCallback += OnRemove;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		
		EditorGUILayout.PropertyField(prop_type);
		connections.DoLayoutList();

		List<GridNode> c = new List<GridNode>(GridUtils.GetNeighbours(target as GridNode));
		connections.displayAdd = prop_connections.arraySize != c.Count;

		serializedObject.ApplyModifiedProperties();
	}

	protected void OnSceneGUI()
	{
		if(!Application.isPlaying)
		{
			GridNode node = target as GridNode;

			Vector3 nearest = Vector3.zero;
			float nearestDistance = float.PositiveInfinity;

			for(int x = -Grid.WIDTH; x < Grid.WIDTH + 1; x++)
			{
				for(int z = -Grid.HEIGHT; z < Grid.HEIGHT + 1; z++)
				{
					Vector3 nodePosition = new Vector3(node.transform.position.x, 0, node.transform.position.z);
					Vector3 position = new Vector3(x + 1.5f, 0, z + 1.5f) * Grid.SIZE;

					float distance = (position - nodePosition).sqrMagnitude;

					if(distance < nearestDistance)
					{
						nearest = position;
						nearestDistance = distance;
					}
				}
			}

			node.transform.position = nearest + (Vector3.up * node.transform.position.y);
		}
	}

	private void OnDrawHeader(Rect position)
	{
		EditorGUI.LabelField(position, "Connections");
	}

	private void OnDrawElement(Rect position, int index, bool isActive, bool isFocused)
	{
		SerializedProperty element = prop_connections.GetArrayElementAtIndex(index);

		GridNode node1 = target as GridNode;
		GridNode node2 = element.objectReferenceValue as GridNode;

		position.y += 3;
		position.height = EditorGUIUtility.singleLineHeight;

		Vector2 direction = (node2.GridPosition - node1.GridPosition).normalized;
		string label = "Invalid";

		if(direction == Vector2.up)
		{
			label = "Up";
		}
		else if(direction == Vector2.left)
		{
			label = "Left";
		}
		else if(direction == Vector2.down)
		{
			label = "Down";
		}
		else if(direction == Vector2.right)
		{
			label = "Right";
		}

		EditorGUI.LabelField(position, label);
	}

	private void OnAddDropdown(Rect rect, ReorderableList reorderableList)
	{
		GenericMenu menu = new GenericMenu();
		GridNode node = target as GridNode;

		AddGenericMenuElement(menu, node, "Up", Vector2.up);
		AddGenericMenuElement(menu, node, "Left", Vector2.left);
		AddGenericMenuElement(menu, node, "Down", Vector2.down);
		AddGenericMenuElement(menu, node, "Right", Vector2.right);

		menu.ShowAsContext();
	}

	private void AddGenericMenuElement(GenericMenu menu, GridNode source, string text, Vector2 direction)
	{
		GridNode node = GridUtils.GetNodeAt(source.GridPosition + direction);

		if(node != null && !source.HasConnection(node))
		{
			menu.AddItem(new GUIContent(text), false, OnAdd, direction);
		}
	}

	private void OnAdd(object direction)
	{
		GridNode node1 = target as GridNode;
		GridNode node2 = GridUtils.GetNodeAt(node1.GridPosition + (Vector2)direction);

		node1.AddConnection(node2);
		node2.AddConnection(node1);

		SceneView.RepaintAll();
	}

	private void OnRemove(ReorderableList reorderableList)
	{
		GridNode node1 = target as GridNode;
		GridNode node2 = prop_connections.GetArrayElementAtIndex(reorderableList.index).objectReferenceValue as GridNode;

		node1.RemoveConnection(node2);
		node2.RemoveConnection(node1);

		SceneView.RepaintAll();
	}
}