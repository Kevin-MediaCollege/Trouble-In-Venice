using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A gate link connection, uses the Animator to open or close a gate/entrance.
	/// </summary>
	public class LinkPickupConnectionGate : LinkPickupConnection
	{
		[SerializeField] private Animator animator;
		[SerializeField] private bool startOpen;

		protected override void Awake()
		{
			base.Awake();

			node.Active = startOpen;
			animator.SetBool("Open", startOpen);
		}

		protected void OnValidate()
		{
			animator.SetBool("Open", startOpen);
		}
		
		public override void OnPickup()
		{
			node.Active = !node.Active;		
			animator.SetBool("Open", !animator.GetBool("Open"));
		}
	}
}
