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
	/// Manages credits screen UI.
	/// </summary>
	public class ScreenCredits : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("button_back")] private Touchable buttonBack;		
		[SerializeField, FormerlySerializedAs("button_easteregg")] private Touchable buttonEasterEgg;

		[SerializeField] private CanvasGroup group;

		private int easterEggCount;

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

			buttonEasterEgg.OnPointerDownEvent += OnButtonEasterEgg;
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

			buttonEasterEgg.OnPointerDownEvent -= OnButtonEasterEgg;
		}

		public override void OnScreenEnter()
		{
			StartCoroutine("OnFadeIn");
		}

		public override IEnumerator OnScreenFadeout()
		{
			group.alpha = 1f;
			group.DOFade(0f, 0.2f);

			yield return new WaitForSeconds(0.2f);
		}

		public override string GetScreenName()
		{
			return "ScreenCredits";
		}

		private void OnButtonEasterEgg(Touchable _sender, PointerEventData _eventData)
		{
			easterEggCount++;

			if(easterEggCount == 10)
			{
				Text[] texts = ScreenManager.instance.GetComponentsInChildren<Text>(true);

				for(int i = 0; i < texts.Length; i++)
				{
					texts[i].text = "ted is koning";
				}
			}
		}

		private void OnButtonBack(Touchable _sender, PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen("ScreenMainMenu");
		}

		private IEnumerator OnFadeIn()
		{
			group.alpha = 0f;
			group.DOFade(1f, 0.3f);
			yield return new WaitForSeconds(0.3f);
		}
	}
}
