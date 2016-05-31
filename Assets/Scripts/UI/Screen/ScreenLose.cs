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
		[SerializeField, FormerlySerializedAs("temp_button")] private Touchable buttonRetry;
		[SerializeField] private Touchable buttonMenu;

		protected void OnEnable()
		{
			if(Application.isMobilePlatform)
			{ 
				buttonRetry.OnPointerUpEvent += OnButtonRetry;
				buttonMenu.OnPointerUpEvent += OnButtonMenu;
			} 
			else 
			{
				buttonRetry.OnPointerDownEvent += OnButtonRetry;
				buttonMenu.OnPointerDownEvent += OnButtonMenu;
			}
		}

		protected void OnDisable()
		{
			if(Application.isMobilePlatform)
			{
				buttonRetry.OnPointerUpEvent -= OnButtonRetry;
				buttonMenu.OnPointerUpEvent -= OnButtonMenu;
			}
			else
			{
				buttonRetry.OnPointerDownEvent -= OnButtonRetry;
				buttonMenu.OnPointerDownEvent -= OnButtonMenu;
			}
		}

		public override string GetScreenName()
		{
			return "ScreenLose";
		}

		private void OnButtonRetry(Touchable _sender, PointerEventData _eventData)
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}

		private void OnButtonMenu (Touchable _sender, PointerEventData _eventData)
		{
			SceneManager.LoadScene ("Menu");
		}
	}
}

