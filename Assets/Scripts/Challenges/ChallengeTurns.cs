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
			Debug.Log ("Lenght = " + StatTracker.GetTracker<TurnCountTracker> ().GetValue ());

			if (numberOfTurns <= StatTracker.GetTracker<TurnCountTracker> ().GetValue ())
			{
				return true;
			} 
			else
			{
				return false;
			}
		}

		public override string getString()
		{
			return "Complete level in " + numberOfTurns + " turns";
		}
	}
}
