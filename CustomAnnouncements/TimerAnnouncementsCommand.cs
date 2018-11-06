using System;
using Smod2;
using Smod2.Commands;
using System.IO;
using Smod2.API;
using System.Collections.Generic;

namespace CustomAnnouncements
{
	class TimerAnnouncementsCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;

		public TimerAnnouncementsCommand(Plugin plugin)
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
			return "(TIA / TIMEDANNOUNCEMENT) (ADD / REMOVE / LIST) (TIME) (TEXT)";
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
				if (args[0].ToLower() == "add")
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

						if (currentText.Length > 0)
						{
							foreach (string str in currentText)
							{
								if (str.Split(':')[0] == time.ToString())
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

						File.AppendAllText(CustomAnnouncements.timerFilePath, time + ": " + saveText + Environment.NewLine);
						return new string[] { "Saved timer." };

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
						string time = args[1];

						string[] currentText = File.ReadAllLines(CustomAnnouncements.timerFilePath);
						List<string> newText = new List<string>();

						if (currentText.Length > 0)
						{
							int val = currentText.Length;
							int count = 0;
							foreach (string str in currentText)
							{
								if (str.Split(':')[0] != time)
								{
									newText.Add(str);
									count++;
								}
							}

							if (val == count)
							{
								return new string[] { "Error: couldn't find timer." };
							}

							File.WriteAllText(CustomAnnouncements.timerFilePath, String.Empty);

							foreach (string str in newText.ToArray())
							{
								File.AppendAllText(CustomAnnouncements.timerFilePath, str + Environment.NewLine);
							}
							return new string[] { "Removed timer." };
						}
						else
						{
							return new string[] { "Error: there are no saved presets." };
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
						return new string[] { "There are no timers." };
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
