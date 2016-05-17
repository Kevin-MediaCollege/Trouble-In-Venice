using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class GridConnectionDrawer : MonoBehaviour
	{
		public static Mesh Mesh { private set; get; }

		protected void Awake()
		{
			Mesh = new Mesh();

			GetComponent<MeshFilter>().sharedMesh = Mesh;
			GetComponent<MeshRenderer>().material = Resources.Load<Material>("Grid Connection");
		}

		public static void RegisterEntry(GridConnection.Entry _entry)
		{
			_entry.StartIndex = Mesh.vertexCount;
			_entry.EndIndex = Mesh.vertexCount + 1;

			Vector3[] vertices = new Vector3[Mesh.vertices.Length + 2];
			for(int i = 0; i < Mesh.vertices.Length; i++)
			{
				vertices[i] = Mesh.vertices[i];
			}

			int[] indices = new int[Mesh.GetIndices(0).Length + 2];
			for(int i = 0; i < Mesh.GetIndices(0).Length; i++)
			{
				indices[i] = Mesh.GetIndices(0)[i];
			}

			vertices[_entry.StartIndex] = _entry.StartPosition;
			vertices[_entry.EndIndex] = _entry.EndPosition;

			indices[indices.Length - 2] = _entry.StartIndex;
			indices[indices.Length - 1] = _entry.EndIndex;

			Mesh.vertices = vertices;
			Mesh.SetIndices(indices, MeshTopology.Lines, 0);
			
			Mesh.RecalculateBounds();
		}

		public static IEnumerator Start(GridConnection.Entry _entry)
		{
			yield break;
		}

		public static IEnumerator End(GridConnection.Entry _entry)
		{
			yield break;
		}

		public static IEnumerator Interrupt(GridConnection.Entry _entry)
		{
			yield break;
		}

		public static IEnumerator Restore(GridConnection.Entry _entry)
		{
			yield break;
		}
	}
}
