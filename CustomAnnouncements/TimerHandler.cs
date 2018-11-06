﻿using System;
using Smod2;
using Smod2.Commands;
using Smod2.API;
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
					if (plugin.pluginManager.Server.Round.Duration == Int32.Parse(timer.Split(':')[0]))
					{
						string message = timer.Split(':')[1].Substring(1);
						plugin.pluginManager.Server.Map.AnnounceCustomMessage(message);
					}
				}
				Thread.Sleep(1000);
			}
		}
	}
}