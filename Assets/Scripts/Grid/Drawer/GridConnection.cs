using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// Handles connections drawn between grid nodes, registers one or more <see cref="Entry"/> to <see cref="GridConnectionDrawer"/>.
	/// </summary>
	public class GridConnection : MonoBehaviour
	{
		/// <summary>
		/// A grid connection entry. 
		/// <para>
		/// An entry is half a line between <see cref="GridNode"/>s.
		/// </para>
		/// </summary>
		public class Entry
		{
			/// <summary>
			/// The source <see cref="GridNode"/> of the entry.
			/// </summary>
			public GridNode Source { private set; get; }

			/// <summary>
			/// The destination <see cref="GridNode"/> of the entry.
			/// </summary>
			public GridNode Destination { private set; get; }

			/// <summary>
			/// Array of indices of the vertices at the source position.
			/// </summary>
			public int[] StartIndices { set; get; }

			/// <summary>
			/// Array of indices of the vertices at the destination position.
			/// </summary>
			public int[] EndIndices { set; get; }

			/// <summary>
			/// The source position.
			/// </summary>
			public Vector3 SourcePosition
			{
				get
				{
					return Source.Position;
				}
			}

			/// <summary>
			/// The destination position, the destination position is halfway between the <see cref="Source"/> and <see cref="Destination"/> nodes.
			/// </summary>
			public Vector3 DestinationPosition
			{
				get
				{
					return Source.Position + ((Destination.Position - Source.Position) * 0.5f);
				}
			}

			/// <summary>
			/// Create a new entry.
			/// </summary>
			/// <param name="_source">The source <see cref="GridNode"/>.</param>
			/// <param name="_destination">The destination <see cref="GridNode"/></param>
			public Entry(GridNode _source, GridNode _destination)
			{
				Source = _source;
				Destination = _destination;
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
					if(node.HasConnection(entry.Destination) && entry.Destination.Active)
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
			// Add blockades if this node has become inactive.
			if(wasActive && !node.Active)
			{
				foreach(GridNode n in node.Connections)
				{
					n.AddBlockade(node);
				}
			}
			// Remove blockades if this node has become active.
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
				if(entry.Destination == to)
				{
					return entry;
				}
			}

			return null;
		}
	}
}
