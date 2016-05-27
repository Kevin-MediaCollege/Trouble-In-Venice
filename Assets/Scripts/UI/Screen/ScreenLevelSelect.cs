using DG;
using DG.Tweening;
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

		public Touchable button_back;

		public LevelItem[] items;

		private int currentPage;
		private int maxPages;
		private bool loading;

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

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
			currentPage = 0;
			UpdatePage();
			StartCoroutine ("OnFadeIn");
		}

		private IEnumerator OnFadeIn()
		{
			group.alpha = 0f;
			group.DOFade (1f, 0.3f);
			yield return new WaitForSeconds (0.3f);
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

		void OnButtonDown (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			ScreenManager.SwitchScreen("ScreenMainMenu");
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
			if(!loading)
			{
				loading = true;
				StartCoroutine (loadLevel (_item.levelName));
			}
		}

		private IEnumerator loadLevel(string _name)
		{
			group.alpha = 1f;
			group.DOFade (0f, 0.2f);

			yield return new WaitForSeconds (0.4f);

			SceneManager.LoadScene (_name);
		}

		/// <summary>
		/// Called when switched to other screen
		/// </summary>
		public override IEnumerator OnScreenFadeout()
		{
			group.alpha = 1f;
			group.DOFade (0f, 0.2f);

			yield return new WaitForSeconds (0.2f);
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
