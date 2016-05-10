using System;
using UnityEngine;

namespace Utils
{
	/// <summary>
	/// A collection of Vector2 extensions.
	/// </summary>
	public static class Vector2Extensions
	{
		/// <summary>
		/// Check if the difference between <paramref name="_lhs"/> and <paramref name="_rhs"/> is less than <paramref name="_epsilon"/>.
		/// </summary>
		/// <param name="_lhs">The first vector.</param>
		/// <param name="_rhs">The second vector.</param>
		/// <param name="_epsilon">The maximum difference between <paramref name="_lhs"/> and <paramref name="_rhs"/>.</param>
		/// <returns>Whether or not the difference between <paramref name="_lhs"/> and <paramref name="_rhs"/> is less than <paramref name="_epsilon"/>.</returns>
		public static bool AlmostEquals(this Vector2 _lhs, Vector2 _rhs, float _epsilon)
		{
			return (Math.Abs(_lhs.x) - Math.Abs(_rhs.x) <= _epsilon) && (Math.Abs(_lhs.y) - Math.Abs(_rhs.y) <= _epsilon);
		}
	}
}
