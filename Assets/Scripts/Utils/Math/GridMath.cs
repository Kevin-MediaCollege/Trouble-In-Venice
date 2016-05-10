using UnityEngine;
using System.Collections;

namespace Utils
{
	/// <summary>
	/// Grid calculation tools
	/// </summary>
	public class GridMath
	{
		/// <summary>
		/// Converts a world position to a grid position
		/// </summary>
		/// <param name="_world">World position</param>
		/// <returns>returns world position</returns>
		public static Vector2 WorldToGrid(Vector3 _world)
		{
			return new Vector2(Mathf.Round((_world.x - 1.5f) * 0.33333f), Mathf.Round((_world.z - 1.5f) * 0.33333f));
		}

		/// <summary>
		/// Converts a grid position to a world position
		/// </summary>
		/// <param name="_grid">Grid position</param>
		/// <returns>world position</returns>
		public static Vector3 GridToWorld(Vector2 _grid)
		{
			return new Vector3((_grid.x * 3f) + 1.5f, 0f, (_grid.y * 3f) + 1.5f);
		}
	}
}
