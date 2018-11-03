using System;
using Smod2;
using Smod2.Commands;
using UnityEngine;

namespace CustomAnnouncements
{
	class CustomTextCommand : ICommandHandler
	{
		private Plugin plugin;
		private NineTailedFoxAnnouncer ann;

		public CustomTextCommand(Plugin plugin)
		{
			this.plugin = plugin;
		}

		private bool IsVoiceLine(string str)
		{
			foreach (NineTailedFoxAnnouncer.VoiceLine vl in ann.voiceLines)
			{
				if (vl.apiName == str.ToUpper())
					return true;
			}
			return false;
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(TA / TEXTANNOUNCEMENT) (TEXT)";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			ann = UnityEngine.Object.FindObjectOfType<NineTailedFoxAnnouncer>();
			if (args.Length > 0)
			{
				foreach (string str in args)
				{
					if (!IsVoiceLine(str))
					{
						return new string[] { "Error: phrase \"" + str + "\" is not in text to speech." };
					}
				}
				plugin.pluginManager.Server.Map.AnnounceCustomMessage(string.Join(" ", args));
				return new string[] { "Announcement has been made." };
			}
			else
			{
				return new string[] { GetUsage() };
			}
		}
	}
}