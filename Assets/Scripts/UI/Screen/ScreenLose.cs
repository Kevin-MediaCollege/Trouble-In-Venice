using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Proeve
{
	/// <summary>
	/// Manages lose screen UI.
	/// </summary>
	public class ScreenLose : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("temp_button")] private Touchable buttonContinue;

		protected void OnEnable()
		{
			if(Application.isMobilePlatform)
			{ 
				buttonContinue.OnPointerUpEvent += OnButtonContinue;
			} 
			else 
			{
				buttonContinue.OnPointerDownEvent += OnButtonContinue;
			}
		}

		protected void OnDisable()
		{
			if(Application.isMobilePlatform)
			{
				buttonContinue.OnPointerUpEvent += OnButtonContinue;
			}
			else
			{
				buttonContinue.OnPointerDownEvent += OnButtonContinue;
			}
		}

		public override string GetScreenName()
		{
			return "ScreenLose";
		}

		private void OnButtonContinue(Touchable _sender, PointerEventData _eventData)
		{
			SceneManager.LoadScene("Menu");
		}
	}
}

