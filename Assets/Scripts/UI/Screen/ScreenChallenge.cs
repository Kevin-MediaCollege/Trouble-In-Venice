using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Utils;

namespace Proeve
{
	public class ScreenChallenge : ScreenBase
	{
		public Touchable button_start;

		protected void Awake()
		{
			button_start.OnPointerDownEvent += OnButtonStart;
		}

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
			GlobalEvents.Invoke(new SetInputEvent(false));
		}

		void OnButtonStart (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenGame");
			GlobalEvents.Invoke(new SetInputEvent(true));
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
			return "ScreenChallenge";
		}
	}
}

