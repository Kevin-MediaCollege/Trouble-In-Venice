using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// Saves and runs commands
	/// </summary>
	public static class DebugCommand 
	{
		public static List<CommandData> commandList;
		public static int commandListLenght = 0;

		/// <summary>
		/// Adds command to the command list
		/// </summary>
		/// <param name="_action">Saved action</param>
		/// <param name="_commandName">Name of the command</param>
		/// <param name="_example">Parameters example, Visible when typing in console</param>
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
		/// Removes command from the commands list
		/// </summary>
		/// <param name="_action">Saved action</param>
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
		/// Calls all commands in the command list with a matching name
		/// </summary>
		/// <param name="_input">Command name with parameters</param>
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
		/// Finds a matching command, Returns null if nothing is found.
		/// </summary>
		/// <param name="_suggestion">Command suggestion</param>
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

		private static void init()
		{
			commandList = new List<CommandData>();
			DebugCommand.RegisterCommand(OnCommandHelp, "help", "[page]");
		}

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

	/// <summary>
	/// 
	/// </summary>
	public class CommandData
	{
		/// <summary>
		/// This saved action will be called when this command is entered in console.
		/// </summary>
		public Action<string[]> action;
		/// <summary>
		/// Name of the command
		/// </summary>
		public string commandName;
		/// <summary>
		/// Parameters example, Visible when typing in console
		/// </summary>
		public string example;

		/// <summary>
		/// Creates a new CommandData class
		/// </summary>
		/// <param name="_action">This saved action will be called when this command is entered in console.</param>
		/// <param name="_commandName">Name of the command</param>
		/// <param name="_example">Parameters example, Visible when typing in console</param>
		public CommandData(Action<string[]> _action, string _commandName, string _example)
		{
			action = _action;
			commandName = _commandName;
			example = _example;
		}
	}
}