using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A link pickup, activates a <see cref="LinkPickupConnection"/> when the player <see cref="Utils.Entity"/> activates this pickup.
	/// </summary>
	public class LinkPickup : Pickup
	{
		[SerializeField] private Animator animator;
		[SerializeField] private LinkPickupConnection[] connections;

		protected override void OnActivate()
		{
			animator.SetTrigger("Switch");

			foreach(LinkPickupConnection connection in connections)
			{
				connection.OnPickup();
			}
		}
	}
}
