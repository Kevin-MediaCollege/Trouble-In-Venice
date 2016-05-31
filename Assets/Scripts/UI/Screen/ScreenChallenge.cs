using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Collections;
using Utils;

namespace Proeve
{
	[System.Serializable]
	public class Challenge
	{
		/// <summary>
		/// Challenge ui holder
		/// </summary>
		public GameObject gameObject;

		/// <summary>
		/// Challenge ui image
		/// </summary>
		public Image star;

		/// <summary>
		/// Challenge ui text
		/// </summary>
		public Text text;
	}

	/// <summary>
	/// Manages challenge screen UI.
	/// </summary>
	public class ScreenChallenge : ScreenBase
	{
		[SerializeField] private Challenge[] challenges;
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

			if(ChallengesManager.instance == null)
			{
				StartCoroutine("skipScreen");
				return;
			}

			for(int i = 0; i < 3; i++)
			{
				if (i < ChallengesManager.instance.challenges.Length) 
				{
					challenges [i].gameObject.SetActive (true);
					challenges [i].text.text = ChallengesManager.instance.challenges[i].getString();
				}
				else
				{
					challenges [i].gameObject.SetActive (false);
				}
			}
		}

		public IEnumerator skipScreen()
		{
			yield return null;

			GlobalEvents.Invoke(new SetInputEvent(true));
			ScreenManager.SwitchScreen ("ScreenGame");
		}

		public override string GetScreenName()
		{
			return "ScreenChallenge";
		}

		private void OnButtonStart(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			GlobalEvents.Invoke(new SetInputEvent(true));
			ScreenManager.SwitchScreen ("ScreenGame");
		}
	}
}

