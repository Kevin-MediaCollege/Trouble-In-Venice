using UnityEngine;

namespace Utils
{
	public static class GizmosUtils
	{
		public const float SEGMENT_ANGLE_THRESHOLD = 6f;
		public static readonly int CIRCLE_SEGMENTS;
		public static readonly float CIRCLE_SEGMENT_ANGLE;

		static GizmosUtils()
		{
			CIRCLE_SEGMENTS = Mathf.CeilToInt(360f / SEGMENT_ANGLE_THRESHOLD);
			CIRCLE_SEGMENT_ANGLE = 360f / CIRCLE_SEGMENTS;
		}

		public static void DrawRingXZ(Vector3 _p, float _r1, float _r2)
		{
			const int step = 12;
			Quaternion q = Quaternion.Euler(0f, step, 0f);
			Vector3 a1 = new Vector3(_r1, 0f, 0f);
			Vector3 a0;
			Vector3 b1 = new Vector3(_r2, 0f, 0f);
			Vector3 b0;
			for(int i = step; i <= 360; i += step)
			{
				a0 = q * a1;
				b0 = q * b1;
				Gizmos.DrawLine(_p + a1, _p + a0);        // ring 1
				Gizmos.DrawLine(_p + b1, _p + b0);        // ring 2
				Gizmos.DrawLine(_p + a1, _p + b1);        // connector
				a1 = a0;
				b1 = b0;
			}
		}

		public static void DrawArrowXZ(Vector3 _p, Vector3 _direction)
		{
			DrawArrow(_p, _direction, .3f, 0f, Vector3.up);
		}

		public static void DrawArrowXZ(Vector3 _p, Vector3 _direction, float _baseWidth)
		{
			DrawArrow(_p, _direction, _baseWidth, 0f, Vector3.up);
		}

		public static void DrawArrowXZ(Vector3 _p, Vector3 _direction, float _baseWidth, float _headLength)
		{
			DrawArrow(_p, _direction, _baseWidth, _headLength, Vector3.up);
		}

		public static void DrawArrowXY(Vector3 _p, Vector3 _direction)
		{
			DrawArrow(_p, _direction, .3f, 0f, Vector3.forward);
		}

		public static void DrawArrowXY(Vector3 _p, Vector3 _direction, float _baseWidth)
		{
			DrawArrow(_p, _direction, _baseWidth, 0f, Vector3.forward);
		}

		public static void DrawArrowXY(Vector3 _p, Vector3 _direction, float _baseWidth, float _headLength)
		{
			DrawArrow(_p, _direction, _baseWidth, _headLength, Vector3.forward);
		}

		public static void DrawArrow(Vector3 _p, Vector3 _direction, float _baseWidth, float _headLength, Vector3 _normal)
		{
			DrawGradientArrow(_p, _direction, _baseWidth, _headLength, _normal, Gizmos.color, Gizmos.color, 1);
		}
		public static void DrawGradientArrow(Vector3 _p, Vector3 _direction, float _baseWidth, float _headLength, Vector3 _normal, Color _baseColor, Color _tipColor)
		{
			DrawGradientArrow(_p, _direction, _baseWidth, _headLength, _normal, _baseColor, _tipColor, 20);
		}
		public static void DrawGradientArrow(Vector3 _p, Vector3 _direction, float _baseWidth, float _headLength, Vector3 _normal, Color _baseColor, Color _tipColor, int _segments)
		{
			float l = _direction.magnitude;
			float baseL = _headLength == 0f ? .6f : (l - _headLength) / l;
			Vector3 crossB = Vector3.Cross(_direction.normalized, _normal) * _baseWidth;

			Vector3 p0 = _p - (crossB * .25f);
			Vector3 p1 = _p + (crossB * .25f);
			Vector3 p2 = p1 + (_direction * baseL);
			Vector3 p3 = p2 + (crossB * .25f);
			Vector3 p4 = _p + _direction;
			Vector3 p5 = p3 - (crossB);
			Vector3 p6 = p2 - (crossB * .5f);

			Color gizCol = Gizmos.color;
			Gizmos.color = _baseColor;
			Gizmos.DrawLine(p0, p1);
			DrawGradientLine(p1, p2, _baseColor, _tipColor, _segments, crossB * -.5f);
			Gizmos.color = _tipColor;
			Gizmos.DrawLine(p2, p3);
			Gizmos.DrawLine(p3, p4);
			Gizmos.DrawLine(p4, p5);
			Gizmos.DrawLine(p5, p6);
			Gizmos.color = gizCol;
		}

		public static void DrawGradientLine(Vector3 _from, Vector3 _to, Color _color1, Color _color2, int _segments, params Vector3[] _offsets)
		{
			Color gizCol = Gizmos.color;
			_segments = Mathf.Max(_segments, 1);
			Vector3 dir = (_to - _from) / _segments;
			float step = (_segments == 1) ? 0f : 1f / (_segments - 1);

			for(int i = 0; i < _segments; i++)
			{
				Gizmos.color = Color.Lerp(_color1, _color2, i * step);
				Vector3 p1 = _from + i * dir;
				Vector3 p2 = _from + (i + 1) * dir;
				Gizmos.DrawLine(p1, p2);
				foreach(Vector3 v in _offsets)
				{
					Gizmos.DrawLine(p1 + v, p2 + v);
				}
			}

			Gizmos.color = gizCol;
		}
	}
}
