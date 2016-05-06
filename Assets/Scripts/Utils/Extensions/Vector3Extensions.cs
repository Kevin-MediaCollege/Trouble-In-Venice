using System;
using UnityEngine;

namespace Utils
{
	/// <summary>
	/// 
	/// </summary>
	public static class Vector3Extensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_v"></param>
		/// <returns></returns>
		public static float Vec2Length(this Vector3 _v)
		{
			return MathHelper.Dist2(0f, 0f, _v.x, _v.y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_v"></param>
		/// <returns></returns>
		public static float Vec3Length(this Vector3 _v)
		{
			return MathHelper.Dist3(0f, 0f, 0f, _v.x, _v.y, _v.z);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_v"></param>
		/// <returns></returns>
		public static float ToRotationY(this Vector3 _v)
		{
			return Mathf.Atan2(_v.z, _v.x) * 180f / Mathf.PI;
		}
	}
}
