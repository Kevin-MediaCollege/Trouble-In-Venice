﻿using DG.Tweening;
using UnityEngine;
using System.Collections;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Proeve
{
	/// <summary>
	/// Manages character selection UI.
	/// </summary>
	public class ScreenCharacterSelection : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("button_back")] private Touchable buttonBack;

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
		}

		public override void OnScreenEnter()
		{
			StartCoroutine("OnFadeIn");
		}

		public override IEnumerator OnScreenFadeout()
		{
			group.alpha = 1f;
			group.DOFade (0f, 0.2f);

			yield return new WaitForSeconds (0.2f);
		}

		public override string GetScreenName()
		{
			return "ScreenCharacterSelection";
		}

		private IEnumerator OnFadeIn()
		{
			group.alpha = 0f;
			group.DOFade (1f, 0.3f);
			yield return new WaitForSeconds (0.3f);
		}

		private void OnButtonBack(Touchable _sender, PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen("ScreenMainMenu");
		}
	}
}
