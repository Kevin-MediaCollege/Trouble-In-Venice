using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Utils;

namespace Proeve
{
	public class ScreenPause : ScreenBase
	{
		public Touchable button_quit;
		public Touchable button_resume;

		protected void Awake()
		{
			if (Application.isMobilePlatform)
			{ 
				button_quit.OnPointerUpEvent += OnButtonQuit;
				button_resume.OnPointerUpEvent += OnButtonResume;
			} 
			else 
			{
				button_quit.OnPointerDownEvent += OnButtonQuit;
				button_resume.OnPointerDownEvent += OnButtonResume;
			}
		}
		
		private void OnButtonQuit(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			SceneManager.LoadScene ("Menu");
		}

		private void OnButtonResume(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenGame");
		}

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
		}

		/// <summary>
		/// Called when switched to other screen
		/// </summary>
		public override IEnumerator OnScreenFadeout()
		{
			yield break;
		}

		/// <summary>
		/// Called after OnScreenFadeout
		/// </summary>
		public override void OnScreenExit()
		{
		}

		/// <summary>
		/// Returns name of the screen
		/// </summary>
		public override string GetScreenName()
		{
			return "ScreenPause";
		}
	}
}

