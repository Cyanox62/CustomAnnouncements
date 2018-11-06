using System;
using System.IO;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System.Threading;

namespace CustomAnnouncements
{
	class RoundEventHandler : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerTeamRespawn
	{
		private Plugin plugin;

		public RoundEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnRoundStart(RoundStartEvent ev)
		{
			CustomAnnouncements.roundStarted = true;
			Thread TimerHandler = new Thread(new ThreadStart(() => new TimerHandler(plugin)));
			TimerHandler.Start();
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			CustomAnnouncements.roundStarted = false;

			string[] message = File.ReadAllLines(CustomAnnouncements.roundendFilePath);
			if (message.Length > 0)
			{
				plugin.pluginManager.Server.Map.AnnounceCustomMessage(message[0]);
			}
		}

		public void OnTeamRespawn(TeamRespawnEvent ev)
		{
			if (ev.SpawnChaos)
			{
				string[] message = File.ReadAllLines(CustomAnnouncements.chaosFilePath);
				if (message.Length > 0)
				{
					plugin.pluginManager.Server.Map.AnnounceCustomMessage(message[0]);
				}
			}
		}
	}
}
