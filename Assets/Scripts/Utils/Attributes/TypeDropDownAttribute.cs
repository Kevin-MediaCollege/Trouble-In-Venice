using UnityEngine;
using System;

namespace Utils
{
	/// <summary>
	/// A TypeDropDownAttribute. Creates a nice dropdown list containing all types inheriting from the given type.
	/// </summary>
	public class TypeDropdownAttribute : PropertyAttribute
	{
		/// <summary>
		/// The base type.
		/// </summary>
		public Type BaseType { private set; get; }

		/// <summary>
		/// Create a new TypeDropdownAttribute.
		/// </summary>
		/// <param name="_baseType">The base type.</param>
		public TypeDropdownAttribute(Type _baseType)
		{
			BaseType = _baseType;
		}
	}
}
