using DG;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Proeve
{
	/// <summary>
	/// Manges main menu screen UI.
	/// </summary>
	public class ScreenMainMenu : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("button_play")] private Touchable buttonPlay;
		[SerializeField, FormerlySerializedAs("button_settings")] private Touchable buttonSettings;
		[SerializeField, FormerlySerializedAs("button_credits")] private Touchable buttonCredits;

		[SerializeField] private CanvasGroup group;

		protected void OnEnable()
		{
			if(Application.isMobilePlatform)
			{ 
				buttonPlay.OnPointerUpEvent += OnButtonPlay; 
				buttonSettings.OnPointerUpEvent += OnButtonSettings; 
				buttonCredits.OnPointerUpEvent += OnButtonCredits; 
			} 
			else 
			{ 
				buttonPlay.OnPointerDownEvent += OnButtonPlay;
				buttonSettings.OnPointerDownEvent += OnButtonSettings;
				buttonCredits.OnPointerDownEvent += OnButtonCredits;
			}
		}

		protected void OnDisable()
		{
			if(Application.isMobilePlatform)
			{
				buttonPlay.OnPointerUpEvent -= OnButtonPlay;
				buttonSettings.OnPointerUpEvent -= OnButtonSettings;
				buttonCredits.OnPointerUpEvent -= OnButtonCredits;
			}
			else
			{
				buttonPlay.OnPointerDownEvent -= OnButtonPlay;
				buttonSettings.OnPointerDownEvent -= OnButtonSettings;
				buttonCredits.OnPointerDownEvent -= OnButtonCredits;
			}
		}

		public override void OnScreenEnter()
		{
			StartCoroutine("OnFadeIn");
		}

		public override IEnumerator OnScreenFadeout()
		{
			group.alpha = 1f;
			group.DOFade (0f, 0.3f);

			yield return new WaitForSeconds (0.3f);
		}

		public override string GetScreenName()
		{
			return "ScreenMainMenu";
		}

		private void OnButtonPlay(Touchable _sender, PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenCharacterSelection");
		}

		private void OnButtonSettings(Touchable _sender, PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenSettings");
		}

		private void OnButtonCredits(Touchable _sender, PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenCredits");
		}

		private IEnumerator OnFadeIn()
		{
			group.alpha = 0f;
			group.DOFade (1f, 0.3f);

			yield return new WaitForSeconds (0.3f);
		}
	}
}
