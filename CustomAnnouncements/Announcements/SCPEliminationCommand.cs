using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using System.Threading;

namespace CustomAnnouncements
{
    class SCPEliminationCommand : ICommandHandler
    {
		private Announcement an;
        private Plugin plugin;

        public SCPEliminationCommand(Plugin plugin)
        {
			an = new Announcement(GetUsage(), "ca_scp_whitelist");
			this.plugin = plugin;
		}

        public string GetCommandDescription()
        {
            return "Creates custom CASSIE announcements.";
        }

        public string GetUsage()
        {
            return "(SCPA / SCPANNOUNCEMENT) (SCP NUMBER)";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
			CustomAnnouncements.ann = UnityEngine.Object.FindObjectOfType<NineTailedFoxAnnouncer>();
			if (!an.CanRunCommand(sender))
				return new string[] { "You are not allowed to run this command." };

			if (args.Length > 0)
            {
                if (!Int32.TryParse(args[0], out int a))
                    return new string[] { "Error: not a valid number." };
				return an.PlaySCPAnnouncement(args[0], "Announcement has been made.");
            }
            else
            {
                return new string[] { GetUsage() };
            }

        }
    }
}
