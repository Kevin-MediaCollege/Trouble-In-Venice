using UnityEngine;
using System.Collections;
using Utils;

namespace Proeve
{
	public class ChallengeRose : ChallengeBase 
	{
		public override bool getStar()
		{
			return StatTracker.GetTracker<RosePickupTracker>().GetValue();
		}

		public override string getString()
		{
			return "Pickup the rose";
		}
	}
}
