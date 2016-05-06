using UnityEngine;
using System.Collections;

namespace Proeve
{
	public class ScreenLevelSelect : ScreenBase
	{
		public override void OnScreenEnter()
		{
		}

		public override IEnumerator OnScreenFadeout()
		{
			yield break;
		}

		public override void OnScreenExit()
		{
		}

		public override string GetScreenName()
		{
			return "ScreenLevelSelect";
		}
	}
}
