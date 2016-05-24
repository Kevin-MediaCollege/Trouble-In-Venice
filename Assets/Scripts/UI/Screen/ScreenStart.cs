using DG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Proeve
{
	public class ScreenStart : ScreenBase
	{
		public CanvasGroup group_main;
		public CanvasGroup group_anyKey;
		public CanvasGroup group_overlay;

		public Text text;
		public Image background;
		private bool fading;

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
			fading = true;
			StartCoroutine ("OnScreenFadein");
			text.text = Application.isMobilePlatform ? "Touch to start" : "Press any key to start";
		}

		private IEnumerator OnScreenFadein()
		{
			background.enabled = true;
			group_overlay.alpha = 1f;
			group_main.alpha = 0f;
			group_anyKey.alpha = 0f;
			yield return new WaitForSeconds (0.5f);
			group_overlay.DOFade (0f, 0.5f);
			yield return new WaitForSeconds (0.2f);
			StartCoroutine("FlashAnyKey");
			group_main.DOFade (1f, 0.5f);
			yield return new WaitForSeconds (0.5f);
			fading = false;
		}

		/// <summary>
		/// Called when switched to other screen
		/// </summary>
		public override IEnumerator OnScreenFadeout()
		{
			StopCoroutine ("FlashAnyKey");
			group_anyKey.DOFade(0f, 0.5f);
			group_main.DOFade (0f, 0.5f);
			yield return new WaitForSeconds (0.5f);
		}

		/// <summary>
		/// Called after OnScreenFadeout
		/// </summary>
		public override void OnScreenExit()
		{
		}

		protected void Update()
		{
			if(Input.anyKeyDown)
			{
				ScreenManager.instance.SwitchScreen ("ScreenMainMenu");
			}
		}

		private IEnumerator FlashAnyKey()
		{
			group_anyKey.alpha = 0f;
			group_anyKey.DOFade(1f, 0.8f).SetEase(Ease.InOutSine);

			yield return new WaitForSeconds (0.8f);

			while(true)
			{
				group_anyKey.DOFade(0.5f, 0.8f).SetEase(Ease.InOutSine);

				yield return new WaitForSeconds (0.8f);

				group_anyKey.DOFade(1f, 0.8f).SetEase(Ease.InOutSine);

				yield return new WaitForSeconds (0.8f);
			}
		}

		/// <summary>
		/// Returns name of the screen
		/// </summary>
		public override string GetScreenName()
		{
			return "ScreenStart";
		}
	}
}
