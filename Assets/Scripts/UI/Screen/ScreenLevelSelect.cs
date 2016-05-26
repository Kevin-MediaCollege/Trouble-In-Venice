using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenLevelSelect : ScreenBase
	{
		public Touchable button_left;
		public Touchable button_right;
		public CanvasGroup group;

		public LevelItem[] items;

		private int currentPage;
		private int maxPages;

		protected void Awake()
		{
			for(int i = 0; i < 3; i++)
			{
				items[i].Init(this);
			}

			if (Application.isMobilePlatform)
			{ 
				button_left.OnPointerUpEvent += OnButtonPageLeft; 
				button_right.OnPointerUpEvent += OnButtonPageRight; 
			} 
			else 
			{ 
				button_left.OnPointerDownEvent += OnButtonPageLeft;
				button_right.OnPointerDownEvent += OnButtonPageRight;
			}

			maxPages = Mathf.CeilToInt(LevelManager.Levels.Length / 3f);
			UpdatePage ();
		}

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
			currentPage = 0;
			UpdatePage();
		}

		private void OnButtonPageLeft (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			if(currentPage > 0)
			{
				currentPage--;
				UpdatePage();
			}
		}

		private void OnButtonPageRight (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			if(currentPage < maxPages - 1)
			{
				currentPage++;
				UpdatePage();
			}
		}

		public void UpdatePage()
		{
			button_left.gameObject.SetActive (currentPage > 0 ? true : false);
			button_right.gameObject.SetActive (currentPage < maxPages - 1 ? true : false);
			
			int levelLenght = LevelManager.Levels.Length;
			int levelID = 0;

			for(int i = 0; i < 3; i++)
			{
				levelID = (currentPage * 3) + i;
				if ((currentPage * 3) + i < levelLenght) 
				{
					items[i].rect.gameObject.SetActive(true);
					items[i].levelName = LevelManager.Levels[levelID].levelName;
					items[i].SetStars(LevelManager.Levels[levelID].maxStars);
					items[i].level.sprite = LevelManager.Levels[levelID].levelImage;
					items[i].debugText.text = LevelManager.Levels[levelID].levelName;
				}
				else 
				{
					items[i].rect.gameObject.SetActive(false);
				}
			}
		}

		public void OnLevelButton(LevelItem _item)
		{
			SceneManager.LoadScene (_item.levelName);
			//Load level,  _item.levelName
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
			return "ScreenLevelSelect";
		}
	}

	[System.Serializable]
	public class LevelItem
	{
		public RectTransform rect;
		public Image level;
		public Touchable button;
		public Image[] stars;
		public Text debugText;

		[System.NonSerialized]
		public string levelName;

		[System.NonSerialized]
		public ScreenLevelSelect levelSelect;

		public void Init(ScreenLevelSelect _screen)
		{
			if (Application.isMobilePlatform) { button.OnPointerUpEvent += OnButton; } else { button.OnPointerDownEvent += OnButton; }
			levelSelect = _screen;
		}

		public void SetStars(int _stars)
		{
			for(int i = 0; i < 3; i++)
			{
				stars [i].enabled = i < _stars ? true : false;
			}
		}

		private void OnButton(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			levelSelect.OnLevelButton(this);
		}
	}
}
