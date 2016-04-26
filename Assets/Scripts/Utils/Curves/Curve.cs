using UnityEngine;
using System.Collections;

public class CurveArrayData
{
	public Vector3 StartPosition { set; get; }
	public Vector3 EndPosition { set; get; }

	public float Start { set; get; }
	public float End { set; get; }
	public float Lenght { set; get; }
	public float Direction { set; get; }

	public CurveArrayData(float _start, float _end, float _direction, Vector3 _posStart, Vector3 _posEnd)
	{
		Start = _start;
		End = _end;
		Lenght = _end - _start;
		Direction = _direction;
		StartPosition = _posStart;
		EndPosition = _posEnd;
	}

	public CurveData GetData(float _pos)
	{
		_pos = (_pos - Start) / Lenght;
		return new CurveData((StartPosition * (1f - _pos)) + (EndPosition * _pos), Direction);
	}

	public CurveData GetStart()
	{
		return new CurveData(StartPosition, Direction);
	}
}

public class CurveData
{
	public Vector3 Position { set; get; }

	public float Direction { set; get; }

	public CurveData(Vector3 _pos, float _direction)
	{
		Position = _pos;
		Direction = _direction;
	}
}

public class Curve
{
	public float Length { private set; get; }

	private CurveArrayData[] array;
	private Vector3 endPos;
	
	private float endDir;

	private int arrayLenght;

	public Curve(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d, int _resolution = 40)
	{
		arrayLenght = _resolution;
		array = new CurveArrayData[arrayLenght];
		Recalculate(_a, _b, _c, _d);
	}

	public void Recalculate(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d)
	{
		Vector3 pos = Bezier.Get4(_a, _b, _c, _d, 0f);
		Vector3 nextPos;
		Vector3 dir;
		float rotY;
		float dist = 0f;
		Length = 0f;

		for(int i = 1; i < arrayLenght + 1; i++)
		{
			dir = Bezier.GetFirstDerivative4(_a, _b, _c, _d, (i - 1) / ((float)arrayLenght)).normalized;
			rotY = dir.ToRotationY();
			nextPos = Bezier.Get4(_a, _b, _c, _d, i / ((float)arrayLenght));
			dist = MathHelper.Dist3(pos, nextPos);

			array[i - 1] = new CurveArrayData(Length, Length + dist, rotY, pos, nextPos);
			Length += dist;
			pos = nextPos;
		}

		endPos = pos;
		endDir = Bezier.GetFirstDerivative4(_a, _b, _c, _d, 1f).normalized.ToRotationY();
	}

	public CurveData GetDataAt(float _curvePos)
	{
		if(_curvePos <= 0f)
		{
			return array[0].GetStart();
		}

		for(int i = 0; i < arrayLenght; i++)
		{
			if(array[i].Start <= _curvePos && array[i].End > _curvePos)
			{
				return array[i].GetData(_curvePos);
			}
		}

		return new CurveData(endPos, endDir);
	}
}
