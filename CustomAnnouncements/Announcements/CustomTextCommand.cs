using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
	class CustomTextCommand : ICommandHandler
	{
		private Announcement an;
		private Plugin plugin;

		public CustomTextCommand(Plugin plugin)
		{
			an = new Announcement(GetUsage(), "ca_text_whitelist");
			this.plugin = plugin;
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
			if (!an.CanRunCommand(sender))
				return new string[] { "You are not allowed to run this command." };

			if (args.Length > 0)
				return an.PlayCustomAnnouncement(CustomAnnouncements.StringArrayToString(args, 0), "Announcement has been made.");
			return new string[] { GetUsage() };
		}
	}
}