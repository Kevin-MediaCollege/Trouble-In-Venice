using UnityEngine;
using System.Collections;

namespace Utils
{
	/// <summary>
	/// 
	/// </summary>
	public class CurveArrayData
	{
		/// <summary>
		/// 
		/// </summary>
		public Vector3 StartPosition { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public Vector3 EndPosition { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public float Start { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public float End { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public float Lenght { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public float Direction { set; get; }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_start"></param>
		/// <param name="_end"></param>
		/// <param name="_direction"></param>
		/// <param name="_posStart"></param>
		/// <param name="_posEnd"></param>
		public CurveArrayData(float _start, float _end, float _direction, Vector3 _posStart, Vector3 _posEnd)
		{
			Start = _start;
			End = _end;
			Lenght = _end - _start;
			Direction = _direction;
			StartPosition = _posStart;
			EndPosition = _posEnd;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_pos"></param>
		/// <returns></returns>
		public CurveData GetData(float _pos)
		{
			_pos = (_pos - Start) / Lenght;
			return new CurveData((StartPosition * (1f - _pos)) + (EndPosition * _pos), Direction);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public CurveData GetStart()
		{
			return new CurveData(StartPosition, Direction);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class CurveData
	{
		/// <summary>
		/// 
		/// </summary>
		public Vector3 Position { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public float Direction { set; get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_pos"></param>
		/// <param name="_direction"></param>
		public CurveData(Vector3 _pos, float _direction)
		{
			Position = _pos;
			Direction = _direction;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Curve
	{
		/// <summary>
		/// 
		/// </summary>
		public float Length { private set; get; }

		private CurveArrayData[] array;
		private Vector3 endPos;

		private float endDir;

		private int arrayLenght;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_a"></param>
		/// <param name="_b"></param>
		/// <param name="_c"></param>
		/// <param name="_d"></param>
		/// <param name="_resolution"></param>
		public Curve(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d, int _resolution = 40)
		{
			arrayLenght = _resolution;
			array = new CurveArrayData[arrayLenght];
			Recalculate(_a, _b, _c, _d);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_a"></param>
		/// <param name="_b"></param>
		/// <param name="_c"></param>
		/// <param name="_d"></param>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_curvePos"></param>
		/// <returns></returns>
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
}
