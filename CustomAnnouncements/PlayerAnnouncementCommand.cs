using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;
using System.IO;
using System.Collections.Generic;

namespace CustomAnnouncements
{
	class PlayerAnnouncementCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;

		public PlayerAnnouncementCommand(Plugin plugin)
		{
			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_player_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(PA / PLAYERANNOUNCEMENT) (SET / REMOVE / LIST) (STEAMID) (TEXT)";
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
						string saveText = null;
						string steamid = args[1];

						if (!ulong.TryParse(steamid, out ulong a))
						{
							return new string[] { "Error: invalid steamid." };
						}

						saveText = CustomAnnouncements.StringArrayToString(args, 2);

						foreach (string str in saveText.Split(' '))
						{
							if (!CustomAnnouncements.IsVoiceLine(str))
							{
								return new string[] { "Error: phrase \"" + str + "\" is not in text to speech." };
							}
						}

						int output = CustomAnnouncements.AddLineToFile(CustomAnnouncements.playerFilePath, steamid, saveText);

						if (output == -1)
						{
							return new string[] { "Error: Player id already exists." };
						}
						return new string[] { "Saved announcement for player \"" + steamid + "\"." };


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
							File.WriteAllText(CustomAnnouncements.playerFilePath, String.Empty);
							return new string[] { "Removed all player announcements." };
						}

						string steamid = args[1];

						string[] currentText = File.ReadAllLines(CustomAnnouncements.playerFilePath);
						List<string> newText = new List<string>();

						if (currentText.Length > 0)
						{
							if (CustomAnnouncements.RemoveLineFromFile(currentText, steamid, CustomAnnouncements.playerFilePath) == -1)
							{
								return new string[] { "Error: couldn't find player \"" + steamid + "\"." };
							}
							else
							{
								return new string[] { "Removed player \"" + steamid + "\"." };
							}
						}
						else
						{
							return new string[] { "There are no player announcements." };
						}
					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "list")
				{
					string[] players = File.ReadAllLines(CustomAnnouncements.playerFilePath);
					if (players.Length > 0)
					{
						return players;
					}
					else
					{
						return new string[] { "There are no player announcements." };
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