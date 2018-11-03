using System;
using Smod2;
using Smod2.Commands;
using System.IO;

namespace CustomAnnouncements
{
	class LoadAnnouncement : ICommandHandler
	{
		private Plugin plugin;
		//private string[] whitelist;

		public LoadAnnouncement(Plugin plugin)
		{
			this.plugin = plugin;
			//whitelist = plugin.GetConfigList("ca_load_whitelist");
			//for (int i = 0; i < whitelist.Length; i++)
			//	whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(LA / LOADANNOUNCEMENT) (NAME)";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			CustomAnnouncements.ann = UnityEngine.Object.FindObjectOfType<NineTailedFoxAnnouncer>();
			/*if (sender is Player)
			{
				Player player = (Player)sender;
				if (!CustomAnnouncements.IsPlayerWhitelisted(player, whitelist))
				{
					return new string[] { "You are not allowed to run this command." };
				}
			}*/

			if (args.Length > 0)
			{
				string name = args[0];
				string text = null;

				string[] currentText = File.ReadAllLines(CustomAnnouncements.filePath);

				if (currentText.Length > 0)
				{
					foreach (string str in currentText)
					{
						if (str.Split(':')[0].ToLower() == name.ToLower())
						{
							text = str.Substring(str.IndexOf(':'));
						}
					}
				}
				else
				{
					return new string[] { "Error: no presets saved." };
				}

				if (text == null)
				{
					return new string[] { "Error: couldn't load preset \"" + name + "\"" };
				}

				plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
				return new string[] { "Loaded preset \"" + name + "\"." };
			}
			else
			{
				return new string[] { GetUsage() };
			}
		}
	}
}