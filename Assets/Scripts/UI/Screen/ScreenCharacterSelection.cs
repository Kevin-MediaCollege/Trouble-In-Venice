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
	public class ScreenCharacterSelection : ScreenBase
	{
		public Touchable button_back;
		public CanvasGroup group;

		protected void Awake()
		{
			if (Application.isMobilePlatform) { button_back.OnPointerUpEvent += OnButtonBack; } else { button_back.OnPointerDownEvent += OnButtonBack; }
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
			group.DOFade (0f, 0.2f);

			yield return new WaitForSeconds (0.2f);
		}

		/// <summary>
		/// Called after OnScreenFadeout
		/// </summary>
		public override void OnScreenExit()
		{

		}

		private void OnButtonBack(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenMainMenu");
		}

		/// <summary>
		/// Returns name of the screen
		/// </summary>
		public override string GetScreenName()
		{
			return "ScreenCharacterSelection";
		}
	}
}
