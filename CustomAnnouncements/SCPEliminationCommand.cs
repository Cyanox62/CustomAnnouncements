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
        private Plugin plugin;

        public SCPEliminationCommand(Plugin plugin)
        {
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
            if (args.Length > 0)
            {
                if (Int32.TryParse(args[0], out int a))
                {
                    plugin.pluginManager.Server.Map.AnnounceScpKill(args[0]);
                }
                else
                {
                    return new string[] { "Not a valid number!" };
                }

                return new string[] { "Announcement has been made." };
            }
            else
            {
                return new string[] { GetUsage() };
            }

        }
    }
}
