using System;
using System.IO;
using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
	class RoundEndCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;

		public RoundEndCommand(Plugin plugin)
		{
			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_roundend_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(RE / ROUNDEND) (SET / CLEAR) (TEXT)";
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
						string saveText = CustomAnnouncements.StringArrayToString(args, 1);

						if (saveText.Length > 0)
						{
							string text = CustomAnnouncements.NonValidText(saveText.Split(' '));
							if (text != null)
							{
								return new string[] { "Error: phrase \"" + text + "\" is not in text to speech." };
							}
						}
						else
						{
							return new string[] { GetUsage() };
						}

						File.WriteAllText(CustomAnnouncements.roundendFilePath, String.Empty);
						File.AppendAllText(CustomAnnouncements.roundendFilePath, saveText);

						return new string[] { "Round end announcement set." };
					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "clear")
				{
					File.WriteAllText(CustomAnnouncements.roundendFilePath, String.Empty);
					return new string[] { "Round end announcement cleared." };
				}
				else
				{
					return new string[] { GetUsage() };
				}
			}
			else
			{
				return new string[] { GetUsage() };
			}
		}
	}
}
