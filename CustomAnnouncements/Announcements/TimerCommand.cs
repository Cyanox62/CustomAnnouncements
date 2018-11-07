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
		private KeyAnnouncement va;
		private Plugin plugin;
		private string[] whitelist;

		public TimerCommand(Plugin plugin)
		{
			va = new KeyAnnouncement(GetUsage(), CustomAnnouncements.timerFilePath);
			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_timer_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
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
						int time = 0;
						if (Int32.TryParse(args[1], out int a))
							time = a;
						else
							return new string[] { "Error: invalid time" };

						if (args.Length > 2)
							return va.SetVariable(time.ToString(), CustomAnnouncements.StringArrayToString(args, 2), "Error: Preset name already exists.", "Saved timer.");

					}
				}
				else if (args[0].ToLower() == "remove")
				{
					if (args.Length > 1)
					{
						return va.RemoveVariable(args[1], "Error: there are no set timers.", "Error: couldn't find timer.", "Removed all timers.", "Removed timer.");
					}
				}
				else if (args[0].ToLower() == "list")
				{
					return va.ListVariables("Error: there are no set timers.");
				}
			}
			return new string[] { GetUsage() };
		}
	}
}
