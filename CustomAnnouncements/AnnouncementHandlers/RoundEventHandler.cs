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
	class RoundEventHandler : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerTeamRespawn, IEventHandlerPlayerJoin
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
				string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
				plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
			}
		}

		public void OnTeamRespawn(TeamRespawnEvent ev)
		{
			if (ev.SpawnChaos)
			{
				string[] message = File.ReadAllLines(CustomAnnouncements.chaosFilePath);
				if (message.Length > 0)
				{
					string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
					plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
				}
			}
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (File.ReadAllLines(CustomAnnouncements.playerFilePath).Length > 0)
			{
				if (CustomAnnouncements.DoesKeyExistInFile(CustomAnnouncements.playerFilePath, ev.Player.SteamId))
				{
					plugin.pluginManager.Server.Map.AnnounceCustomMessage(CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.GetValueOfKey(CustomAnnouncements.playerFilePath, ev.Player.SteamId.ToString()))));
				}
			}
		}
	}
}
