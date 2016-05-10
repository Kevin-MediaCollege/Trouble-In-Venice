using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Mark a field as an Enum flag, when a field has the <see cref="EnumFlagsAttribute"/>, it is shown in the inspector as an enum field popup.
	/// </summary>
	public class EnumFlagsAttribute : PropertyAttribute
	{
	}
}
