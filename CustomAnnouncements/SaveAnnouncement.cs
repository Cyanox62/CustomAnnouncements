using System;
using Smod2;
using Smod2.Commands;
using System.IO;

namespace CustomAnnouncements
{
	class SaveAnnouncement : ICommandHandler
	{
		private Plugin plugin;
		//private string[] whitelist;

		public SaveAnnouncement(Plugin plugin)
		{
			this.plugin = plugin;
			//whitelist = plugin.GetConfigList("ca_save_whitelist");
			//for (int i = 0; i < whitelist.Length; i++)
			//	whitelist[i] = whitelist[i].Replace(" ", "");
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(SA / SAVEANNOUNCEMENT) (NAME) (TEXT)";
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

			if (args.Length > 1)
			{
				string saveText = null;
				string name = args[0];

				for (int i = 1; i < args.Length; i++)
				{
					saveText += args[i];
					if (i != args.Length - 1)
						saveText += " ";
				}

				string[] currentText = File.ReadAllLines(CustomAnnouncements.filePath);

				if (currentText.Length > 0)
				{
					foreach (string str in currentText)
					{
						if (str.Split(':')[0].ToLower() == name.ToLower())
						{
							return new string[] { "Error: Preset name already exists." };
						}
					}
				}

				foreach (string str in saveText.Split(' '))
				{
					if (!CustomAnnouncements.IsVoiceLine(str))
					{
						return new string[] { "Error: phrase \"" + str + "\" is not in text to speech." };
					}
				}

				File.AppendAllText("C:/Users/Infer/Desktop/savedAnnouncements.txt", name + ": " + saveText + Environment.NewLine);
				return new string[] { "Saved preset \"" + name + "\"." };
			}
			else
			{
				return new string[] { GetUsage() };
			}
		}
	}
}