using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class LinkPickup : Pickup
	{
		[SerializeField] private Animator animator;
		[SerializeField] private LinkPickupConnection[] connections;

		/// <summary>
		/// 
		/// </summary>
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
