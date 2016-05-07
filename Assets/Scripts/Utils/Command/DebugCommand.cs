using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	public static class DebugCommand 
	{
		public static List<CommandData> commandList;
		public static int commandListLenght = 0;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_action"></param>
		/// <param name="_commandName"></param>
		/// <param name="_example"></param>
		public static void RegisterCommand(Action<string[]> _action, string _commandName, string _example = "")
		{
			if(commandList == null)
				init();

			if(commandListLenght == 0)
			{
				commandList.Add(new CommandData(_action, _commandName, _example));
				commandListLenght++;
			}
			else
			{
				for(int i = 0; i < commandListLenght; i++)
				{
					if(_commandName.CompareTo(commandList[i].commandName) == -1)
					{
						commandList.Insert(i, new CommandData(_action, _commandName, _example));
						commandListLenght++;
						break;
					}
					else if(i == commandListLenght - 1)
					{
						commandList.Add(new CommandData(_action, _commandName, _example));
						commandListLenght++;
						break;
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_action"></param>
		public static void UnregisterCommand(Action<string[]> _action)
		{
			if(commandList != null)
			{
				foreach(CommandData data in commandList)
				{
					if(data.action == _action)
					{
						commandList.Remove(data);
						commandListLenght--;
						break;
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_input"></param>
		public static void RunCommand(string _input)
		{
			if(commandList == null)
				init();
			
			CommandData cd = GetCommandSuggestion(_input);
			if(cd != null && _input.StartsWith(cd.commandName))
			{
				_input = _input.Replace(cd.commandName, "");
				_input = _input.Length > 0 && _input[0] == ' ' ? _input.Remove(0, 1) : _input;
				cd.action(_input.Length > 0 ? _input.Split(' ') : new string[0]);
				return;
			}
			DebugConsole.Log("Invalid command!", new Color32(255, 0, 0, 255));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_suggestion"></param>
		public static CommandData GetCommandSuggestion(string _suggestion)
		{
			if(commandList != null && !string.IsNullOrEmpty(_suggestion))
			{
				int largestLenght = 0;
				CommandData largest = null;
				foreach(CommandData data in commandList)
				{
					if(data.commandName.StartsWith(_suggestion) || _suggestion.StartsWith(data.commandName))
					{
						if(data.commandName.StartsWith(_suggestion) && _suggestion.Length > largestLenght)
						{
							largestLenght = _suggestion.Length;
							largest = data;
						}
						else if(_suggestion.StartsWith(data.commandName) && data.commandName.Length > largestLenght)
						{
							if(_suggestion.Length > data.commandName.Length && _suggestion[data.commandName.Length] == ' ')
							{
								largestLenght = data.commandName.Length;
								largest = data;
							}
						}
					}
				}

				if(largestLenght > 0)
				{
					return largest;
				}
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		private static void init()
		{
			commandList = new List<CommandData>();
			DebugCommand.RegisterCommand(OnCommandHelp, "help", "[page]");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_params"></param>
		private static void OnCommandHelp(string[] _params)
		{
			int page = 0;
			if(_params.Length > 0 && !string.IsNullOrEmpty(_params[0]) && int.TryParse(_params[0], out page))
			{
				int max = Mathf.CeilToInt(DebugCommand.commandListLenght / 9f);
				page = page < 1 ? 1 : page > max ? max : page;
				DebugConsole.Log("- - - - - - - - - - - help page " + page + " / " + max + " - - - - - - - - - - -");

				for(int i = 0; i < 9; i++)
				{
					if(DebugCommand.commandListLenght > ((page - 1) * 9) + i)
					{
						DebugConsole.Log(DebugCommand.commandList[((page - 1) * 9) + i].commandName + " " + DebugCommand.commandList[((page - 1) * 9) + i].example, new Color32(255, 255, 0, 255));
					}
				}
			}
			else
			{
				DebugConsole.Log("Invalid parameter! Example: help [page]", new Color32(255, 0, 0, 255));
			}
		}
	}

	public class CommandData
	{
		public Action<string[]> action;
		public string commandName;
		public string example;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_action"></param>
		/// <param name="_commandName"></param>
		/// <param name="_example"></param>
		public CommandData(Action<string[]> _action, string _commandName, string _example)
		{
			action = _action;
			commandName = _commandName;
			example = _example;
		}
	}
}