using System;
using System.Collections.Generic;
using Snakybo.Audio;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A draw bridge link pickup connection.
	/// </summary>
	public class LinkPickupConnectionDrawBridge : LinkPickupConnection
	{
		[SerializeField] private GridNode[] nodes;

		[SerializeField] private new AudioObject audio;
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
			audio.Play();
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
