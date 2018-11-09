﻿using System;
using System.IO;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System.Threading;

namespace CustomAnnouncements
{
	class RoundEventHandler : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerTeamRespawn, IEventHandlerPlayerJoin, IEventHandlerWaitingForPlayers
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

			string[] message = File.ReadAllLines(CustomAnnouncements.RoundStartFilePath);
			if (message.Length > 0)
			{
				string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.HandlePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
				plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
				plugin.Info("Running round start announcement...");
			}
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			CustomAnnouncements.roundStarted = false;

			string[] message = File.ReadAllLines(CustomAnnouncements.RoundEndFilePath);
			if (message.Length > 0)
			{
				string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.HandlePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
				plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
				plugin.Info("Running round end anmnouncement...");
			}
		}

		public void OnTeamRespawn(TeamRespawnEvent ev)
		{
			if (ev.SpawnChaos)
			{
				string[] message = File.ReadAllLines(CustomAnnouncements.ChaosSpawnFilePath);
				if (message.Length > 0)
				{
					string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.HandlePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
					plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
					plugin.Info("Running Chaos Insurgency spawn announcement...");
				}
			}
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (File.ReadAllLines(CustomAnnouncements.PlayerJoinFilePath).Length > 0)
			{
				if (CustomAnnouncements.DoesKeyExistInFile(CustomAnnouncements.PlayerJoinFilePath, ev.Player.SteamId))
				{
					plugin.pluginManager.Server.Map.AnnounceCustomMessage(CustomAnnouncements.ReplaceVariables(CustomAnnouncements.HandlePeriods(CustomAnnouncements.GetValueOfKey(CustomAnnouncements.PlayerJoinFilePath, ev.Player.SteamId.ToString()))));
					plugin.Info("Running player join announcement for player: " + ev.Player.Name);
				}
			}
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			string[] message = File.ReadAllLines(CustomAnnouncements.WaitingForPlayersFilePath);
			if (message.Length > 0)
			{
				Thread WaitingForPlayersHandler = new Thread(new ThreadStart(() => new WaitingForPlayersHandler(plugin, CustomAnnouncements.ReplaceVariables(CustomAnnouncements.HandlePeriods(CustomAnnouncements.StringArrayToString(message, 0))))));
				WaitingForPlayersHandler.Start();
			}
		}
	}
}
