using UnityEngine;
using System.Collections;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenBase : MonoBehaviour
	{
		/// <summary>
		/// 
		/// </summary>
		public virtual void OnScreenEnter()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual IEnumerator OnScreenFadeout()
		{
			yield break;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void OnScreenExit()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual string GetScreenName()
		{
			return "NULL";
		}
	}
}
