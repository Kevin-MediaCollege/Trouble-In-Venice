using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class LinkPickup : Pickup
	{
		[SerializeField] private LinkPickupConnection[] connections;

		/// <summary>
		/// 
		/// </summary>
		protected override void OnActivate()
		{
			foreach(LinkPickupConnection connection in connections)
			{
				connection.OnPickup();
			}
		}
	}
}
