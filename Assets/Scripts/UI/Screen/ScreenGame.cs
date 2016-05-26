using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Utils;

namespace Proeve
{
	public class ScreenGame : ScreenBase 
	{
		public Touchable button_reset;
		public Touchable button_options;

		protected void Awake()
		{
			if (Application.isMobilePlatform)
			{ 
				button_reset.OnPointerUpEvent += OnButtonReset;
				button_options.OnPointerUpEvent += OnButtonOptions;
			} 
			else 
			{
				button_reset.OnPointerDownEvent += OnButtonReset;
				button_options.OnPointerDownEvent += OnButtonOptions;
			}
		}

		private void OnButtonReset(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}

		private void OnButtonOptions(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenPause");
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
			return "ScreenGame";
		}
	}
}