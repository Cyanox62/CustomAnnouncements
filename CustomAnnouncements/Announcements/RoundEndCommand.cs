using System;
using System.IO;
using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
	class RoundEndCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;
		private Announcement ea;

		public RoundEndCommand(Plugin plugin)
		{
			ea = new Announcement(GetUsage(), "Round end announcement set.", "Round end announcement cleared.", CustomAnnouncements.roundendFilePath);

			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_roundend_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(RE / ROUNDEND) (SET / CLEAR) (TEXT)";
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
						return ea.SetAnnouncement(CustomAnnouncements.StringArrayToString(args, 1));
					}
				}
				else if (args[0].ToLower() == "clear")
				{
					return ea.ClearAnnouncement();
				}
			}
			return new string[] { GetUsage() };
		}
	}
}
