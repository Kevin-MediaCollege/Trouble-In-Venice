using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Proeve
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class GridConnectionDrawer : MonoBehaviour
	{
		private const float SPEED = 7f;
		private const float LINE_WIDTH = 0.03f;

		public static Mesh Mesh { private set; get; }

		protected void Awake()
		{
			Mesh = new Mesh();

			GetComponent<MeshFilter>().sharedMesh = Mesh;
			GetComponent<MeshRenderer>().material = Resources.Load<Material>("Grid Connection");
		}

		public static void RegisterEntry(GridConnection.Entry _entry)
		{
			// Assign vertex indices
			_entry.StartIndices = new int[] { Mesh.vertexCount, Mesh.vertexCount + 1 };
			_entry.EndIndices = new int[] { Mesh.vertexCount + 2, Mesh.vertexCount + 3 };

			// Create and fill vertices array
			Vector3[] vertices = new Vector3[Mesh.vertices.Length + 4];
			for(int i = 0; i < Mesh.vertices.Length; i++)
			{
				vertices[i] = Mesh.vertices[i];
			}

			// Create and fill indices array
			int[] indices = new int[Mesh.GetIndices(0).Length + 4];
			for(int i = 0; i < Mesh.GetIndices(0).Length; i++)
			{
				indices[i] = Mesh.GetIndices(0)[i];
			}

			Vector3[] startPosition = GetLinePosition(_entry, _entry.StartPosition);

			// Assign new vertices
			vertices[_entry.StartIndices[0]] = startPosition[0];
			vertices[_entry.StartIndices[1]] = startPosition[1];
			vertices[_entry.EndIndices[0]] = startPosition[0];
			vertices[_entry.EndIndices[1]] = startPosition[1];

			// Assign new indices
			indices[indices.Length - 4] = _entry.StartIndices[0];
			indices[indices.Length - 3] = _entry.StartIndices[1];
			indices[indices.Length - 2] = _entry.EndIndices[0];
			indices[indices.Length - 1] = _entry.EndIndices[1];

			// Upload new data
			Mesh.vertices = vertices;
			Mesh.SetIndices(indices, MeshTopology.Quads, 0);

			Mesh.RecalculateNormals();
			Mesh.RecalculateBounds();
		}

		public static IEnumerator Start(GridConnection.Entry _entry)
		{
			float st = Time.time;
			float t = 0;

			float length = (_entry.StartPosition - _entry.EndPosition).sqrMagnitude;

			while(t < 1)
			{
				t = ((Time.time - st) * SPEED) / (length * length);

				Vector3 position = Vector3.Lerp(_entry.StartPosition, _entry.EndPosition, t);
				Vector3[] vertices = Mesh.vertices;

				Vector3[] linePosition = GetLinePosition(_entry, position);

				vertices[_entry.EndIndices[0]] = linePosition[1];
				vertices[_entry.EndIndices[1]] = linePosition[0];
				Mesh.vertices = vertices;

				yield return null;
			}
		}

		public static IEnumerator End(GridConnection.Entry _entry)
		{
			float st = Time.time;
			float t = 0;

			float length = (_entry.StartPosition - _entry.EndPosition).sqrMagnitude;

			while(t < 1)
			{
				t = ((Time.time - st) * SPEED) / (length * length);

				Vector3 position = Vector3.Lerp(_entry.EndPosition, _entry.StartPosition, t);
				Vector3[] vertices = Mesh.vertices;

				Vector3[] linePosition = GetLinePosition(_entry, position);

				vertices[_entry.EndIndices[0]] = linePosition[1];
				vertices[_entry.EndIndices[1]] = linePosition[0];
				Mesh.vertices = vertices;

				yield return null;
			}
		}

		public static IEnumerator Interrupt(GridConnection.Entry _entry)
		{
			yield break;
		}

		public static IEnumerator Restore(GridConnection.Entry _entry)
		{
			yield break;
		}

		private static Vector3[] GetLinePosition(GridConnection.Entry _entry, Vector3 _position)
		{
			Vector3 axis = (_entry.End.GridPosition - _entry.Start.GridPosition).normalized;

			// Add width on Z axis
			if(Mathf.Abs(axis.x) == 1)
			{
				return new Vector3[]
				{
					_position + new Vector3(0, 0, LINE_WIDTH),
					_position + new Vector3(0, 0, -LINE_WIDTH)
				};
			}
			// Add width on X axis
			else
			{
				return new Vector3[]
				{
					_position + new Vector3(LINE_WIDTH, 0, 0),
					_position + new Vector3(-LINE_WIDTH, 0, 0)
				};
			}
		}
	}
}
