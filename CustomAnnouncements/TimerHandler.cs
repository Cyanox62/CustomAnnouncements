using System;
using Smod2;
using System.IO;
using System.Threading;

namespace CustomAnnouncements
{
	class TimerHandler
	{
		public TimerHandler(Plugin plugin)
		{
			string[] timers = File.ReadAllLines(CustomAnnouncements.timerFilePath);

			while (CustomAnnouncements.roundStarted)
			{
				foreach (string timer in timers)
				{
					if (timer.Length > 0)
					{
						if (plugin.pluginManager.Server.Round.Duration == Int32.Parse(timer.Split(':')[0]))
						{
							string message = timer.Split(':')[1].Substring(1);
							plugin.pluginManager.Server.Map.AnnounceCustomMessage(CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(message)));
						}
					}
				}
				Thread.Sleep(1000);
			}
		}
	}
}
