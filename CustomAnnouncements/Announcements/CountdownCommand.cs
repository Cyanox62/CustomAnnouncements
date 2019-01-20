using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;

namespace CustomAnnouncements
{
	class CountdownCommand : ICommandHandler
	{
		private Announcement an;
		private Plugin plugin;

		public CountdownCommand(Plugin plugin)
		{
			this.plugin = plugin;
			an = new Announcement(GetUsage(), "ca_countdown_whitelist");
		}

		private string[] GetCountdown(int start, int end)
		{
			int count = 0;
			if (start > end)
			{
				string[] num = new string[((start - end + 1) * 2) - 1];
				for (int i = start; i >= end; i--)
				{
					num[count] = (i > 20) ? CustomAnnouncements.ann.ConvertNumber(i).ToString() : i.ToString();
					if (i < 100)
						if (i != end)
							num[count + 1] = ".";
					count += 2;
				}
				return num;
			}
			else
			{
				return null;
			}
		}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(CD / COUNTDOWN) (START) (END) (TEXT)";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			CustomAnnouncements.ann = UnityEngine.Object.FindObjectOfType<NineTailedFoxAnnouncer>();
			if (!an.CanRunCommand(sender))
				return new string[] { "You are not allowed to run this command." };

			if (args.Length > 1)
			{
				int start, end = 0;
				if (int.TryParse(args[0], out int a))
				{
					start = a;
				}
				else
				{
					return new string[] { "Not a valid number!" };
				}

				if (int.TryParse(args[1], out int b))
				{
					end = b;
				}
				else
				{
					return new string[] { "Not a valid number!" };
				}

				string[] statement = GetCountdown(start, end);

				if (statement != null)
				{
					if (args.Length > 2)
					{
						string saveText = CustomAnnouncements.HandleNumbers(CustomAnnouncements.SpacePeriods(CustomAnnouncements.StringArrayToString(args, 2)));
						return an.PlayCustomAnnouncement(string.Join(" ", statement) + " . . " + CustomAnnouncements.ReplaceVariables(saveText), "Countdown started.");
					}
					else
					{
						return an.PlayCustomAnnouncement(string.Join(" ", statement), "Countdown started.");
					}
				}
				else
				{
					return new string[] { "Error: starting value is less than or equal to ending value." };
				}
			}
			return new string[] { GetUsage() };
		}
	}
}