using System;
using System.Collections.Generic;
using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// Saves and runs commands.
	/// </summary>
	public static class DebugCommand 
	{
		private static List<CommandData> commands;

		/// <summary>
		/// Adds command to the command list.
		/// </summary>
		/// <param name="_action">Saved action.</param>
		/// <param name="_commandName">Name of the command.</param>
		/// <param name="_example">Parameters example, Visible when typing in console.</param>
		public static void RegisterCommand(Action<string[]> _action, string _commandName, string _example = "")
		{
			if(commands == null)
				Init();

			if(commands.Count == 0)
			{
				commands.Add(new CommandData(_action, _commandName, _example));
			}
			else
			{
				for(int i = 0; i < commands.Count; i++)
				{
					if(_commandName.CompareTo(commands[i].Name) == -1)
					{
						commands.Insert(i, new CommandData(_action, _commandName, _example));
						break;
					}
					else if(i == commands.Count - 1)
					{
						commands.Add(new CommandData(_action, _commandName, _example));
						break;
					}
				}
			}
		}

		/// <summary>
		/// Removes command from the commands list.
		/// </summary>
		/// <param name="_action">Saved action.</param>
		public static void UnregisterCommand(Action<string[]> _action)
		{
			if(commands != null)
			{
				foreach(CommandData data in commands)
				{
					if(data.Action == _action)
					{
						commands.Remove(data);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Calls all commands in the command list with a matching name.
		/// </summary>
		/// <param name="_input">Command name with parameters.</param>
		public static void RunCommand(string _input)
		{
			if(commands == null)
				Init();
			
			CommandData cd = GetCommandSuggestion(_input);
			if(cd != null && _input.StartsWith(cd.Name))
			{
				_input = _input.Replace(cd.Name, "");
				_input = _input.Length > 0 && _input[0] == ' ' ? _input.Remove(0, 1) : _input;
				cd.Action(_input.Length > 0 ? _input.Split(' ') : new string[0]);
				return;
			}
			DebugConsole.Log("Invalid command!", new Color32(255, 0, 0, 255));
		}

		/// <summary>
		/// Finds a matching command, Returns null if nothing is found.
		/// </summary>
		/// <param name="_suggestion">Command suggestion.</param>
		public static CommandData GetCommandSuggestion(string _suggestion)
		{
			if(commands != null && !string.IsNullOrEmpty(_suggestion))
			{
				int largestLenght = 0;
				CommandData largest = null;
				foreach(CommandData data in commands)
				{
					if(data.Name.StartsWith(_suggestion) || _suggestion.StartsWith(data.Name))
					{
						if(data.Name.StartsWith(_suggestion) && _suggestion.Length > largestLenght)
						{
							largestLenght = _suggestion.Length;
							largest = data;
						}
						else if(_suggestion.StartsWith(data.Name) && data.Name.Length > largestLenght)
						{
							if(_suggestion.Length > data.Name.Length && _suggestion[data.Name.Length] == ' ')
							{
								largestLenght = data.Name.Length;
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

		private static void Init()
		{
			commands = new List<CommandData>();
			DebugCommand.RegisterCommand(OnCommandHelp, "help", "[page]");
		}

		private static void OnCommandHelp(string[] _params)
		{
			int page = 0;
			if(_params.Length > 0 && !string.IsNullOrEmpty(_params[0]) && int.TryParse(_params[0], out page))
			{
				int max = Mathf.CeilToInt(DebugCommand.commands.Count / 9f);
				page = page < 1 ? 1 : page > max ? max : page;
				DebugConsole.Log("- - - - - - - - - - - help page " + page + " / " + max + " - - - - - - - - - - -");

				for(int i = 0; i < 9; i++)
				{
					if(DebugCommand.commands.Count > ((page - 1) * 9) + i)
					{
						DebugConsole.Log(DebugCommand.commands[((page - 1) * 9) + i].Name + " " + DebugCommand.commands[((page - 1) * 9) + i].ParamExample, new Color32(255, 255, 0, 255));
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
	/// Data container for console commands.
	/// </summary>
	public class CommandData
	{
		/// <summary>
		/// This saved action will be called when this command is entered in console.
		/// </summary>
		public Action<string[]> Action { private set; get; }

		/// <summary>
		/// Name of the command.
		/// </summary>
		public string Name { private set; get; }

		/// <summary>
		/// Parameters example, Visible when typing in console.
		/// </summary>
		public string ParamExample { private set; get; }

		/// <summary>
		/// Creates a new CommandData class.
		/// </summary>
		/// <param name="_action">This saved action will be called when this command is entered in console.</param>
		/// <param name="_name">Name of the command.</param>
		/// <param name="_paramExample">Parameters example, Visible when typing in console.</param>
		public CommandData(Action<string[]> _action, string _name, string _paramExample)
		{
			Action = _action;
			Name = _name;
			ParamExample = _paramExample;
		}
	}
}