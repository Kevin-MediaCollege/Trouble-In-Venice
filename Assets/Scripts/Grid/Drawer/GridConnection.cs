using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Proeve
{
	public class GridConnection : MonoBehaviour
	{
		public class Entry
		{
			public GridNode Start { private set; get; }
			public GridNode End { private set; get; }

			public int[] StartIndices { set; get; }
			public int[] EndIndices { set; get; }

			public Vector3 StartPosition
			{
				get
				{
					return Start.Position;
				}
			}

			public Vector3 EndPosition
			{
				get
				{
					return Start.Position + ((End.Position - Start.Position) * 0.5f);
				}
			}

			public Entry(GridNode _start, GridNode _end)
			{
				Start = _start;
				End = _end;
			}
		}

		private List<Entry> entries;
		private GridNode node;

		private bool wasActive;

		protected void Awake()
		{
			node = GetComponent<GridNode>();
			entries = new List<Entry>();

			foreach(GridNode connection in node.Connections)
			{
				Entry entry = new Entry(node, connection);
				GridConnectionDrawer.RegisterEntry(entry);

				entries.Add(entry);
			}

			wasActive = node.Active;
		}

		protected void OnEnable()
		{
			if(node.Active)
			{
				foreach(Entry entry in entries)
				{
					if(node.HasConnection(entry.End) && entry.End.Active)
					{
						StartCoroutine(GridConnectionDrawer.Start(entry));
					}
				}
			}

			node.onBlockadeAddedEvent += OnBlockadeAdded;
			node.onBlockadeRemovedEvent += OnBlockadeRemoved;
		}

		protected void OnDisable()
		{
			node.onBlockadeAddedEvent -= OnBlockadeAdded;
			node.onBlockadeRemovedEvent -= OnBlockadeRemoved;
		}

		protected void LateUpdate()
		{
			if(wasActive && !node.Active)
			{
				foreach(GridNode n in node.Connections)
				{
					n.AddBlockade(node);
				}
			}
			else if(!wasActive && node.Active)
			{
				foreach(GridNode n in node.Connections)
				{
					n.RemoveBlockade(node);
				}
			}

			wasActive = node.Active;
		}

		private void OnBlockadeRemoved(GridNode to)
		{
			Entry entry = FindEntry(to);
			if(entry != null)
			{
				StartCoroutine(GridConnectionDrawer.Start(entry));
			}
		}

		private void OnBlockadeAdded(GridNode to)
		{
			Entry entry = FindEntry(to);
			if(entry != null)
			{
				StartCoroutine(GridConnectionDrawer.End(entry));
			}
		}

		private Entry FindEntry(GridNode to)
		{
			foreach(Entry entry in entries)
			{
				if(entry.End == to)
				{
					return entry;
				}
			}

			return null;
		}
	}
}
