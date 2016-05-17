using DG;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenMainMenu : ScreenBase
	{
		public Touchable button_play;
		public Touchable button_settings;
		public Touchable button_credits;
		public CanvasGroup group;

		protected void Awake()
		{
			if (Application.isMobilePlatform)
			{ 
				button_play.OnPointerUpEvent += OnButtonPlay; 
				button_settings.OnPointerUpEvent += OnButtonSettings; 
				button_credits.OnPointerUpEvent += OnButtonCredits; 
			} 
			else 
			{ 
				button_play.OnPointerDownEvent += OnButtonPlay;
				button_settings.OnPointerDownEvent += OnButtonSettings;
				button_credits.OnPointerDownEvent += OnButtonCredits;
			}
		}

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
			StartCoroutine ("OnFadeIn");
		}

		private IEnumerator OnFadeIn()
		{
			group.alpha = 0f;
			group.DOFade (1f, 0.3f);
			yield return new WaitForSeconds (0.3f);
		}

		/// <summary>
		/// Called when switched to other screen
		/// </summary>
		public override IEnumerator OnScreenFadeout()
		{
			group.alpha = 1f;
			group.DOFade (0f, 0.3f);
			yield return new WaitForSeconds (0.3f);
		}

		/// <summary>
		/// Called after OnScreenFadeout
		/// </summary>
		public override void OnScreenExit()
		{
		}

		private void OnButtonPlay(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.instance.SwitchScreen ("ScreenLevelSelect");
		}

		private void OnButtonSettings(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.instance.SwitchScreen ("ScreenSettings");
		}

		private void OnButtonCredits(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.instance.SwitchScreen ("ScreenCredits");
		}

		/// <summary>
		/// Returns name of the screen
		/// </summary>
		public override string GetScreenName()
		{
			return "ScreenMainMenu";
		}
	}
}
