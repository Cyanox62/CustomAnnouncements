using System;
using System.IO;
using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
	class ChaosSpawnCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;
		private Announcement ea;

		public ChaosSpawnCommand(Plugin plugin)
		{
			ea = new Announcement(GetUsage(), "Chaos spawn announcement set.", "Chaos spawn announcement cleared.", CustomAnnouncements.chaosFilePath);

			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_chaosspawn_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(CS / CHAOSSPAWN) (SET / CLEAR) (TEXT)";
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
						string text = CustomAnnouncements.StringArrayToString(args, 1);
						return ea.SetAnnouncement(text);
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
