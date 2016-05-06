using UnityEngine;
using System.Collections;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenMainMenu : ScreenBase
	{
		/// <summary>
		/// 
		/// </summary>
		public override void OnScreenEnter()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override IEnumerator OnScreenFadeout()
		{
			yield break;
		}

		/// <summary>
		/// 
		/// </summary>
		public override void OnScreenExit()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string GetScreenName()
		{
			return "ScreenMainMenu";
		}
	}
}
