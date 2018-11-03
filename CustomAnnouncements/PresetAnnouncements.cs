using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;
using System.IO;
using System.Collections.Generic;

namespace CustomAnnouncements
{
	class PresetAnnouncements : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;

		public PresetAnnouncements(Plugin plugin)
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
			return "(PA / PRESETANNOUNCEMENT) (SAVE / LOAD / REMOVE / LIST) (NAME)";
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

						for (int i = 2; i < args.Length; i++)
						{
							saveText += args[i];
							if (i != args.Length - 1)
								saveText += " ";
						}

						string[] currentText = File.ReadAllLines(CustomAnnouncements.presetFilePath);

						if (currentText.Length > 0)
						{
							foreach (string str in currentText)
							{
								if (str.Split(':')[0].ToLower() == name.ToLower())
								{
									return new string[] { "Error: Preset name already exists." };
								}
							}
						}

						foreach (string str in saveText.Split(' '))
						{
							if (!CustomAnnouncements.IsVoiceLine(str))
							{
								return new string[] { "Error: phrase \"" + str + "\" is not in text to speech." };
							}
						}

						File.AppendAllText(CustomAnnouncements.presetFilePath, name + ": " + saveText + Environment.NewLine);
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

						plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
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
						string name = args[1];

						string[] currentText = File.ReadAllLines(CustomAnnouncements.presetFilePath);
						List<string> newText = new List<string>();

						if (currentText.Length > 0)
						{
							foreach (string str in currentText)
							{
								if (str.Split(':')[0].ToLower() != name.ToLower())
								{
									newText.Add(str);
								} // FIXME : determine if the preset was actually found, and say 'preset not found' if not
							}
						}
						else
						{
							return new string[] { "Error: there are no saved presets." };
						}

						File.WriteAllText(CustomAnnouncements.presetFilePath, String.Empty);

						foreach (string str in newText.ToArray())
						{
							File.AppendAllText(CustomAnnouncements.presetFilePath, str + Environment.NewLine);
						}
						return new string[] { "Removed preset \"" + name + "\"" };
					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "list")
				{
					string[] presets = File.ReadAllLines(CustomAnnouncements.presetFilePath);

					return presets;
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
