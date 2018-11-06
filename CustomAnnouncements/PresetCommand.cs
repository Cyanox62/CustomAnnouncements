using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;
using System.IO;
using System.Collections.Generic;

namespace CustomAnnouncements
{
	class PresetCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;

		public PresetCommand(Plugin plugin)
		{
			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_preset_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(PR / PRESET) (SAVE / LOAD / REMOVE / LIST) (NAME) (TEXT)";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			CustomAnnouncements.ann = UnityEngine.Object.FindObjectOfType<NineTailedFoxAnnouncer>();
			if (sender is Player)
			{
				Player player = (Player)sender;
				if (!CustomAnnouncements.IsPlayerWhitelisted(player, whitelist))
				{
					return new string[] { "You are not allowed to run this command." };
				}
			}

			if (args.Length > 0)
			{
				if (args[0].ToLower() == "save")
				{
					if (args.Length > 2)
					{
						string saveText = null;
						string name = args[1];

						saveText = CustomAnnouncements.StringArrayToString(args, 2);

						if (saveText.Length > 0)
						{
							string text = CustomAnnouncements.NonValidText(saveText.Split(' '));
							if (text != null)
							{
								return new string[] { "Error: phrase \"" + text + "\" is not in text to speech." };
							}
						}
						else
						{
							return new string[] { GetUsage() };
						}

						int output = CustomAnnouncements.AddLineToFile(CustomAnnouncements.presetFilePath, name, saveText);

						if (output == -1)
						{
							return new string[] { "Error: Preset name already exists." };
						}
						return new string[] { "Saved preset \"" + name + "\"." };
					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "load")
				{
					if (args.Length > 1)
					{
						string text = null;
						string name = args[1];

						string[] currentText = File.ReadAllLines(CustomAnnouncements.presetFilePath);

						if (currentText.Length > 0)
						{
							foreach (string str in currentText)
							{
								if (str.Split(':')[0].ToLower() == name.ToLower())
								{
									text = str.Substring(str.IndexOf(':'));
								}
							}
						}
						else
						{
							return new string[] { "Error: no presets saved." };
						}

						if (text == null)
						{
							return new string[] { "Error: couldn't find preset \"" + name + "\"." };
						}

						plugin.pluginManager.Server.Map.AnnounceCustomMessage(CustomAnnouncements.ReplaceVariables(text));
						return new string[] { "Loaded preset \"" + name + "\"." };
					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "remove")
				{
					if (args.Length > 1)
					{
						if (args[1].ToLower() == "all" || args[1] == "*")
						{
							File.WriteAllText(CustomAnnouncements.presetFilePath, String.Empty);
							return new string[] { "Removed all presets." };
						}

						string name = args[1];

						string[] currentText = File.ReadAllLines(CustomAnnouncements.presetFilePath);
						List<string> newText = new List<string>();

						if (currentText.Length > 0)
						{
							if (CustomAnnouncements.RemoveLineFromFile(currentText, name, CustomAnnouncements.presetFilePath) == -1)
							{
								return new string[] { "Error: couldn't find preset \"" + name + "\"." };
							}
							else
							{
								return new string[] { "Removed player \"" + name + "\"." };
							}
						}
						else
						{
							return new string[] { GetUsage() };
						}
					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "list")
				{
					string[] presets = File.ReadAllLines(CustomAnnouncements.presetFilePath);
					if (presets.Length > 0)
					{
						return presets;
					}
					else
					{
						return new string[] { "There are no saved presets." };
					}
				}
				else
				{
					return new string[] { GetUsage() };
				}
			}
			else
			{
				return new string[] { GetUsage() };
			}
		}
	}
}
