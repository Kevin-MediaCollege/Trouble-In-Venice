using DG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Manages level select UI.
	/// </summary>
	public class ScreenLevelSelect : ScreenBase
	{
		public Touchable button_left;
		public Touchable button_right;
		public CanvasGroup group;

		public Touchable button_back;
		public Image background;

		public Text loadingText;

		public LevelItem[] items;

		private int currentPage;
		private int maxPages;
		private bool loading;

		private LevelUnlocker levelUnlocker;

		protected void Awake()
		{
			levelUnlocker = Dependency.Get<LevelUnlocker>();
			for(int i = 0; i < 3; i++)
			{
				items[i].Init(this);
			}

			if (Application.isMobilePlatform)
			{ 
				button_left.OnPointerUpEvent += OnButtonPageLeft; 
				button_right.OnPointerUpEvent += OnButtonPageRight; 
				button_back.OnPointerUpEvent += OnButtonDown;
			} 
			else 
			{ 
				button_left.OnPointerDownEvent += OnButtonPageLeft;
				button_right.OnPointerDownEvent += OnButtonPageRight;
				button_back.OnPointerDownEvent += OnButtonDown;
			}

			loading = false;
			maxPages = Mathf.CeilToInt(LevelManager.Levels.Length / 3f);
			UpdatePage ();
		}

		public override void OnScreenEnter()
		{
			loadingText.enabled = false;
			background.enabled = true;
			levelUnlocker = Dependency.Get<LevelUnlocker>();

			if(!string.IsNullOrEmpty(ScreenManager.lastLoadedLevel))
			{
				currentPage = Mathf.FloorToInt(LevelManager.GetLevelIDFromName(ScreenManager.lastLoadedLevel) / 3f);
			}
			else
			{
				currentPage = 0;
			}

			UpdatePage();
			StartCoroutine ("OnFadeIn");
		}

		public override IEnumerator OnScreenFadeout()
		{
			group.alpha = 1f;
			group.DOFade(0f, 0.2f);

			yield return new WaitForSeconds(0.2f);
		}
		
		public override string GetScreenName()
		{
			return "ScreenLevelSelect";
		}

		/// <summary>
		/// Called by <see cref="LevelItem"/> when a level button has been pressed.
		/// </summary>
		/// <param name="_item"></param>
		public void OnLevelButton(LevelItem _item)
		{
			if(!loading)
			{
				ScreenManager.lastLoadedLevel = _item.levelName;
				loadingText.enabled = true;
				loading = true;
				StartCoroutine(LoadLevel(_item.levelName));
			}
		}

		private void UpdatePage()
		{
			button_left.gameObject.SetActive(currentPage > 0 ? true : false);
			button_right.gameObject.SetActive(currentPage < maxPages - 1 ? true : false);

			int levelLenght = LevelManager.Levels.Length;
			int levelID = 0;

			for(int i = 0; i < 3; i++)
			{
				levelID = (currentPage * 3) + i;
				if((currentPage * 3) + i < levelLenght)
				{
					items[i].rect.gameObject.SetActive(true);
					items[i].levelName = LevelManager.Levels[levelID].levelName;
					items[i].SetStars(LevelManager.Levels[levelID].maxStars);
					items[i].level.sprite = LevelManager.Levels[levelID].levelImage;
					items[i].debugText.text = LevelManager.Levels[levelID].levelName;

					int id = Convert.ToInt32(LevelManager.Levels[levelID].levelName.Split('_')[1]);
					if(levelUnlocker.IsUnlocked(id))
					{
						items[i].imageLock.enabled = false;
						items[i].locked = false;
						items[i].level.color = new Color(1f, 1f, 1f, 1f);
					}
					else
					{
						items[i].imageLock.enabled = true;
						items[i].locked = true;
						items[i].level.color = new Color(0.5f, 0.5f, 0.5f, 1f);
					}
				}
				else
				{
					items[i].rect.gameObject.SetActive(false);
				}
			}
		}

		private void OnButtonPageLeft(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			if(currentPage > 0)
			{
				currentPage--;
				UpdatePage();
			}
		}

		private void OnButtonPageRight(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			if(currentPage < maxPages - 1)
			{
				currentPage++;
				UpdatePage();
			}
		}

		private void OnButtonDown(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen("ScreenMainMenu");
		}

		private IEnumerator LoadLevel(string _name)
		{
			group.alpha = 1f;
			group.DOFade (0f, 0.2f);

			yield return new WaitForSeconds (0.4f);

			SceneManager.LoadScene (_name);
		}

		private IEnumerator OnFadeIn()
		{
			group.alpha = 0f;
			group.DOFade(1f, 0.3f);
			yield return new WaitForSeconds(0.3f);
		}
	}

	[Serializable]
	public class LevelItem
	{
		public RectTransform rect;
		public Image level;
		public Touchable button;
		public Image[] stars;
		public Text debugText;
		public Image imageLock;

		[System.NonSerialized]
		public bool locked;

		[System.NonSerialized]
		public string levelName;

		[System.NonSerialized]
		public ScreenLevelSelect levelSelect;

		/// <summary>
		/// Initialize the levelItem
		/// </summary>
		/// <param name="_screen"></param>
		public void Init(ScreenLevelSelect _screen)
		{
			if (Application.isMobilePlatform) { button.OnPointerUpEvent += OnButton; } else { button.OnPointerDownEvent += OnButton; }
			levelSelect = _screen;
			locked = false;
		}

		/// <summary>
		/// Set the amount of stars received for the level.
		/// </summary>
		/// <param name="_stars"></param>
		public void SetStars(int _stars)
		{
			for(int i = 0; i < 3; i++)
			{
				stars[i].enabled = i < _stars ? true : false;
			}
		}

		private void OnButton(Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			if(!locked)
			{
				levelSelect.OnLevelButton(this);
			}
		}
	}
}
