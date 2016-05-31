using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using System;

namespace Proeve
{
	/// <summary>
	/// Manages the win screen UI.
	/// </summary>
	public class ScreenWin : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("temp_button")] private Touchable buttonContinue;
		[SerializeField] private Touchable buttonMenu;
		private int nextLevelID = 0;

		[SerializeField] private Image[] stars;
		[SerializeField] private Sprite star_empty;
		[SerializeField] private Sprite star_filled;

		protected void OnEnable()
		{
			int currentLevelID = Convert.ToInt32(SceneManager.GetActiveScene().name.Split('_')[1]);
			nextLevelID = 0;
			if ((nextLevelID = LevelManager.GetLevelIDFromName ("Level_" + (currentLevelID + 1))) > 0) 
			{
				buttonContinue.gameObject.SetActive (true);
				if (Application.isMobilePlatform) 
				{ 
					buttonContinue.OnPointerUpEvent += OnButtonContinue;
				} 
				else 
				{
					buttonContinue.OnPointerDownEvent += OnButtonContinue;
				}
			} 
			else 
			{
				buttonContinue.gameObject.SetActive (false);
			}

			if(Application.isMobilePlatform)
			{ 
				buttonMenu.OnPointerUpEvent += OnButtonMenu;
			} 
			else 
			{
				buttonMenu.OnPointerDownEvent += OnButtonMenu;
			}

			if(ChallengesManager.instance != null)
			{
				setStars(ChallengesManager.instance.stars);
			}
			else
			{
				setStars(0);
			}
		}

		private void setStars(int _stars)
		{
			int levelID = Convert.ToInt32(SceneManager.GetActiveScene().name.Split('_')[1]);
			LevelData ld = LevelManager.Levels [levelID];

			for(int i = 0; i < 3; i++)
			{
				if(i >= ld.maxStars)
				{
					stars[i].gameObject.SetActive (false);
				}
				else if(i < _stars)
				{
					stars[i].gameObject.SetActive (true);
					stars[i].sprite = star_filled;
				}
				else
				{
					stars[i].gameObject.SetActive (true);
					stars[i].sprite = star_empty;
				}
			}
		}

		protected void OnDisable()
		{
			if(nextLevelID != 0)
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

			if(Application.isMobilePlatform)
			{
				buttonMenu.OnPointerUpEvent -= OnButtonMenu;
			}
			else
			{
				buttonMenu.OnPointerDownEvent -= OnButtonMenu;
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
			SceneManager.LoadScene ("Level_" + nextLevelID);
		}

		private void OnButtonMenu (Touchable _sender, PointerEventData _eventData)
		{
			SceneManager.LoadScene ("Menu");
		}
	}
}