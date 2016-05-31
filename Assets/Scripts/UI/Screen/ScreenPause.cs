using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Proeve
{
	/// <summary>
	/// Manages the pause screen UI.
	/// </summary>
	public class ScreenPause : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("button_quit")] private Touchable buttonQuit;
		[SerializeField, FormerlySerializedAs("button_resume")] private Touchable buttonResume;

		protected void OnEnable()
		{
			if (Application.isMobilePlatform)
			{ 
				buttonQuit.OnPointerUpEvent += OnButtonQuit;
				buttonResume.OnPointerUpEvent += OnButtonResume;
			} 
			else 
			{
				buttonQuit.OnPointerDownEvent += OnButtonQuit;
				buttonResume.OnPointerDownEvent += OnButtonResume;
			}
		}

		protected void OnDisable()
		{
			if(Application.isMobilePlatform)
			{
				buttonQuit.OnPointerUpEvent -= OnButtonQuit;
				buttonResume.OnPointerUpEvent -= OnButtonResume;
			}
			else
			{
				buttonQuit.OnPointerDownEvent -= OnButtonQuit;
				buttonResume.OnPointerDownEvent -= OnButtonResume;
			}
		}

		public override string GetScreenName()
		{
			return "ScreenPause";
		}

		private void OnButtonQuit(Touchable _sender, PointerEventData _eventData)
		{
			SceneManager.LoadScene ("Menu");
		}

		private void OnButtonResume(Touchable _sender, PointerEventData _eventData)
		{
			GlobalEvents.Invoke(new SetInputEvent(true));

			ScreenManager.SwitchScreen("ScreenGame");
		}
	}
}

