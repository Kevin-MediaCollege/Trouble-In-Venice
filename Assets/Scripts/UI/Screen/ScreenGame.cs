using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Utils;

namespace Proeve
{
	public class ScreenGame : ScreenBase 
	{
		public Touchable button_reset;
		public Touchable button_options;
		public Touchable button_camera;

		private int cameraMode;

		protected void Awake()
		{
			if (Application.isMobilePlatform)
			{ 
				button_reset.OnPointerUpEvent += OnButtonReset;
				button_options.OnPointerUpEvent += OnButtonOptions;
				button_camera.OnPointerUpEvent += OnButtonCamera;
			} 
			else 
			{
				button_reset.OnPointerDownEvent += OnButtonReset;
				button_options.OnPointerDownEvent += OnButtonOptions;
				button_camera.OnPointerDownEvent += OnButtonCamera;
			}
		}

		protected void Start()
		{
			cameraMode = 0;
			GameCamera.instance.setCameraMode (cameraMode);
		}

		private void OnButtonCamera (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			if(!GameCamera.instance.cutscene)
			{
				cameraMode = cameraMode == 0 ? 2 : 0;
				GameCamera.instance.setCameraMode (cameraMode);
			}
		}

		private void OnButtonReset(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}

		private void OnButtonOptions(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			GlobalEvents.Invoke(new SetInputEvent(false));

			ScreenManager.SwitchScreen ("ScreenPause");
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
			return "ScreenGame";
		}
	}
}