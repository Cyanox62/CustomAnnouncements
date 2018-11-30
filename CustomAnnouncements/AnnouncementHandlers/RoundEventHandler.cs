using System.IO;
using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System.Threading;

namespace CustomAnnouncements
{
	class RoundEventHandler : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerCheckEscape, IEventHandlerTeamRespawn, IEventHandlerPlayerJoin, IEventHandlerWaitingForPlayers
	{
		private Plugin plugin;
		public static bool isPlaying = false;

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
				string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
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
				string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
				plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
				plugin.Info("Running round end announcement...");
			}
		}

		public void OnTeamRespawn(TeamRespawnEvent ev)
		{
			if (ev.SpawnChaos)
			{
				string[] message = File.ReadAllLines(CustomAnnouncements.ChaosSpawnFilePath);
				if (message.Length > 0)
				{
					string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
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
					plugin.pluginManager.Server.Map.AnnounceCustomMessage(CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.GetValueOfKey(CustomAnnouncements.PlayerJoinFilePath, ev.Player.SteamId.ToString()))));
					plugin.Info("Running player join announcement for player: " + ev.Player.Name);
				}
			}
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			string[] message = File.ReadAllLines(CustomAnnouncements.WaitingForPlayersFilePath);
			if (message.Length > 0)
			{
				Thread WaitingForPlayersHandler = new Thread(new ThreadStart(() => new WaitingForPlayersHandler(plugin, CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.StringArrayToString(message, 0))))));
				WaitingForPlayersHandler.Start();
			}
		}

		public void OnCheckEscape(PlayerCheckEscapeEvent ev)
		{
			string[] message = File.ReadAllLines(CustomAnnouncements.PlayerEscapeFilePath);
			if (message.Length > 0)
			{
				if (!isPlaying)
				{
					string str = CustomAnnouncements.StringArrayToString(message, 0).Replace("$escape_class", RoleConversions.RoleConversionDict[ev.Player.TeamRole.Role]);
					plugin.pluginManager.Server.Map.AnnounceCustomMessage(CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(str)));
					plugin.Info("Running player escape announcement...");
					Thread WaitingForPlayersHandler = new Thread(new ThreadStart(() => new PlayerEscapeHandler(str)));
					WaitingForPlayersHandler.Start();
				}
			}
		}
	}
}
