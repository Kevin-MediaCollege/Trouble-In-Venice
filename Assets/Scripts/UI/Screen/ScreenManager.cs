using UnityEngine;
using System.Collections;

namespace Proeve
{
	public class ScreenManager : MonoBehaviour
	{
		public static ScreenManager instance;

		public ScreenBase[] screenList;
		private int screenListLenght;
		private ScreenBase currentScreen;

		public string startScreen;
		private bool switching;

		void Awake()
		{
			instance = this;
			switching = false;
			screenListLenght = screenList.Length;

			for(int i = 0; i < screenListLenght; i++)
			{
				screenList[i].gameObject.SetActive(false);
			}
		}

		void Start()
		{
			SwitchScreen(startScreen);
		}

		public void SwitchScreen(string _screen)
		{
			ScreenBase nextScreen;
			if(!switching && (nextScreen = GetScreenByName(_screen)) != null)
			{
				switching = true;
				StartCoroutine(SetScreen(nextScreen));
			}
		}

		private IEnumerator SetScreen(ScreenBase _screen)
		{
			if(currentScreen != null)
			{
				yield return currentScreen.OnScreenFadeout();
				currentScreen.OnScreenExit();
				currentScreen.gameObject.SetActive(false);
				currentScreen = null;
			}

			yield return null;

			currentScreen = _screen;
			currentScreen.gameObject.SetActive(true);
			currentScreen.OnScreenEnter();

			switching = false;
		}

		private ScreenBase GetScreenByName(string _name)
		{
			for(int i = 0; i < screenListLenght; i++)
			{
				if(_name == screenList[i].GetScreenName())
				{
					return screenList[i];
				}
			}

			Debug.LogError("screen name not found!");
			return null;
		}
	}
}
