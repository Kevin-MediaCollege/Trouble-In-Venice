using UnityEngine;
using System.Collections;

namespace Utils
{
	public class GridMath
	{
		public static Vector2 WorldToGrid(Vector3 _world)
		{
			return new Vector2(Mathf.Round((_world.x - 1.5f) * 0.33333f), Mathf.Round((_world.z - 1.5f) * 0.33333f));
		}

		public static Vector3 GridToWorld(Vector2 _grid)
		{
			return new Vector3((_grid.x * 3f) + 1.5f, 0f, (_grid.y * 3f) + 1.5f);
		}
	}
}
