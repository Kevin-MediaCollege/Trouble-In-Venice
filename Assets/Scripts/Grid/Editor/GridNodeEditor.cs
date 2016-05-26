using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace Proeve
{
	/// <summary>
	/// Editor add-on for <see cref="GridNode"/>.
	/// </summary>
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

			GUI.enabled = false;
			EditorGUILayout.LabelField("Grid Position: " + (target as GridNode).GridPosition);
			GUI.enabled = true;

			EditorGUILayout.PropertyField(prop_type);
			connections.DoLayoutList();

			List<GridNode> c = new List<GridNode>(GridUtils.GetNeighbours8(target as GridNode));
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

				Vector3 newPosition = nearest + (Vector3.up * node.transform.position.y);

				if(node.transform.position != newPosition)
				{
					node.RemoveAllConnections();
					node.transform.position = newPosition;
				}
			}
		}

		private void OnDrawHeader(Rect _position)
		{
			EditorGUI.LabelField(_position, "Connections");
		}

		private void OnDrawElement(Rect _position, int _index, bool _isActive, bool _isFocused)
		{
			if(_index >= prop_connections.arraySize)
			{
				return;
			}

			SerializedProperty element = prop_connections.GetArrayElementAtIndex(_index);

			GridNode node1 = target as GridNode;
			GridNode node2 = element.objectReferenceValue as GridNode;

			if(node2 == null)
			{
				prop_connections.DeleteArrayElementAtIndex(_index);
				return;
			}

			_position.y += 3;
			_position.height = EditorGUIUtility.singleLineHeight;

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

			EditorGUI.LabelField(_position, label);
		}

		private void OnAddDropdown(Rect _rect, ReorderableList _reorderableList)
		{
			GenericMenu menu = new GenericMenu();
			GridNode node = target as GridNode;

			AddGenericMenuElement(menu, node, "Up", Vector2.up);
			AddGenericMenuElement(menu, node, "Left", Vector2.left);
			AddGenericMenuElement(menu, node, "Down", Vector2.down);
			AddGenericMenuElement(menu, node, "Right", Vector2.right);

			menu.ShowAsContext();
		}

		private void AddGenericMenuElement(GenericMenu _menu, GridNode _source, string _text, Vector2 _direction)
		{
			GridNode node = GridUtils.GetNodeAt(_source.GridPosition + _direction);

			if(node != null && !_source.HasConnection(node))
			{
				_menu.AddItem(new GUIContent(_text), false, OnAdd, _direction);
			}
		}

		private void OnAdd(object _direction)
		{
			GridNode node1 = target as GridNode;
			GridNode node2 = GridUtils.GetNodeAt(node1.GridPosition + (Vector2)_direction);

			node1.AddConnection(node2);
			node2.AddConnection(node1);

			SceneView.RepaintAll();
		}

		private void OnRemove(ReorderableList _reorderableList)
		{
			GridNode node1 = target as GridNode;
			GridNode node2 = prop_connections.GetArrayElementAtIndex(_reorderableList.index).objectReferenceValue as GridNode;

			node1.RemoveConnection(node2);
			node2.RemoveConnection(node1);

			SceneView.RepaintAll();
		}
	}
}
