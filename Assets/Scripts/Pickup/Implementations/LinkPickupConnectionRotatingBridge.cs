using System.Collections;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// Rotating bridge behaviour.
	/// </summary>
	public class LinkPickupConnectionRotatingBridge : LinkPickupConnection
	{
		[SerializeField] private Animator animator;

		[SerializeField] private GridNode[] side1;
		[SerializeField] private GridNode[] side2;

		private GridNode[] current;

		protected override void Awake()
		{
			base.Awake();

			current = side1;

			SetActive(side2, false);
			SetActive(side1, true);

			animator.SetInteger("Rotation", 1);
			animator.SetTrigger("Rotate");
		}
		
		public override void OnPickup()
		{
			current = current == side1 ? side2 : side1;
			int side = current == side1 ? 1 : 0;

			animator.SetInteger("Rotation", side);
			animator.SetTrigger("Rotate");

			StartCoroutine(Switch(side));
		}

		private IEnumerator Switch(int side)
		{
			if(side == 0)
			{
				SetActive(side1, false);
			}
			else if(side == 1)
			{
				SetActive(side2, false);
			}

			yield return new WaitForSeconds(1);

			if(side == 0)
			{
				SetActive(side2, true);
			}
			else if(side == 1)
			{
				SetActive(side1, true);
			}
		}

		private void SetActive(GridNode[] nodes, bool active)
		{
			foreach(GridNode node in nodes)
			{
				node.Active = active;
			}
		}
	}
}
