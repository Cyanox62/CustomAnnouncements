using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;
using System.IO;
using System.Collections.Generic;

namespace CustomAnnouncements
{
	class PresetCommand : ICommandHandler
	{
		private KeyAnnouncement va;
		private Plugin plugin;
		private string[] whitelist;

		public PresetCommand(Plugin plugin)
		{
			va = new KeyAnnouncement(GetUsage(), CustomAnnouncements.presetFilePath);
			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_preset_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(PR / PRESET) (SAVE / LOAD / REMOVE / LIST) (NAME) (TEXT)";
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
				if (args[0].ToLower() == "save")
				{
					if (args.Length > 2)
					{ 
						string name = args[1];

						if (name.ToLower() == "all" || name == "*")
						{
							return new string[] { "Error: invalid preset name." };
						}

						return va.SetVariable(name, CustomAnnouncements.StringArrayToString(args, 2), "Error: Preset name already exists.", "Saved preset \"" + name + "\".");
					}
				}
				else if (args[0].ToLower() == "load")
				{
					if (args.Length > 1)
					{
						return va.LoadVariable(args[1], "Error: no presets saved.", "Error: couldn't find preset \"" + args[1] + "\".", "Loaded preset \"" + args[1] + "\".");
					}
				}
				else if (args[0].ToLower() == "remove")
				{
					if (args.Length > 1)
					{
						return va.RemoveVariable(args[1], "Error: there are no saved presets.", "Error: couldn't find preset \"" + args[1] + "\".", "Removed all presets.", "Removed preset \"" + args[1] + "\".");
					}
				}
				else if (args[0].ToLower() == "list")
				{ 
					return va.ListVariables("Error: there are no saved presets.");
				}
			}
			return new string[] { GetUsage() };
		}
	}
}
