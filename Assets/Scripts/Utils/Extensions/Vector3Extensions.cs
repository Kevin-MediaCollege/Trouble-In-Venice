using System;
using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Vector Math helper
	/// </summary>
	public static class Vector3Extensions
	{
		/// <summary>
		/// distance from 0,0 to _v vector
		/// </summary>
		/// <param name="_v">distance of the vector to calculate</param>
		/// <returns>returns the distance from 0.0 to the vector2</returns>
		public static float Vec2Length(this Vector3 _v)
		{
			return MathHelper.Dist2(0f, 0f, _v.x, _v.y);
		}

		/// <summary>
		/// distance from 0,0 to _v vector
		/// </summary>
		/// <param name="_v">distance of the vector to calculate</param>
		/// <returns>returns the distance from 0.0 to the vector3</returns>
		public static float Vec3Length(this Vector3 _v)
		{
			return MathHelper.Dist3(0f, 0f, 0f, _v.x, _v.y, _v.z);
		}

		/// <summary>
		/// Rotation of 0,0 to vector3
		/// </summary>
		/// <param name="_v">rotation to vector3</param>
		/// <returns>rotation</returns>
		public static float ToRotationY(this Vector3 _v)
		{
			return Mathf.Atan2(_v.z, _v.x) * 180f / Mathf.PI;
		}
	}
}
