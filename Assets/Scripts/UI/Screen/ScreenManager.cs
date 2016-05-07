using UnityEngine;
using System.Collections;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenManager : MonoBehaviour
	{
		public static ScreenManager instance;

		public ScreenBase[] screenList;
		private int screenListLenght;
		private ScreenBase currentScreen;

		public string startScreen;
		private bool switching;

		protected void Awake()
		{
			instance = this;
			switching = false;
			screenListLenght = screenList.Length;
			DebugCommand.RegisterCommand(OnSetScreenCommand, "screen", "[name]");

			for(int i = 0; i < screenListLenght; i++)
			{
				screenList[i].gameObject.SetActive(false);
			}
		}

		protected void Start()
		{
			SwitchScreen(startScreen);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_screen"></param>
		public bool SwitchScreen(string _screen)
		{
			ScreenBase nextScreen;
			if(!switching && (nextScreen = GetScreenByName(_screen)) != null)
			{
				switching = true;
				StartCoroutine(SetScreen(nextScreen));
				return true;
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_screen"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_name"></param>
		/// <returns></returns>
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

		private void OnSetScreenCommand(string[] _params)
		{
			if(_params.Length > 0 && !string.IsNullOrEmpty(_params[0]))
			{
				if(SwitchScreen(_params[0]))
				{
					DebugConsole.Log("Switching to screen: " + _params[0]);	
				}
				else
				{
					DebugConsole.Log("Cannot switch to screen: " + _params[0], new Color32(255, 0, 0, 255));
				}
			}
		}
	}
}
