using System;
using System.Collections.Generic;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Guard command to attack the player.
	/// </summary>
	public class GuardCommandAttackPlayer : AICommand
	{
		private EntityNodeTracker playerNodeTracker;

		public GuardCommandAttackPlayer()
		{
			Entity player = EntityUtils.GetEntityWithTag("Player");
			playerNodeTracker = player.GetComponent<EntityNodeTracker>();
		}

		public override void Execute(AIBase _ai)
		{
			GlobalEvents.Invoke(new PlayerDiedEvent(playerNodeTracker.CurrentNode, _ai.Entity));
			GlobalEvents.Invoke(new SetInputEvent(false));

			ScreenManager.SwitchScreen("ScreenLose");
		}
	}
}
