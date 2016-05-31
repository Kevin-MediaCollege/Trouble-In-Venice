using UnityEngine;
using System.Collections;
using Utils;

namespace Proeve
{
	public class ChallengeTurns : ChallengeBase 
	{
		public int numberOfTurns = 1;

		public override bool getStar()
		{
			if (numberOfTurns <= StatTracker.GetTracker<TurnCountTracker> ().GetValue ())
			{
				return true;
			} 
			else
			{
				return false;
			}
		}
	}
}
