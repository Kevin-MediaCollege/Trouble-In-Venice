using UnityEngine;
using System.Collections;

namespace Proeve
{
	/// <summary>
	/// Base screen functions, Used by screenManager
	/// </summary>
	public class ScreenBase : MonoBehaviour
	{
		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public virtual void OnScreenEnter()
		{
		}

		/// <summary>
		/// Called when switched to other screen
		/// </summary>
		public virtual IEnumerator OnScreenFadeout()
		{
			yield break;
		}

		/// <summary>
		/// Called after OnScreenFadeout
		/// </summary>
		public virtual void OnScreenExit()
		{
		}

		/// <summary>
		/// Returns name of the screen
		/// </summary>
		public virtual string GetScreenName()
		{
			return "NULL";
		}
	}
}
