using System;
using System.IO;
using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
	class ChaosSpawnCommand : ICommandHandler
	{
		private Plugin plugin;
		private string[] whitelist;

		public ChaosSpawnCommand(Plugin plugin)
		{
			this.plugin = plugin;
			whitelist = plugin.GetConfigList("ca_chaosspawn_whitelist");
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(CS / CHAOSSPAWN) (SET / CLEAR) (TEXT)";
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
							string text = CustomAnnouncements.GetNonValidText(CustomAnnouncements.SpacePeriods(saveText).Split(' '));
							if (text != null)
							{
								return new string[] { "Error: phrase \"" + text + "\" is not in text to speech." };
							}
						}
						else
						{
							return new string[] { GetUsage() };
						}

						File.WriteAllText(CustomAnnouncements.chaosFilePath, String.Empty);
						File.AppendAllText(CustomAnnouncements.chaosFilePath, saveText);

						return new string[] { "Chaos spawn announcement set." };
					}
					else
					{
						return new string[] { GetUsage() };
					}
				}
				else if (args[0].ToLower() == "clear")
				{
					File.WriteAllText(CustomAnnouncements.chaosFilePath, String.Empty);
					return new string[] { "Chaos spawn announcement cleared." };
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
