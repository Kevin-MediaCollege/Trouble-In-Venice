using System;
using UnityEngine;

public static class Vector2Extensions
{
	public static bool AlmostEquals(this Vector2 lhs, Vector2 rhs, float epsilon)
	{
		return (Math.Abs(lhs.x) - Math.Abs(rhs.x) <= epsilon) && (Math.Abs(lhs.y) - Math.Abs(rhs.y) <= epsilon);
	}
}