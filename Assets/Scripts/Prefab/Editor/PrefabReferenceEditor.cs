using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(PrefabReference))]
public class PrefabReferenceEditor : Editor
{
	private ReorderableList reorderableList;

	private SerializedProperty prop_prefabs;

	protected void OnEnable()
	{
		prop_prefabs = serializedObject.FindProperty("prefabs");

		reorderableList = new ReorderableList(serializedObject, prop_prefabs, false, true, true, true);
		reorderableList.drawHeaderCallback += OnDrawHeader;
		reorderableList.drawElementCallback += OnDrawElement;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		reorderableList.DoLayoutList();

		serializedObject.ApplyModifiedProperties();
	}

	private void OnDrawHeader(Rect position)
	{
		EditorGUI.LabelField(position, "Prefab References");
	}

	private void OnDrawElement(Rect position, int index, bool isActive, bool isFocused)
	{
		SerializedProperty element = prop_prefabs.GetArrayElementAtIndex(index);

		SerializedProperty prefab = element.FindPropertyRelative("prefab");
		SerializedProperty id = element.FindPropertyRelative("id");

		position.y += 3;
		position.height = EditorGUIUtility.singleLineHeight;

		Rect idPosition = position;
		idPosition.width /= 5;
		EditorGUI.PropertyField(idPosition, id, GUIContent.none);

		Rect prefabPosition = position;
		prefabPosition.width -= idPosition.width + 5;
		prefabPosition.x = idPosition.x + idPosition.width + 5;
		EditorGUI.PropertyField(prefabPosition, prefab, GUIContent.none);
	}
}