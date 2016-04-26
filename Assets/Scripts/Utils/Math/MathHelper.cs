using UnityEngine;
using System.Collections;

public static class MathHelper
{
	public static float Cos(float _dir)
	{
		return Mathf.Cos(_dir * Mathf.PI / 180f);
	}
	
	public static float Sin(float _dir)
	{
		return Mathf.Sin(_dir * Mathf.PI / 180f);
	}
	
	public static float Dist1(float _a, float _b)
	{
		return Mathf.Sqrt((_a - _b) * (_a - _b));
	}

	public static float Dist2(Vector2 _v1, Vector2 _v2)
	{
		return Dist2(_v1.x, _v1.y, _v2.x, _v2.y);
	}
	
	public static float Dist2(float _x1, float _y1, float _x2, float _y2)
	{
		return Mathf.Sqrt((_x1 - _x2) * (_x1 - _x2) + (_y1 - _y2) * (_y1 - _y2));
	}

	public static float Dist3(Vector3 _v1, Vector3 _v2)
	{
		return Dist3(_v1.x, _v1.y, _v1.z, _v2.x, _v2.y, _v2.z);
	}
	
	public static float Dist3(float _x1, float _y1, float _z1, float _x2, float _y2, float _z2)
	{
		return Mathf.Sqrt((_x1 - _x2) * (_x1 - _x2) + (_y1 - _y2) * (_y1 - _y2) + (_z1 - _z2) * (_z1 - _z2));
	}

	public static float PointToRotation(Vector2 v1, Vector2 v2)
	{
		return PointToRotation(v1.x, v1.y, v2.x, v2.y);
	}

	public static float PointToRotation(float _x1, float _y1, float _x2, float _y2)
	{
		return Mathf.Atan2((_y2 - _y1), (_x2 - _x1)) * 180f / Mathf.PI;
	}

	public static Vector3 XYtoXZ(Vector2 _v2)
	{
		return new Vector3 (_v2.x, 0f, _v2.y);
	}

	public static Vector2 XZtoXY(Vector3 _v3)
	{
		return new Vector2 (_v3.x, _v3.z);
	}
}
