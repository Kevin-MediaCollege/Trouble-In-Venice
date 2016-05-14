using UnityEngine;
using System.Collections;

namespace Proeve
{
	public class LimitedUsesPickup : Pickup
	{
		[SerializeField] private int uses;

		private int remaining;

		protected override void Awake()
		{
			base.Awake();

			remaining = uses;
		}

		protected override void OnActivate()
		{
			remaining--;

			if(remaining <= 0)
			{
				node.Active = false;
			}
		}
	}
}
