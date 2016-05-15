using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A gate link connection, uses the Animator to open or close a gate/entrance.
	/// </summary>
	public class LinkPickupConnectionGate : LinkPickupConnection
	{
		[SerializeField] private Animator animator;
		[SerializeField] private GridDirection direction;
		[SerializeField] private bool startOpen;

		private GridNode blockedNode;

		protected override void Awake()
		{
			base.Awake();

			blockedNode = GridUtils.GetConnectionInDirection(node, GridUtils.GetDirectionVector(direction));

			if(blockedNode != null && !startOpen)
			{
				node.AddBlockade(blockedNode);
			}
			
			if(startOpen)
			{
				animator.SetTrigger("StartOpen");
			}

			animator.SetBool("Open", startOpen);
		}
		
		public override void OnPickup()
		{
			bool open = animator.GetBool("Open");

			if(open)
			{
				node.AddBlockade(blockedNode);
			}
			else
			{
				node.RemoveBlockade(blockedNode);
			}

			animator.SetBool("Open", !open);
		}
	}
}
