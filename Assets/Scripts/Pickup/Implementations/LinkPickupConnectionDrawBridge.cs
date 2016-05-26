using System;
using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	public class LinkPickupConnectionDrawBridge : LinkPickupConnection
	{
		[SerializeField] private GridNode[] nodes;

		[SerializeField] private Animator animator;
		[SerializeField] private bool startOpen;

		protected override void Awake()
		{
			base.Awake();

			if(startOpen)
			{
				animator.SetTrigger("StartOpen");
			}
			
			SetOpen(startOpen);
		}

		public override void OnPickup()
		{
			SetOpen(!animator.GetBool("Open"));
		}
		
		private void SetOpen(bool open)
		{
			animator.SetBool("Open", open);

			foreach(GridNode node in nodes)
			{
				node.Active = !open;
			}
		}
	}
}
