using UnityEngine;

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

	public static void DrawRingXZ(Vector3 p, float r1, float r2)
	{
		const int step = 12;
		Quaternion q = Quaternion.Euler(0f, step, 0f);
		Vector3 a1 = new Vector3(r1, 0f, 0f);
		Vector3 a0;
		Vector3 b1 = new Vector3(r2, 0f, 0f);
		Vector3 b0;
		for(int i = step; i <= 360; i += step)
		{
			a0 = q * a1;
			b0 = q * b1;
			Gizmos.DrawLine(p + a1, p + a0);        // ring 1
			Gizmos.DrawLine(p + b1, p + b0);        // ring 2
			Gizmos.DrawLine(p + a1, p + b1);        // connector
			a1 = a0;
			b1 = b0;
		}
	}

	public static void DrawArrowXZ(Vector3 p, Vector3 direction)
	{
		DrawArrow(p, direction, .3f, 0f, Vector3.up);
	}

	public static void DrawArrowXZ(Vector3 p, Vector3 direction, float baseWidth)
	{
		DrawArrow(p, direction, baseWidth, 0f, Vector3.up);
	}

	public static void DrawArrowXZ(Vector3 p, Vector3 direction, float baseWidth, float headLength)
	{
		DrawArrow(p, direction, baseWidth, headLength, Vector3.up);
	}

	public static void DrawArrowXY(Vector3 p, Vector3 direction)
	{
		DrawArrow(p, direction, .3f, 0f, Vector3.forward);
	}

	public static void DrawArrowXY(Vector3 p, Vector3 direction, float baseWidth)
	{
		DrawArrow(p, direction, baseWidth, 0f, Vector3.forward);
	}

	public static void DrawArrowXY(Vector3 p, Vector3 direction, float baseWidth, float headLength)
	{
		DrawArrow(p, direction, baseWidth, headLength, Vector3.forward);
	}

	public static void DrawArrow(Vector3 p, Vector3 direction, float baseWidth, float headLength, Vector3 normal)
	{
		DrawGradientArrow(p, direction, baseWidth, headLength, normal, Gizmos.color, Gizmos.color, 1);
	}
	public static void DrawGradientArrow(Vector3 p, Vector3 direction, float baseWidth, float headLength, Vector3 normal, Color baseColor, Color tipColor)
	{
		DrawGradientArrow(p, direction, baseWidth, headLength, normal, baseColor, tipColor, 20);
	}
	public static void DrawGradientArrow(Vector3 p, Vector3 direction, float baseWidth, float headLength, Vector3 normal, Color baseColor, Color tipColor, int segments)
	{
		float l = direction.magnitude;
		float baseL = headLength == 0f ? .6f : (l - headLength) / l;
		Vector3 crossB = Vector3.Cross(direction.normalized, normal) * baseWidth;

		Vector3 p0 = p - (crossB * .25f);
		Vector3 p1 = p + (crossB * .25f);
		Vector3 p2 = p1 + (direction * baseL);
		Vector3 p3 = p2 + (crossB * .25f);
		Vector3 p4 = p + direction;
		Vector3 p5 = p3 - (crossB);
		Vector3 p6 = p2 - (crossB * .5f);

		Color gizCol = Gizmos.color;
		Gizmos.color = baseColor;
		Gizmos.DrawLine(p0, p1);
		DrawGradientLine(p1, p2, baseColor, tipColor, segments, crossB * -.5f);
		Gizmos.color = tipColor;
		Gizmos.DrawLine(p2, p3);
		Gizmos.DrawLine(p3, p4);
		Gizmos.DrawLine(p4, p5);
		Gizmos.DrawLine(p5, p6);
		Gizmos.color = gizCol;
	}

	public static void DrawGradientLine(Vector3 from, Vector3 to, Color color1, Color color2, int segments, params Vector3[] offsets)
	{
		Color gizCol = Gizmos.color;
		segments = Mathf.Max(segments, 1);
		Vector3 dir = (to - from) / segments;
		float step = (segments == 1) ? 0f : 1f / (segments - 1);
		for(int i = 0; i < segments; i++)
		{
			Gizmos.color = Color.Lerp(color1, color2, i * step);
			Vector3 p1 = from + i * dir;
			Vector3 p2 = from + (i + 1) * dir;
			Gizmos.DrawLine(p1, p2);
			foreach(Vector3 v in offsets)
				Gizmos.DrawLine(p1 + v, p2 + v);
		}

		Gizmos.color = gizCol;
	}
}