using System;
using Smod2;
using Smod2.Commands;
using System.IO;
using Smod2.API;
using System.Collections.Generic;

namespace CustomAnnouncements
{
	class TimerCommand : ICommandHandler
	{
		private Announcement an;
		private Plugin plugin;

		public TimerCommand(Plugin plugin)
		{
			an = new Announcement(CustomAnnouncements.TimersFilePath, GetUsage(), plugin.GetConfigList("ca_timer_whitelist"));
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(TI / TIMER) (SET / REMOVE / LIST) (TIME) (TEXT)";
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
						int time = 0;
						if (Int32.TryParse(args[1], out int a))
							time = a;
						else
							return new string[] { "Error: invalid time" };

						if (args.Length > 2)
							return an.SetVariable(time.ToString(), CustomAnnouncements.StringArrayToString(args, 2), "Error: Preset name already exists.", "Saved timer.");

					}
				}
				else if (args[0].ToLower() == "remove")
				{
					if (args.Length > 1)
					{
						return an.RemoveVariable(args[1], "Error: there are no set timers.", "Error: couldn't find timer.", "Removed all timers.", "Removed timer.");
					}
				}
				else if (args[0].ToLower() == "list")
				{
					return an.ListVariables("Error: there are no set timers.");
				}
			}
			return new string[] { GetUsage() };
		}
	}
}
