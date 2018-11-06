using System;
using Smod2;
using Smod2.Commands;
using System.IO;
using Smod2.API;
using System.Collections.Generic;

namespace CustomAnnouncements
{
	class TimerCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;

		public TimerCommand(Plugin plugin)
		{
			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_timer_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(TI / TIMER) (SET / REMOVE / LIST) (TIME) (TEXT)";
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
				if (args[0].ToLower() == "set")
				{
					if (args.Length > 1)
					{
						string[] currentText = File.ReadAllLines(CustomAnnouncements.timerFilePath);
						string saveText = CustomAnnouncements.StringArrayToString(args, 2);
						int time = 0;

						if (Int32.TryParse(args[1], out int a))
						{
							time = a;
						}
						else
						{
							return new string[] { "Error: invalid time" };
						}

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

						int output = CustomAnnouncements.AddLineToFile(CustomAnnouncements.timerFilePath, time.ToString(), saveText);

						if (output == -1)
						{
							return new string[] { "Error: Preset name already exists." };
						}

						return new string[] { "Saved timer." };

					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "remove")
				{
					if (args.Length > 0)
					{
						if (args[1].ToLower() == "all" || args[1] == "*")
						{
							File.WriteAllText(CustomAnnouncements.timerFilePath, String.Empty);
							return new string[] { "Removed all timers." };
						}

						string time = args[1];

						string[] currentText = File.ReadAllLines(CustomAnnouncements.timerFilePath);
						List<string> newText = new List<string>();

						if (currentText.Length > 0)
						{
							if (CustomAnnouncements.RemoveLineFromFile(currentText, time, CustomAnnouncements.timerFilePath) == -1)
							{
								return new string[] { "Error: couldn't find timer." };
							}
							else
							{
								return new string[] { "Removed timer." };
							}
						}
						else
						{
							return new string[] { "Error: there are no set timers." };
						}
					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "list")
				{
					string[] currentText = File.ReadAllLines(CustomAnnouncements.timerFilePath);
					if (currentText.Length > 0)
					{
						return currentText;
					}
					else
					{
						return new string[] { "Error: there are no set timers." };
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
