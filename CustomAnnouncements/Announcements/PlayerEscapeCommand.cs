using Smod2;
using Smod2.Commands;

namespace CustomAnnouncements
{
	class PlayerEscapeCommand : ICommandHandler
	{
		private Announcement an;
		private Plugin plugin;

		public PlayerEscapeCommand(Plugin plugin)
		{
			an = new Announcement(GetUsage(), "ca_playerescape_whitelist", CustomAnnouncements.PlayerEscapeFilePath);
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(PE / PLAYERESCAPE) (SET / CLEAR / VIEW) (TEXT)";
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
						return an.SetAnnouncement(text, "Player escape announcement set.");
					}
				}
				else if (args[0].ToLower() == "view")
				{
					return an.ViewAnnouncement("Error: announcement has not been set.");
				}
				else if (args[0].ToLower() == "clear")
				{
					return an.ClearAnnouncement("Player escape announcement cleared.");
				}
			}
			return new string[] { GetUsage() };
		}
	}
}
