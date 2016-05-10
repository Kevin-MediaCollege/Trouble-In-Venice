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
		
		public override void OnPickup()
		{
			nodeTracker.CurrentNode.Active = !nodeTracker.CurrentNode.Active;		
			animator.SetBool("Open", !animator.GetBool("Open"));
		}
	}
}
