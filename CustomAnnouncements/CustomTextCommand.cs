using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
	class CustomTextCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;

		public CustomTextCommand(Plugin plugin)
		{
			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_text_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(TA / TEXTANNOUNCEMENT) (TEXT)";
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
				foreach (string str in args)
				{
					if (!CustomAnnouncements.IsVoiceLine(str))
					{
						return new string[] { "Error: phrase \"" + str + "\" is not in text to speech." };
					}
				}
				plugin.pluginManager.Server.Map.AnnounceCustomMessage(string.Join(" ", args));
				return new string[] { "Announcement has been made." };
			}
			else
			{
				return new string[] { GetUsage() };
			}
		}
	}
}