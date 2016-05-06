using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	public class LinkPickup : Pickup
	{
		[SerializeField] private LinkPickupConnection[] connections;

		protected override void OnActivate()
		{
			foreach(LinkPickupConnection connection in connections)
			{
				connection.OnPickup();
			}
		}
	}
}
