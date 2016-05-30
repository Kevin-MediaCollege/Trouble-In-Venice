using UnityEngine;
using System.Collections;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreenManager : MonoBehaviour
	{
		/// <summary>
		/// ScreenManager singleton
		/// </summary>
		public static ScreenManager instance;

		/// <summary>
		/// List of screens
		/// </summary>
		public ScreenBase[] screenList;
		private int screenListLenght;
		private ScreenBase currentScreen;

		/// <summary>
		/// ScreenBase to load on Awake
		/// </summary>
		public string startScreen;
		private bool switching;

		public static string lastLoadedLevel = "";

		protected void Awake()
		{
			instance = this;
			switching = false;
			screenListLenght = screenList.Length;

			for(int i = 0; i < screenListLenght; i++)
			{
				screenList[i].gameObject.SetActive(false);
			}
		}

		protected void Start()
		{
			if(startScreen == "ScreenStart" && !string.IsNullOrEmpty(lastLoadedLevel))
			{
				SwitchScreen("ScreenLevelSelect");
			}
			else
			{
				SwitchScreen(startScreen);
			}
		}

		protected void OnEnable()
		{
			DebugCommand.RegisterCommand(OnSetScreenCommand, "screen", "[name]");
		}

		protected void OnDisable()
		{
			DebugCommand.UnregisterCommand(OnSetScreenCommand);
		}

		/// <summary>
		/// Switch to a different ScreenBase
		/// </summary>
		/// <param name="_screen">Name of the screen to switch to</param>
		public static bool SwitchScreen(string _screen)
		{
			ScreenBase nextScreen;
			if(!instance.switching && (nextScreen = instance.GetScreenByName(_screen)) != null)
			{
				instance.switching = true;
				instance.StartCoroutine(instance.SetScreen(nextScreen));
				return true;
			}
			return false;
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

		public static string GetCurrentScreenName()
		{
			return instance != null ? instance.currentScreen != null ? instance.currentScreen.GetScreenName() : "NULL" : "NULL";
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
