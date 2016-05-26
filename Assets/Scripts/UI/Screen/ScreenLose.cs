using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Utils;

namespace Proeve
{
	public class ScreenLose : ScreenBase
	{
		public Touchable temp_button;

		protected void Awake()
		{
			if (Application.isMobilePlatform)
			{ 
				temp_button.OnPointerUpEvent += OnButtonTemp;
			} 
			else 
			{
				temp_button.OnPointerDownEvent += OnButtonTemp;
			}
		}

		private void OnButtonTemp(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			SceneManager.LoadScene ("Menu");
		}

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
		}

		/// <summary>
		/// Called when switched to other screen
		/// </summary>
		public override IEnumerator OnScreenFadeout()
		{
			yield break;
		}

		/// <summary>
		/// Called after OnScreenFadeout
		/// </summary>
		public override void OnScreenExit()
		{
		}

		/// <summary>
		/// Returns name of the screen
		/// </summary>
		public override string GetScreenName()
		{
			return "ScreenLose";
		}
	}
}

