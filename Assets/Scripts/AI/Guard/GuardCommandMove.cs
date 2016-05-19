using System;
using System.Collections.Generic;

namespace Proeve
{
	public class GuardCommandMove : AICommand
	{
		public override void Execute(AIBase _ai)
		{
			Guard guard = _ai as Guard;

			while(!guard.Movement.CanMove(GridUtils.GetDirectionVector(guard.Direction)))
			{
				switch(guard.Direction)
				{
				case GridDirection.Up:
					guard.Direction = GridDirection.Down;
					break;
				case GridDirection.Left:
					guard.Direction = GridDirection.Right;
					break;
				case GridDirection.Down:
					guard.Direction = GridDirection.Up;
					break;
				case GridDirection.Right:
					guard.Direction = GridDirection.Left;
					break;
				}
			}

			guard.Movement.Move(GridUtils.GetDirectionVector(guard.Direction));
			guard.FindTargetNode();
		}
	}
}
