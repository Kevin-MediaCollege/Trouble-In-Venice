using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Utils
{
	[CustomEditor(typeof(Entity))]
	public class EntityEditor : Editor
	{
		private ReorderableList startingTags;

		private SerializedProperty prop_startingTags;

		protected void OnEnable()
		{
			prop_startingTags = serializedObject.FindProperty("startingTags");

			startingTags = new ReorderableList(serializedObject, prop_startingTags, false, true, true, true);
			startingTags.drawHeaderCallback += OnDrawHeader;
			startingTags.drawElementCallback += OnDrawElement;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.Space();
			startingTags.DoLayoutList();

			serializedObject.ApplyModifiedProperties();
		}

		private void OnDrawHeader(Rect _position)
		{
			EditorGUI.LabelField(_position, new GUIContent("Tags"));
		}

		private void OnDrawElement(Rect _position, int _index, bool _isActive, bool _isFocused)
		{
			_position.y += 3;
			_position.height = EditorGUIUtility.singleLineHeight;

			SerializedProperty element = prop_startingTags.GetArrayElementAtIndex(_index);
			EditorGUI.PropertyField(_position, element, new GUIContent("Tag " + (_index + 1)));
		}
	}
}
