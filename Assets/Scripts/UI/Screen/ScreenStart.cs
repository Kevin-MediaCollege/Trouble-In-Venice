using DG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;

namespace Proeve
{
	/// <summary>
	/// Manages start screen UI.
	/// </summary>
	public class ScreenStart : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("group_main")] private CanvasGroup groupMain;
		[SerializeField, FormerlySerializedAs("group_anyKey")] private CanvasGroup groupAnyKey;
		[SerializeField, FormerlySerializedAs("group_overlay")] private CanvasGroup groupOverlay;

		[SerializeField] private Text title;
		[SerializeField] private Text text;
		[SerializeField] private Image background;

		private int easterEgg;

		protected void Update()
		{ 
			if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Alpha9))
			{
				ScreenManager.SwitchScreen ("ScreenMainMenu");
			}
			else if(Input.GetKeyDown(KeyCode.Alpha9))
			{
				easterEgg++;
				if(easterEgg > 10)
				{
					title.text = "Romantix";
				}
			}
		}

		public override void OnScreenEnter()
		{
			StartCoroutine ("OnScreenFadein");
			text.text = Application.isMobilePlatform ? "Touch to start" : "Press any key to start";
		}

		public override IEnumerator OnScreenFadeout()
		{
			StopCoroutine ("FlashAnyKey");
			groupAnyKey.DOFade(0f, 0.5f);
			groupMain.DOFade (0f, 0.5f);
			yield return new WaitForSeconds (0.5f);
		}
		
		public override string GetScreenName()
		{
			return "ScreenStart";
		}

		private IEnumerator OnScreenFadein()
		{
			background.enabled = true;
			groupOverlay.alpha = 1f;
			groupMain.alpha = 0f;
			groupAnyKey.alpha = 0f;
			yield return new WaitForSeconds (0.5f);
			groupOverlay.DOFade (0f, 0.5f);
			yield return new WaitForSeconds (0.2f);
			StartCoroutine("FlashAnyKey");
			groupMain.DOFade (1f, 0.5f);
			yield return new WaitForSeconds (0.5f);
		}

		private IEnumerator FlashAnyKey()
		{
			groupAnyKey.alpha = 0f;
			groupAnyKey.DOFade(1f, 0.8f).SetEase(Ease.InOutSine);

			yield return new WaitForSeconds (0.8f);

			while(true)
			{
				groupAnyKey.DOFade(0.5f, 0.8f).SetEase(Ease.InOutSine);

				yield return new WaitForSeconds (0.8f);

				groupAnyKey.DOFade(1f, 0.8f).SetEase(Ease.InOutSine);

				yield return new WaitForSeconds (0.8f);
			}
		}
	}
}
