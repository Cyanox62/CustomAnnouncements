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
		private KeyAnnouncement va;
		private Plugin plugin;
		private string[] whitelist;

		public PlayerAnnouncementCommand(Plugin plugin)
		{
			va = new KeyAnnouncement(GetUsage(), CustomAnnouncements.playerFilePath);
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
				if (args[0].ToLower() == "save")
				{
					if (args.Length > 2)
					{
						string steamid = args[1];

						if (!ulong.TryParse(steamid, out ulong a))
						{
							return new string[] { "Error: invalid steamid." };
						}

						return va.SetVariable(steamid, CustomAnnouncements.StringArrayToString(args, 2), "Error: Player id already exists.", "Saved announcement for player \"" + steamid + "\".");
					}
				}
				else if (args[0].ToLower() == "remove")
				{
					if (args.Length > 1)
					{
						return va.RemoveVariable(args[1], "Error: there are no player announcements.", "Error: couldn't find player \"" + args[1] + "\".", "Removed all player announcements.", "Removed player \"" + args[1] + "\".");
					}
				}
				else if (args[0].ToLower() == "list")
				{
					return va.ListVariables("Error: there are no player announcements.");
				}
			}
			return new string[] { GetUsage() };
		}
	}
}