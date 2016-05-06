using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

namespace Utils
{
	/// <summary>
	/// The property drawer for the <see cref="TypeDropdownAttribute"/>
	/// </summary>
	[CustomPropertyDrawer(typeof(TypeDropdownAttribute))]
	public class TypeDropdownDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
		{
			EditorGUI.BeginProperty(_position, _label, _property);
			TypeDropdownAttribute typeAttribute = attribute as TypeDropdownAttribute;
			Type baseType = typeAttribute.BaseType;

			string[] allTypes = Reflection.AllTypeStringsFrom(baseType).ToArray();

			if(allTypes.Length <= 0)
			{
				EditorGUI.Popup(_position, _label.text, 0, new string[] { "No types of " + baseType.Name + " found" });
			}
			else
			{
				Array.Sort(allTypes);

				SerializedProperty stringProperty = null;

				if(_property.propertyType == SerializedPropertyType.String)
				{
					stringProperty = _property;
				}

				if(stringProperty != null)
				{
					string currentType = stringProperty.stringValue;

					int selected = string.IsNullOrEmpty(stringProperty.stringValue) ? 0 : Array.IndexOf(allTypes, currentType);
					int newSelection = EditorGUI.Popup(_position, _label.text, selected, allTypes);

					stringProperty.stringValue = allTypes[Mathf.Max(0, newSelection)];
				}
				else
				{
					GUI.Label(_position, "The TypeDropDownAttribute only works on strings.");
				}
			}

			EditorGUI.EndProperty();
		}
	}
}
