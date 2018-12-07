using System;
using System.IO;
using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
	class AutoWarheadCommand : ICommandHandler
	{
		private Announcement an;
		private Plugin plugin;

		public AutoWarheadCommand(Plugin plugin)
		{
			an = new Announcement(GetUsage(), "ca_autowarhead_whitelist", CustomAnnouncements.WarheadAutoStartFilePath);
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(AW / AUTOWARHEAD) (SET / CLEAR / VIEW) (TEXT)";
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
						string text = CustomAnnouncements.StringArrayToString(args, 1);
						return an.SetAnnouncement(text, "Auto warhead announcement set.");
					}
				}
				else if (args[0].ToLower() == "view")
				{
					return an.ViewAnnouncement("Error: announcement has not been set.");
				}
				else if (args[0].ToLower() == "clear")
				{
					return an.ClearAnnouncement("Auto warhead announcement cleared.");
				}
			}
			return new string[] { GetUsage() };
		}
	}
}
