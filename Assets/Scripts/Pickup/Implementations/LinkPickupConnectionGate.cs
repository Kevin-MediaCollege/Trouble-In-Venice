using UnityEngine;
using System.Collections;
using System;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class LinkPickupConnectionGate : LinkPickupConnection
	{
		[SerializeField] private Animator animator;
		[SerializeField] private bool startOpen;

		private EntityNodeTracker nodeTracker;

		protected void Awake()
		{
			nodeTracker = GetComponent<EntityNodeTracker>();
		
			nodeTracker.CurrentNode.Active = startOpen;
			animator.SetBool("Open", startOpen);
		}

		protected void OnValidate()
		{
			animator.SetBool("Open", startOpen);
		}

		/// <summary>
		/// 
		/// </summary>
		public override void OnPickup()
		{
			nodeTracker.CurrentNode.Active = !nodeTracker.CurrentNode.Active;		
			animator.SetBool("Open", !animator.GetBool("Open"));
		}
	}
}
