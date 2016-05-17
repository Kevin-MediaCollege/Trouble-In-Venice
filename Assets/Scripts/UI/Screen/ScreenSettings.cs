using DG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenSettings : ScreenBase
	{
		public Touchable button_back;
		public Touchable button_music;
		public Touchable button_sounds;

		public RectTransform rect_music;
		public RectTransform rect_sound;

		public Image image_music;
		public Image image_sound;

		public CanvasGroup group;

		protected void Awake()
		{
			if (Application.isMobilePlatform) { button_back.OnPointerUpEvent += OnButtonBack; } else { button_back.OnPointerDownEvent += OnButtonBack; }

			button_music.OnPointerDownEvent += OnSliderMusic;
			button_music.OnPointerMoveEvent += OnSliderMusic;
			button_sounds.OnPointerDownEvent += OnSliderSound;
			button_sounds.OnPointerMoveEvent += OnSliderSound;
		}

		private void OnSliderMusic (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			float value = ((((_eventData.position.x / Screen.width) * 1920f) - 960f - rect_music.anchoredPosition.x + 270f) / 540f);
			value = Mathf.Clamp01 (value);
			image_music.rectTransform.anchoredPosition = new Vector2 (rect_music.anchoredPosition.x - 270f + (value * 540f), image_music.rectTransform.anchoredPosition.y);
		}

		private void OnSliderSound (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			float value = ((((_eventData.position.x / Screen.width) * 1920f) - 960f - rect_sound.anchoredPosition.x + 270f) / 540f);
			value = Mathf.Clamp01 (value);
			image_sound.rectTransform.anchoredPosition = new Vector2 (rect_sound.anchoredPosition.x - 270f + (value * 540f), image_sound.rectTransform.anchoredPosition.y);
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

		private void OnButtonBack(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.instance.SwitchScreen ("ScreenMainMenu");
		}

		/// <summary>
		/// Returns name of the screen
		/// </summary>
		public override string GetScreenName()
		{
			return "ScreenSettings";
		}
	}
}
