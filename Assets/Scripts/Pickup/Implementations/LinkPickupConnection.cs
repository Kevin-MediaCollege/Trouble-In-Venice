using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// A connection for a <see cref="LinkPickup"/>.
	/// </summary>
	public abstract class LinkPickupConnection : GridNodeObject
	{
		/// <summary>
		/// Called by <see cref="LinkPickup.OnActivate"/>.
		/// </summary>
		public abstract void OnPickup();
	}
}
