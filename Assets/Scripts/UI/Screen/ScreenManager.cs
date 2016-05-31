using UnityEngine;
using System.Collections;

namespace Proeve
{
	/// <summary>
	/// Manages all screens.
	/// </summary>
	public class ScreenManager : MonoBehaviour
	{
		public static ScreenManager instance;

		public static string lastLoadedLevel = "";

		[SerializeField] private ScreenBase[] screenList;
		[SerializeField] private string startScreen;

		private ScreenBase currentScreen;
		private bool switching;

		protected void Awake()
		{
			instance = this;
			switching = false;

			for(int i = 0; i < screenList.Length; i++)
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

		private ScreenBase GetScreenByName(string _name)
		{
			for(int i = 0; i < screenList.Length; i++)
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
		/// Switch to a different screen.
		/// </summary>
		/// <remarks>
		/// The screen name must be equal to <see cref="ScreenBase.GetScreenName"/>.
		/// </remarks>
		/// <param name="_screen">The name of the screen to switch to.</param>
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

		/// <summary>
		/// Get the name of the current screen.
		/// </summary>
		/// <returns>The name of the current screen.</returns>
		public static string GetCurrentScreenName()
		{
			return instance != null ? instance.currentScreen != null ? instance.currentScreen.GetScreenName() : "NULL" : "NULL";
		}
	}
}
