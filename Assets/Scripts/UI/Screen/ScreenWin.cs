using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Proeve
{
	/// <summary>
	/// Manages the win screen UI.
	/// </summary>
	public class ScreenWin : ScreenBase
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
				buttonContinue.OnPointerUpEvent -= OnButtonContinue;
			}
			else
			{
				buttonContinue.OnPointerDownEvent -= OnButtonContinue;
			}
		}

		/// <summary>
		/// Returns name of the screen
		/// </summary>
		public override string GetScreenName()
		{
			return "ScreenWin";
		}

		private void OnButtonContinue(Touchable _sender, PointerEventData _eventData)
		{
			SceneManager.LoadScene ("Menu");
		}
	}
}