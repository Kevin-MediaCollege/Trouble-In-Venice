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
		/// 
		/// </summary>
		public Type BaseType { private set; get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_baseType"></param>
		public TypeDropdownAttribute(Type _baseType)
		{
			BaseType = _baseType;
		}
	}
}
