using System;
using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	public class GuardCommandRotate : AICommand
	{
		public override void Execute(AIBase _ai)
		{
			Guard guard = _ai as Guard;
			Vector2 dir = Vector2.zero;

			while(!guard.Movement.CanMove(dir))
			{
				if((guard.Direction == GridDirection.Up && guard.PatrolMode == Guard.GuardPatrolMode.RotatingClockwise) ||
				   (guard.Direction == GridDirection.Down && guard.PatrolMode == Guard.GuardPatrolMode.RotatingCounterClockwise))
				{
					guard.Direction = GridDirection.Right;
					dir = Vector2.right;
				}
				else if((guard.Direction == GridDirection.Right && guard.PatrolMode == Guard.GuardPatrolMode.RotatingClockwise) ||
						(guard.Direction == GridDirection.Left && guard.PatrolMode == Guard.GuardPatrolMode.RotatingCounterClockwise))
				{
					guard.Direction = GridDirection.Down;
					dir = Vector2.down;
				}
				else if((guard.Direction == GridDirection.Down && guard.PatrolMode == Guard.GuardPatrolMode.RotatingClockwise) ||
						(guard.Direction == GridDirection.Up && guard.PatrolMode == Guard.GuardPatrolMode.RotatingCounterClockwise))
				{
					guard.Direction = GridDirection.Left;
					dir = Vector2.left;
				}
				else if((guard.Direction == GridDirection.Left && guard.PatrolMode == Guard.GuardPatrolMode.RotatingClockwise) ||
						(guard.Direction == GridDirection.Right && guard.PatrolMode == Guard.GuardPatrolMode.RotatingCounterClockwise))
				{
					guard.Direction = GridDirection.Up;
					dir = Vector2.up;
				}
			}

			guard.FindTargetNode();
		}
	}
}
