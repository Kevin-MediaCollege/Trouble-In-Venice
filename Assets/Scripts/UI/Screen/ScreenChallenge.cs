using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Manages challenge screen UI.
	/// </summary>
	public class ScreenChallenge : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("button_start")] private Touchable buttonStart;

		protected void OnEnable()
		{
			buttonStart.OnPointerDownEvent += OnButtonStart;
		}

		protected void OnDisable()
		{
			buttonStart.OnPointerDownEvent -= OnButtonStart;
		}

		public override void OnScreenEnter()
		{
			GlobalEvents.Invoke(new SetInputEvent(false));
		}

		public override string GetScreenName()
		{
			return "ScreenChallenge";
		}

		private void OnButtonStart(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen ("ScreenGame");
			GlobalEvents.Invoke(new SetInputEvent(true));
		}
	}
}

