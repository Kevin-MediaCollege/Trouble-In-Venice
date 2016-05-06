using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utils;
using UnityEngine.SceneManagement;

namespace Proeve
{
	public class LevelSelectionButton : MonoBehaviour
	{
		[SerializeField] private Button button;

		protected void Start()
		{
			string[] levelName = gameObject.name.Split('_');
			if(levelName.Length > 1)
			{
				LevelUnlocker levelUnlocker = Dependency.Get<LevelUnlocker>();
				int levelIndex = int.Parse(levelName[levelName.Length - 1]);

				button.interactable = levelUnlocker.IsUnlocked(levelIndex);
			}
		}

		protected void OnEnable()
		{
			button.onClick.AddListener(OnClick);
		}

		protected void OnDisable()
		{
			button.onClick.RemoveListener(OnClick);
		}

		protected void Reset()
		{
			button = GetComponent<Button>();
		}

		private void OnClick()
		{
			SceneManager.LoadSceneAsync(gameObject.name);
		}
	}
}
