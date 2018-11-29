using System;
using Smod2;
using Smod2.Commands;

namespace CustomAnnouncements
{
    class MTFAnnouncementCommand : ICommandHandler
    {
		private Announcement an;
        private Plugin plugin;

        public MTFAnnouncementCommand(Plugin plugin)
        {
			an = new Announcement(GetUsage(), "ca_mtf_whitelist");
			this.plugin = plugin;
		}

        public string GetCommandDescription()
        {
            return "Creates custom CASSIE announcements.";
        }

        public string GetUsage()
        {
            return "(MTFA / MTFANNOUNCEMENT) (SCPS LEFT) (MTF NUMBER) (MTF LETTER)";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
			CustomAnnouncements.ann = UnityEngine.Object.FindObjectOfType<NineTailedFoxAnnouncer>();
			if (!an.CanRunCommand(sender))
				return new string[] { "You are not allowed to run this command." };

			if (args.Length == 3)
            {
                if (Int32.TryParse(args[0], out int a))
                {
                    int scpsLeft = a;
                }
                else
                {
                    return new string[] { "Error: not a valid number." };
                }

				if (Int32.TryParse(args[1], out int b))
				{
					int mtfNumber = b;
				}
				else
				{
					return new string[] { "Error: not a valid number." };
				}

				if (char.TryParse(args[2], out char c))
                {
                    char scpsLeft = c;
                }
                else
                {
					return new string[] { "Error: not a valid letter." };
				}

				return an.PlayMTFAnnouncement(a, b, c, "Announcement has been made.");
            }
            else
            {
                return new string[] { GetUsage() };
            }

        }
    }
}
