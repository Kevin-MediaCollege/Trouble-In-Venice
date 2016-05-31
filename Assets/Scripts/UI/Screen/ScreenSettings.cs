using DG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Proeve
{
	/// <summary>
	/// Manages the settings screen UI.
	/// </summary>
	public class ScreenSettings : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("button_back")] private Touchable buttonBack;
		[SerializeField, FormerlySerializedAs("button_music")] private Touchable buttonMusic;
		[SerializeField, FormerlySerializedAs("button_sounds")] private Touchable buttonSounds;

		[SerializeField, FormerlySerializedAs("rect_music")] private RectTransform rectMusic;
		[SerializeField, FormerlySerializedAs("rect_sound")] private RectTransform rectSound;

		[SerializeField, FormerlySerializedAs("image_music")] private Image imageMusic;
		[SerializeField, FormerlySerializedAs("image_sound")] private Image imageSound;

		[SerializeField] private CanvasGroup group;

		protected void OnEnable()
		{
			if(Application.isMobilePlatform)
			{
				buttonBack.OnPointerUpEvent += OnButtonBack;
			}
			else
			{
				buttonBack.OnPointerDownEvent += OnButtonBack;
			}

			buttonMusic.OnPointerDownEvent += OnSliderMusic;
			buttonMusic.OnPointerMoveEvent += OnSliderMusic;
			buttonSounds.OnPointerDownEvent += OnSliderSound;
			buttonSounds.OnPointerMoveEvent += OnSliderSound;
		}

		protected void OnDisable()
		{
			if(Application.isMobilePlatform)
			{
				buttonBack.OnPointerUpEvent -= OnButtonBack;
			}
			else
			{
				buttonBack.OnPointerDownEvent -= OnButtonBack;
			}

			buttonMusic.OnPointerDownEvent -= OnSliderMusic;
			buttonMusic.OnPointerMoveEvent -= OnSliderMusic;
			buttonSounds.OnPointerDownEvent -= OnSliderSound;
			buttonSounds.OnPointerMoveEvent -= OnSliderSound;
		}

		public override void OnScreenEnter()
		{
			StartCoroutine ("OnFadeIn");
		}

		public override IEnumerator OnScreenFadeout()
		{
			group.alpha = 1f;
			group.DOFade (0f, 0.3f);
			yield return new WaitForSeconds (0.3f);
		}

		public override string GetScreenName()
		{
			return "ScreenSettings";
		}

		private void OnSliderMusic(Touchable _sender, PointerEventData _eventData)
		{
			float value = ((((_eventData.position.x / Screen.width) * 1920f) - 960f - rectMusic.anchoredPosition.x + 270f) / 540f);
			value = Mathf.Clamp01 (value);
			imageMusic.rectTransform.anchoredPosition = new Vector2 (rectMusic.anchoredPosition.x - 270f + (value * 540f), imageMusic.rectTransform.anchoredPosition.y);
		}

		private void OnSliderSound (Touchable _sender, PointerEventData _eventData)
		{
			float value = ((((_eventData.position.x / Screen.width) * 1920f) - 960f - rectSound.anchoredPosition.x + 270f) / 540f);
			value = Mathf.Clamp01 (value);
			imageSound.rectTransform.anchoredPosition = new Vector2 (rectSound.anchoredPosition.x - 270f + (value * 540f), imageSound.rectTransform.anchoredPosition.y);
		}

		private void OnButtonBack(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenMainMenu");
		}

		private IEnumerator OnFadeIn()
		{
			group.alpha = 0f;
			group.DOFade (1f, 0.3f);
			yield return new WaitForSeconds (0.3f);
		}
	}
}
