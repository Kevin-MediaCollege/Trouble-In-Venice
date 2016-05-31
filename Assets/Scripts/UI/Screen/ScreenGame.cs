using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Proeve
{
	/// <summary>
	/// Manages ingame UI.
	/// </summary>
	public class ScreenGame : ScreenBase 
	{
		[SerializeField, FormerlySerializedAs("button_reset")] private Touchable buttonReset;
		[SerializeField, FormerlySerializedAs("button_options")] private Touchable buttonOptions;
		[SerializeField, FormerlySerializedAs("button_camera")] private Touchable buttonCamera;

		private int cameraMode;

		protected void OnEnable()
		{
			if(Application.isMobilePlatform)
			{ 
				buttonReset.OnPointerUpEvent += OnButtonReset;
				buttonOptions.OnPointerUpEvent += OnButtonOptions;
				buttonCamera.OnPointerUpEvent += OnButtonCamera;
			} 
			else 
			{
				buttonReset.OnPointerDownEvent += OnButtonReset;
				buttonOptions.OnPointerDownEvent += OnButtonOptions;
				buttonCamera.OnPointerDownEvent += OnButtonCamera;
			}
		}

		protected void OnDisable()
		{
			if(Application.isMobilePlatform)
			{
				buttonReset.OnPointerUpEvent -= OnButtonReset;
				buttonOptions.OnPointerUpEvent -= OnButtonOptions;
				buttonCamera.OnPointerUpEvent -= OnButtonCamera;
			}
			else
			{
				buttonReset.OnPointerDownEvent -= OnButtonReset;
				buttonOptions.OnPointerDownEvent -= OnButtonOptions;
				buttonCamera.OnPointerDownEvent -= OnButtonCamera;
			}
		}

		protected void Start()
		{
			cameraMode = 0;
			GameCamera.instance.setCameraMode (cameraMode);
		}

		public override string GetScreenName()
		{
			return "ScreenGame";
		}

		private void OnButtonCamera(Touchable _sender, PointerEventData _eventData)
		{
			if(!GameCamera.instance.cutscene)
			{
				cameraMode = cameraMode == 0 ? 2 : 0;
				GameCamera.instance.setCameraMode(cameraMode);
			}
		}

		private void OnButtonReset(Touchable _sender, PointerEventData _eventData)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		private void OnButtonOptions(Touchable _sender, PointerEventData _eventData)
		{
			GlobalEvents.Invoke(new SetInputEvent(false));

			ScreenManager.SwitchScreen("ScreenPause");
		}
	}
}