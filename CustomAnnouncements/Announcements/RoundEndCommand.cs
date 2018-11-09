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
		private Announcement an;

		public RoundEndCommand(Plugin plugin)
		{
			an = new Announcement(CustomAnnouncements.RoundEndFilePath, GetUsage(), "ca_roundend_whitelist");
			this.plugin = plugin;
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
			if (!an.CanRunCommand(sender))
				return new string[] { "You are not allowed to run this command." };

			if (args.Length > 0)
			{
				if (args[0].ToLower() == "set")
				{
					if (args.Length > 1)
					{
						return an.SetAnnouncement(CustomAnnouncements.StringArrayToString(args, 1), "Round end announcement set.");
					}
				}
				else if (args[0].ToLower() == "clear")
				{
					return an.ClearAnnouncement("Round end announcement cleared.");
				}
			}
			return new string[] { GetUsage() };
		}
	}
}
