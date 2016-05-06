using UnityEngine;
using System.Collections;

namespace Utils
{
	/// <summary>
	/// 
	/// </summary>
	public static class Bezier
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_a"></param>
		/// <param name="_b"></param>
		/// <param name="_c"></param>
		/// <param name="_t"></param>
		/// <returns></returns>
		public static Vector3 Get3(Vector3 _a, Vector3 _b, Vector3 _c, float _t)
		{
			return Vector3.Lerp(Vector3.Lerp(_a, _b, _t), Vector3.Lerp(_b, _c, _t), _t);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_a"></param>
		/// <param name="_b"></param>
		/// <param name="_c"></param>
		/// <param name="_d"></param>
		/// <param name="_t"></param>
		/// <returns></returns>
		public static Vector3 Get4(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d, float _t)
		{
			_t = Mathf.Clamp01(_t);
			float oneMinusT = 1f - _t;
			return oneMinusT * oneMinusT * oneMinusT * _a + 3f * oneMinusT * oneMinusT * _t * _b + 3f * oneMinusT * _t * _t * _c + _t * _t * _t * _d;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_a"></param>
		/// <param name="_b"></param>
		/// <param name="_c"></param>
		/// <param name="_t"></param>
		/// <returns></returns>
		public static Vector3 GetFirstDerivative3(Vector3 _a, Vector3 _b, Vector3 _c, float _t)
		{
			return 2f * (1f - _t) * (_b - _a) + 2f * _t * (_c - _b);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_a"></param>
		/// <param name="_b"></param>
		/// <param name="_c"></param>
		/// <param name="_d"></param>
		/// <param name="_t"></param>
		/// <returns></returns>
		public static Vector3 GetFirstDerivative4(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d, float _t)
		{
			_t = Mathf.Clamp01(_t);
			float oneMinusT = 1f - _t;
			return 3f * oneMinusT * oneMinusT * (_b - _a) + 6f * oneMinusT * _t * (_c - _b) + 3f * _t * _t * (_d - _c);
		}
	}
}
