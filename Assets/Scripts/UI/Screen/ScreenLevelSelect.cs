using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenLevelSelect : ScreenBase
	{
		public LevelItem[] items;

		protected void Awake()
		{
			for(int i = 0; i < 3; i++)
			{
				items[i].init(this);
			}
		}

		/// <summary>
		/// Called when switched to this screen
		/// </summary>
		public override void OnScreenEnter()
		{
			LevelData[] levels = LevelManager.getLevelList ();
			int levelLenght = levels.Length;
			int currentPage = 0;
			int levelID = 0;

			for(int i = 0; i < 3; i++)
			{
				levelID = (currentPage * 3) + i;
				if ((currentPage * 3) + i < levelLenght) 
				{
					items[i].rect.gameObject.SetActive(true);
					items[i].levelName = levels[levelID].levelName;
					items[i].setStars(levels[levelID].maxStars);
					items[i].level.sprite = levels[levelID].levelImage;
				}
				else 
				{
					items[i].rect.gameObject.SetActive(false);
				}
			}
		}

		public void OnLevelButton(LevelItem _item)
		{
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

		[System.NonSerialized]
		public string levelName;

		[System.NonSerialized]
		public ScreenLevelSelect levelSelect;

		public void init(ScreenLevelSelect _screen)
		{
			if (Application.isMobilePlatform) { button.OnPointerUpEvent += OnButton; } else { button.OnPointerDownEvent += OnButton; }
			levelSelect = _screen;
		}

		void OnButton (Touchable _sender, UnityEngine.EventSystems.PointerEventData _eventData)
		{
			levelSelect.OnLevelButton(this);
		}

		public void setStars(int _stars)
		{
			for(int i = 0; i < 3; i++)
			{
				stars [i].enabled = i < _stars ? true : false;
			}
		}
	}
}
