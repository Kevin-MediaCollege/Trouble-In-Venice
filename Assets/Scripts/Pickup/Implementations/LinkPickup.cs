using System.Collections.Generic;
using Snakybo.Audio;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A link pickup, activates a <see cref="LinkPickupConnection"/> when the player <see cref="Utils.Entity"/> activates this pickup.
	/// </summary>
	public class LinkPickup : Pickup
	{
		[SerializeField] private LinkPickupConnection[] connections;

		[SerializeField] private Animator animator;
		[SerializeField] private new AudioObject audio;		

		protected override void OnActivate()
		{
			audio.Play();
			animator.SetTrigger("Switch");

			foreach(LinkPickupConnection connection in connections)
			{
				connection.OnPickup();
			}
		}
	}
}
