using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using Utils;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Proeve
{
	/// <summary>
	/// Manages level select UI.
	/// </summary>
	public class ScreenLevelSelect : ScreenBase
	{
		[SerializeField, FormerlySerializedAs("button_left")] private Touchable buttonLeft;
		[SerializeField, FormerlySerializedAs("button_right")] private Touchable buttonRight;
		[SerializeField, FormerlySerializedAs("button_back")] private Touchable buttonBack;

		[SerializeField] private LevelItem[] items;

		[SerializeField] private CanvasGroup group;		
		[SerializeField] private Image background;
		[SerializeField] private Text loadingText;		

		private LevelUnlocker levelUnlocker;

		private int currentPage;
		private int maxPages;

		private bool loading;

		protected void Awake()
		{
			levelUnlocker = Dependency.Get<LevelUnlocker>();

			for(int i = 0; i < 3; i++)
			{
				items[i].Init(this);
			}

			loading = false;
			maxPages = Mathf.CeilToInt(LevelManager.Levels.Length / 3f);

			UpdatePage();
		}

		protected void OnEnable()
		{
			if(Application.isMobilePlatform)
			{
				buttonLeft.OnPointerUpEvent += OnButtonPageLeft;
				buttonRight.OnPointerUpEvent += OnButtonPageRight;
				buttonBack.OnPointerUpEvent += OnButtonDown;
			}
			else
			{
				buttonLeft.OnPointerDownEvent += OnButtonPageLeft;
				buttonRight.OnPointerDownEvent += OnButtonPageRight;
				buttonBack.OnPointerDownEvent += OnButtonDown;
			}
		}

		protected void OnDisable()
		{
			if(Application.isMobilePlatform)
			{
				buttonLeft.OnPointerUpEvent -= OnButtonPageLeft;
				buttonRight.OnPointerUpEvent -= OnButtonPageRight;
				buttonBack.OnPointerUpEvent -= OnButtonDown;
			}
			else
			{
				buttonLeft.OnPointerDownEvent -= OnButtonPageLeft;
				buttonRight.OnPointerDownEvent -= OnButtonPageRight;
				buttonBack.OnPointerDownEvent -= OnButtonDown;
			}
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
				ScreenManager.lastLoadedLevel = _item.Name;
				loadingText.enabled = true;
				loading = true;
				StartCoroutine(LoadLevel(_item.Name));
			}
		}

		private void UpdatePage()
		{
			buttonLeft.gameObject.SetActive(currentPage > 0 ? true : false);
			buttonRight.gameObject.SetActive(currentPage < maxPages - 1 ? true : false);

			int levelLenght = LevelManager.Levels.Length;
			int levelID = 0;

			for(int i = 0; i < 3; i++)
			{
				levelID = (currentPage * 3) + i;
				if((currentPage * 3) + i < levelLenght)
				{
					items[i].rect.gameObject.SetActive(true);
					items[i].Name = LevelManager.Levels[levelID].levelName;
					items[i].SetStars(LevelManager.Levels[levelID].maxStars);
					items[i].level.sprite = LevelManager.Levels[levelID].levelImage;
					items[i].debugText.text = LevelManager.Levels[levelID].levelName;

					int id = Convert.ToInt32(LevelManager.Levels[levelID].levelName.Split('_')[1]);
					if(levelUnlocker.IsUnlocked(id))
					{
						items[i].imageLock.enabled = false;
						items[i].IsLocked = false;
						items[i].level.color = new Color(1f, 1f, 1f, 1f);
					}
					else
					{
						items[i].imageLock.enabled = true;
						items[i].IsLocked = true;
						items[i].level.color = new Color(0.5f, 0.5f, 0.5f, 1f);
					}
				}
				else
				{
					items[i].rect.gameObject.SetActive(false);
				}
			}
		}

		private void OnButtonPageLeft(Touchable _sender, PointerEventData _eventData)
		{
			if(currentPage > 0)
			{
				currentPage--;
				UpdatePage();
			}
		}

		private void OnButtonPageRight(Touchable _sender, PointerEventData _eventData)
		{
			if(currentPage < maxPages - 1)
			{
				currentPage++;
				UpdatePage();
			}
		}

		private void OnButtonDown(Touchable _sender, PointerEventData _eventData)
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
		public Image[] stars;

		public RectTransform rect;

		public Image level;
		public Image imageLock;

		public Text debugText;

		public Touchable button;

		public string Name { set; get; }

		public bool IsLocked { set; get; }
		
		private ScreenLevelSelect levelSelect;

		/// <summary>
		/// Initialize the LevelItem.
		/// </summary>
		/// <param name="_screen">The level select UI manager</param>
		public void Init(ScreenLevelSelect _screen)
		{
			if (Application.isMobilePlatform) { button.OnPointerUpEvent += OnButton; } else { button.OnPointerDownEvent += OnButton; }
			levelSelect = _screen;
			IsLocked = false;
		}

		/// <summary>
		/// Set the amount of stars received for the level.
		/// </summary>
		/// <param name="_stars">The amount of stars to activate.</param>
		public void SetStars(int _stars)
		{
			for(int i = 0; i < 3; i++)
			{
				stars[i].enabled = i < _stars ? true : false;
			}
		}

		private void OnButton(Touchable _sender, PointerEventData _eventData)
		{
			if(!IsLocked)
			{
				levelSelect.OnLevelButton(this);
			}
		}
	}
}
