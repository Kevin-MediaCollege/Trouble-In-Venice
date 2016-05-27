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
	public class ScreenCredits : ScreenBase
	{
		public Touchable button_back;
		public CanvasGroup group;
		public Touchable button_easteregg;

		private int easteregg;

		protected void Awake()
		{
			if (Application.isMobilePlatform) { button_back.OnPointerUpEvent += OnButtonBack; } else { button_back.OnPointerDownEvent += OnButtonBack; }

			button_easteregg.OnPointerDownEvent += Button_easteregg_OnPointerDownEvent;
		}

		void Button_easteregg_OnPointerDownEvent (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			easteregg++;
			Debug.Log ("" + easteregg);

			if(easteregg == 10)
			{
				Text[] texts = ScreenManager.instance.GetComponentsInChildren<Text> (true);
				for(int i = 0; i < texts.Length; i++)
				{
					texts[i].text = "ted is koning";
				}
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
			return "ScreenCredits";
		}
	}
}
