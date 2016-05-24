using DG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Snakybo.Audio;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenIntroCompany : ScreenBase
	{
		public GoatPrint[] printList;
		public CanvasGroup logoAlphaGroup;
		public RectTransform logoRect;
		public CanvasGroup overlayAlphaGroup;
		public AudioObject scream;

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
			StartCoroutine("IntroAnimation");
		}

		/// <summary>
		/// Called when switched to other screen
		/// </summary>
		private IEnumerator IntroAnimation()
		{
			//hide everything
			overlayAlphaGroup.alpha = 0f;
			logoAlphaGroup.alpha = 1f;
			logoRect.sizeDelta = new Vector2(0f, 0f);
			for(int i = 0; i < 5; i++)
			{
				printList[i].alphaGroup.alpha = 0f;
				printList[i].rect.sizeDelta = new Vector2(0f, 0f);
			}

			yield return new WaitForSeconds(0.2f);

			//Fade in base logo
			logoRect.DOSizeDelta(new Vector2(1380f, 155f), 0.6f).SetEase(Ease.OutElastic);

			yield return new WaitForSeconds(0.5f);

			//Fade print 1
			printList[0].alphaGroup.alpha = 1f;
			printList[0].rect.DOSizeDelta(new Vector2(196f, 204f), 0.3f).SetEase(Ease.OutBounce);

			yield return new WaitForSeconds(0.1f);

			//Fade print 2
			printList[1].alphaGroup.alpha = 1f;
			printList[1].rect.DOSizeDelta(new Vector2(196f, 204f), 0.3f).SetEase(Ease.OutBounce);

			yield return new WaitForSeconds(0.1f);

			//Fade print 3
			printList[2].alphaGroup.alpha = 1f;
			printList[2].rect.DOSizeDelta(new Vector2(196f, 204f), 0.3f).SetEase(Ease.OutBounce);

			yield return new WaitForSeconds(0.1f);

			//Fade print 4
			printList[3].alphaGroup.alpha = 1f;
			printList[3].rect.DOSizeDelta(new Vector2(196f, 204f), 0.3f).SetEase(Ease.OutBounce);

			yield return new WaitForSeconds(0.1f);

			//Fade print 5
			printList[4].alphaGroup.alpha = 1f;
			printList[4].rect.DOSizeDelta(new Vector2(196f, 204f), 0.3f).SetEase(Ease.OutBounce);

			yield return new WaitForSeconds(0.3f);

			StartCoroutine(PlayNaughtyGoatAudioDelayed());

			//Fade out all prints except print 2
			for(int i = 0; i < 5; i++)
			{
				if(i != 2)
				{
					printList[i].alphaGroup.DOFade(0f, 0.3f);
				}
			}

			yield return new WaitForSeconds(1.4f);

			//fadeout screen
			overlayAlphaGroup.DOFade(1f, 0.6f);

			yield return new WaitForSeconds(0.6f);

			SceneManager.LoadSceneAsync("Menu");
		}

		private IEnumerator PlayNaughtyGoatAudioDelayed()
		{
			yield return new WaitForSeconds(0.15f);

			scream.Play();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string GetScreenName()
		{
			return "ScreenIntroCompany";
		}
	}
	
	/// <summary>
	/// Returns name of the screen
	/// </summary>
	[Serializable]
	public class GoatPrint
	{
		public Image image;
		public CanvasGroup alphaGroup;
		public RectTransform rect;
	}
}
