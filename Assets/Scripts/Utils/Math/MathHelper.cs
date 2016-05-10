using UnityEngine;
using System.Collections;

namespace Utils
{
	/// <summary>
	/// Math helper
	/// </summary>
	public static class MathHelper
	{
		/// <summary>
		/// Cos calculation
		/// </summary>
		/// <param name="_dir">direction</param>
		/// <returns>cos</returns>
		public static float Cos(float _dir)
		{
			return Mathf.Cos(_dir * Mathf.PI / 180f);
		}

		/// <summary>
		/// Sin calculation
		/// </summary>
		/// <param name="_dir">direction</param>
		/// <returns>sin</returns>
		public static float Sin(float _dir)
		{
			return Mathf.Sin(_dir * Mathf.PI / 180f);
		}

		/// <summary>
		/// Distance between 2 numbers
		/// </summary>
		/// <param name="_a">Number 1</param>
		/// <param name="_b">Number 2</param>
		/// <returns>Returns distance between numbers</returns>
		public static float Dist1(float _a, float _b)
		{
			return Mathf.Sqrt((_a - _b) * (_a - _b));
		}

		/// <summary>
		/// Distance between 2 vec2 coordinates
		/// </summary>
		/// <param name="_v1">Vector 1</param>
		/// <param name="_v2">Vector 2</param>
		/// <returns>returns distance between 2 vec2's</returns>
		public static float Dist2(Vector2 _v1, Vector2 _v2)
		{
			return Dist2(_v1.x, _v1.y, _v2.x, _v2.y);
		}

		/// <summary>
		/// Distance between 2 2d coordinates
		/// </summary>
		/// <param name="_x1">Coordinate 1 X</param>
		/// <param name="_y1">Coordinate 1 Y</param>
		/// <param name="_x2">Coordinate 2 X</param>
		/// <param name="_y2">Coordinate 2 Y</param>
		/// <returns>Returns distance between 2d coordinates</returns>
		public static float Dist2(float _x1, float _y1, float _x2, float _y2)
		{
			return Mathf.Sqrt((_x1 - _x2) * (_x1 - _x2) + (_y1 - _y2) * (_y1 - _y2));
		}

		/// <summary>
		/// Distance between 2 vec3 coordinates
		/// </summary>
		/// <param name="_v1">Vector 1</param>
		/// <param name="_v2">Vector 2</param>
		/// <returns>returns distance between 2 vec3's</returns>
		public static float Dist3(Vector3 _v1, Vector3 _v2)
		{
			return Dist3(_v1.x, _v1.y, _v1.z, _v2.x, _v2.y, _v2.z);
		}

		/// <summary>
		/// Distance between 2 3d coordinates
		/// </summary>
		/// <param name="_x1">Coordinate 1 X</param>
		/// <param name="_y1">Coordinate 1 Y</param>
		/// <param name="_z1">Coordinate 1 Z</param>
		/// <param name="_x2">Coordinate 2 X</param>
		/// <param name="_y2">Coordinate 2 Y</param>
		/// <param name="_z2">Coordinate 2 Z</param>
		/// <returns>Returns distance between 3d coordinates</returns>
		public static float Dist3(float _x1, float _y1, float _z1, float _x2, float _y2, float _z2)
		{
			return Mathf.Sqrt((_x1 - _x2) * (_x1 - _x2) + (_y1 - _y2) * (_y1 - _y2) + (_z1 - _z2) * (_z1 - _z2));
		}

		/// <summary>
		/// Calculates the rotation between 2 points
		/// </summary>
		/// <param name="v1">Vector2 from</param>
		/// <param name="v2">Vector2 to</param>
		/// <returns>returns rotation</returns>
		public static float PointToRotation(Vector2 v1, Vector2 v2)
		{
			return PointToRotation(v1.x, v1.y, v2.x, v2.y);
		}

		/// <summary>
		/// Calculates the rotation between 2 points
		/// </summary>
		/// <param name="_x1">x from</param>
		/// <param name="_y1">y from</param>
		/// <param name="_x2">x to</param>
		/// <param name="_y2">y to</param>
		/// <returns>returns rotation</returns>
		public static float PointToRotation(float _x1, float _y1, float _x2, float _y2)
		{
			return Mathf.Atan2((_y2 - _y1), (_x2 - _x1)) * 180f / Mathf.PI;
		}

		/// <summary>
		/// Coverts Vec2 to a Vec3
		/// </summary>
		/// <param name="_v2">Vector2</param>
		/// <returns>returns a vector 2 with x z</returns>
		public static Vector3 XYtoXZ(Vector2 _v2)
		{
			return new Vector3(_v2.x, 0f, _v2.y);
		}

		/// <summary>
		/// Puts x and z from a vec3 in a vec2
		/// </summary>
		/// <param name="_v3">Vector3</param>
		/// <returns>Returns the vector2</returns>
		public static Vector2 XZtoXY(Vector3 _v3)
		{
			return new Vector2(_v3.x, _v3.z);
		}
	}
}
