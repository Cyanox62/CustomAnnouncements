using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
    class MTFAnnouncementCommand : ICommandHandler
    {
        private Plugin plugin;
		private string[] whitelist;

        public MTFAnnouncementCommand(Plugin plugin)
        {
            this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_mtf_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
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
			if (sender is Player)
			{
				Player player = (Player)sender;
				if (!CustomAnnouncements.IsPlayerWhitelisted(player, whitelist))
				{
					return new string[] { "You are not allowed to run this command." };
				}
			}

			if (args.Length == 3)
            {
                if (Int32.TryParse(args[0], out int a))
                {
                    int scpsLeft = a;
                }
                else
                {
                    return new string[] { "Not a valid number!" };
                }

				if (Int32.TryParse(args[1], out int b))
				{
					int mtfNumber = b;
				}
				else
				{
					return new string[] { "Not a valid number!" };
				}

				if (char.TryParse(args[2], out char c))
                {
                    char scpsLeft = c;
                }
                else
                {
                    return new string[] { "Not a valid letter!" };
                }

                plugin.pluginManager.Server.Map.AnnounceNtfEntrance(a, b, c);
                return new string[] { "Announcement has been made." };
            }
            else
            {
                return new string[] { GetUsage() };
            }

        }
    }
}
