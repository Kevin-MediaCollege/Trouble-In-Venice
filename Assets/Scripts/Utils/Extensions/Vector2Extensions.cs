using System;
using UnityEngine;

public static class Vector2Extensions
{
	public static bool AlmostEquals(this Vector2 _lhs, Vector2 _rhs, float _epsilon)
	{
		return (Math.Abs(_lhs.x) - Math.Abs(_rhs.x) <= _epsilon) && (Math.Abs(_lhs.y) - Math.Abs(_rhs.y) <= _epsilon);
	}
}