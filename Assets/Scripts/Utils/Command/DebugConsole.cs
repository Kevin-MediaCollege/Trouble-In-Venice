using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Proeve
{		
	/// <summary>
	/// 
	/// </summary>
	public class DebugConsole : MonoBehaviour 
	{
		/// <summary>
		/// DebugConsole singleton
		/// </summary>
		public static DebugConsole instance;
		private static List<LogItem> log = init();

		private Texture2D background;
		private bool consoleEnabled = false;
		private string currentInput = "";
		private string currentSuggestion = "";
		private float backspaceDelay = 0f;
		private GUIStyle style = new GUIStyle();

		void Awake()
		{
			instance = this;

			background = new Texture2D(1, 1);
			background.SetPixel(0, 0, new Color32(0, 0, 0, 120));
			background.Apply();
		}

		/// <summary>
		/// Creates the log
		/// </summary>
		public static List<LogItem> init()
		{
			List<LogItem> l = new List<LogItem>();
			for(int i = 0; i < 10; i++) { l.Add(new LogItem("", Color.white)); }
			return l;
		}

		protected void OnEnable()
		{
			instance = this;
		}

		protected void OnDisable()
		{
			if (instance == this)
			{
				instance = null;
			}
		}

		protected void Update()
		{
			if(Input.GetKeyDown(KeyCode.BackQuote) && instance == this)
			{
				consoleEnabled = !consoleEnabled;
				backspaceDelay = 0f;
			}

			if(consoleEnabled)
			{
				int l;
				bool updated = false;
				if((l = Input.inputString.Length) > 0)
				{
					char c;
					for(int i = 0; i < l; i++)
					{
						c = Input.inputString[i];

						if(char.IsLetterOrDigit(c) || c == ' ' || c == '.' || c == '-' || c == '_')
						{
							currentInput += c;
							updated = true;
						}
					}
				}

				if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKey(KeyCode.Backspace))
				{
					if(backspaceDelay <= 0f && currentInput.Length > 0)
					{
						currentInput = currentInput.Remove(currentInput.Length - 1, 1);
						updated = true;

						if(Input.GetKeyDown(KeyCode.Backspace))
						{
							backspaceDelay = 0.2f;
						}
						else
						{
							backspaceDelay = 0.05f;
						}
					}
					else
					{
						backspaceDelay -= Time.deltaTime;
					}
				}
				else
				{
					backspaceDelay = 0f;
				}

				if(Input.GetKeyDown(KeyCode.Return) && currentInput.Length > 0)
				{
					DebugCommand.RunCommand(currentInput);
					currentInput = "";
					updated = true;
				}

				if(updated)
				{
					updateSuggestion();
				}
			}
		}

		protected void OnGUI()
		{
			if(consoleEnabled)
			{
				GUI.DrawTexture(new Rect(0f, Screen.height - 172f, 600f, 172f), background);
				for(int i = 0; i < 10; i++)
				{
					style.normal.textColor = log[i].color;
					GUI.Label(new Rect(3f, Screen.height - 22f - (15f * (10 - i)), 600f, 25f), log[i].message, style);
				}

				GUI.DrawTexture(new Rect(0f, Screen.height - 18f, 600f, 18f), background);
				GUI.Label(new Rect(3f, Screen.height - 20f, 600f, 25f), currentInput + (backspaceDelay > 0f || Mathf.FloorToInt((Time.timeSinceLevelLoad * 3f)) % 2 == 0 ? "|" : ""));

				style.normal.textColor = new Color32(255, 255, 255, 85);
				style.alignment = TextAnchor.MiddleLeft;
				style.fontSize = 13;
				Vector2 inputWidth = style.CalcSize(new GUIContent(currentInput));
				GUI.Label(new Rect(3f + inputWidth.x, Screen.height - 22f, 600f, 25f), currentSuggestion, style);
			}
		}
			
		private void updateSuggestion()
		{
			if(currentInput.Length > 0)
			{
				CommandData cd = DebugCommand.GetCommandSuggestion(currentInput);
				if(cd != null)
				{
					if(currentInput.Length < cd.commandName.Length)
					{
						currentSuggestion = cd.commandName.Remove(0, currentInput.Length) + " " + cd.example;
						return;
					}
					else if(cd.example.Length > 0)
					{
						string s = currentInput.Remove(0, cd.commandName.Length);
						if(s.Length > 1 && s[0] == ' ')
						{
							s = s.Remove(0, 1);
							s = s[s.Length - 1] == ' ' ? s.Remove(s.Length - 1, 1) : s;
							int currentParameterCount = s.Split(' ').Length;
							string[] suggestedParameters = cd.example.Split(' ');

							currentSuggestion = "";
							for(int i = currentParameterCount; i < suggestedParameters.Length; i++)
							{
								currentSuggestion += (currentInput[currentInput.Length - 1] != ' ' ? " " : "") + suggestedParameters[i];
							}
						}
						else
						{
							currentSuggestion = s.Length == 1 ? cd.example : " " + cd.example;
						}
						return;
					}
				}
			}
			currentSuggestion = "";
		}

		public class LogItem
		{
			public string message;
			public Color32 color;

			/// <summary>
			/// Stores info about a log item
			/// </summary>
			/// <param name="_message">log message</param>
			/// <param name="_color">log color</param>
			public LogItem(string _message, Color32 _color)
			{
				message = _message;
				color = _color;
			}
		}

		/// <summary>
		/// Adds a new message to the console log
		/// </summary>
		/// <param name="_message">message to log</param>
		/// <param name="_color">log color</param>
		public static void Log(string _message, Color32 _color)
		{
			log.RemoveAt(0);
			log.Add(new LogItem(_message, _color));
			Debug.Log(_message);
		}

		/// <summary>
		/// Adds a new message to the console log
		/// </summary>
		/// <param name="_message">message to log</param>
		public static void Log(string _message)
		{
			Log(_message, new Color32(255, 255, 255, 255));
		}
	}
}
