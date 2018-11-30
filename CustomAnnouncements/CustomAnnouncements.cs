using System;
using Smod2;
using Smod2.API;
using Smod2.Attributes;
using System.IO;
using System.Collections.Generic;

// TO DO:

// ¯\_(ツ)_/¯

namespace CustomAnnouncements
{
	[PluginDetails(
	author = "Cyanox",
	name = "CustomAnnouncements",
	description = "Makes custom CASSIE announcements",
	id = "cyan.custom.announcements",
	version = "1.4",
	SmodMajor = 3,
	SmodMinor = 0,
	SmodRevision = 0
	)]
	public class CustomAnnouncements : Plugin
	{
		private static Plugin plugin;
		public static NineTailedFoxAnnouncer ann;
		public static List<String> roundVariables = new List<string>()
		{
			"$scp_alive",
			"$scp_start",
			"$scp_dead",
			"$scp_zombies",
			"$classd_alive",
			"$classd_escape",
			"$classd_start",
			"$scientists_alive",
			"$scientists_escape",
			"$scientists_start",
			"$scp_kills",
			"$grenade_kills",
			"$mtf_alive",
			"$ci_alive",
			"$tutorial_alive",
			"$round_duration",
			"$escape_class" // this one is not listed in the ReplaceVariables method due to it only applying to one event
		};
		public static string ConfigFolerFilePath = FileManager.GetAppFolder() + "CustomAnnouncements";
		public static string PresetsFilePath = FileManager.GetAppFolder() + "CustomAnnouncements" + Path.DirectorySeparatorChar + "Presets.txt";
		public static string TimersFilePath = FileManager.GetAppFolder() + "CustomAnnouncements" + Path.DirectorySeparatorChar + "Timers.txt";
		public static string ChaosSpawnFilePath = FileManager.GetAppFolder() + "CustomAnnouncements" + Path.DirectorySeparatorChar + "ChaosSpawn.txt";
		public static string RoundStartFilePath = FileManager.GetAppFolder() + "CustomAnnouncements" + Path.DirectorySeparatorChar + "RoundStart.txt";
		public static string RoundEndFilePath = FileManager.GetAppFolder() + "CustomAnnouncements" + Path.DirectorySeparatorChar + "RoundEnd.txt";
		public static string PlayerJoinFilePath = FileManager.GetAppFolder() + "CustomAnnouncements" + Path.DirectorySeparatorChar + "PlayerJoin.txt";
		public static string WaitingForPlayersFilePath = FileManager.GetAppFolder() + "CustomAnnouncements" + Path.DirectorySeparatorChar + "WaitingForPlayers.txt";
		public static string PlayerEscapeFilePath = FileManager.GetAppFolder() + "CustomAnnouncements" + Path.DirectorySeparatorChar + "PlayerEscape.txt";
		public static bool roundStarted = false;

		public override void OnDisable() { }

		public override void OnEnable()
		{
			if (!Directory.Exists(ConfigFolerFilePath))
			{
				Directory.CreateDirectory(ConfigFolerFilePath);
			}
			if (!File.Exists(PresetsFilePath))
			{
				using (new StreamWriter(File.Create(PresetsFilePath))) { }
			}
			if (!File.Exists(TimersFilePath))
			{
				using (new StreamWriter(File.Create(TimersFilePath))) { }
			}
			if (!File.Exists(ChaosSpawnFilePath))
			{
				using (new StreamWriter(File.Create(ChaosSpawnFilePath))) { }
			}
			if (!File.Exists(RoundStartFilePath))
			{
				using (new StreamWriter(File.Create(RoundStartFilePath))) { }
			}
			if (!File.Exists(RoundEndFilePath))
			{
				using (new StreamWriter(File.Create(RoundEndFilePath))) { }
			}
			if (!File.Exists(PlayerJoinFilePath))
			{
				using (new StreamWriter(File.Create(PlayerJoinFilePath))) { }
			}
			if (!File.Exists(WaitingForPlayersFilePath))
			{
				using (new StreamWriter(File.Create(WaitingForPlayersFilePath))) { }
			}
			if (!File.Exists(PlayerEscapeFilePath))
			{
				using (new StreamWriter(File.Create(PlayerEscapeFilePath))) { }
			}
		}
		
        public override void Register()
        {
			plugin = this;

			// Event handlers
			this.AddEventHandlers(new RoundEventHandler(this));

			// Config settings
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_all_whitelist", new string[] {}, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use all commands. This will override all other whitelists"));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_countdown_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the countdown command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_text_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the text command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_mtf_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the mtf command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_scp_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the scp command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_preset_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the preset command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_timer_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the timer command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_chaosspawn_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the chaosspawn command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_roundstart_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the roundstart command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_roundend_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the roundend command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_player_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the player command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_waitingforplayers_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the waitingforplayers command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_playerescape_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the playerescape command."));

			// Commands
			this.AddCommands(new string[] { "customannouncements", "ca" }, new CommandsOutput());
			this.AddCommands(new string[] { "countdown", "cd" }, new CountdownCommand(this));
			this.AddCommands(new string[] { "textannouncement", "ta" }, new CustomTextCommand(this));
			this.AddCommands(new string[] { "mtfannouncement", "mtfa" }, new MTFAnnouncementCommand(this));
			this.AddCommands(new string[] { "scpannouncement", "scpa" }, new SCPEliminationCommand(this));
			this.AddCommands(new string[] { "preset", "pr" }, new PresetCommand(this));
			this.AddCommands(new string[] { "timer", "ti" }, new TimerCommand(this));
			this.AddCommands(new string[] { "chaosspawn", "cs" }, new ChaosSpawnCommand(this));
			this.AddCommands(new string[] { "roundstart", "rs" }, new RoundStartCommand(this));
			this.AddCommands(new string[] { "roundend", "re" }, new RoundEndCommand(this));
			this.AddCommands(new string[] { "playerannouncement", "pa" }, new PlayerAnnouncementCommand(this));
			this.AddCommands(new string[] { "waitingforplayers", "wp" }, new WaitingForPlayersCommand(this));
			this.AddCommands(new string[] { "playerescape", "pe" }, new PlayerEscapeCommand(this));
		}

		public static int CountRoles(Role role)
		{
			int count = 0;
			foreach (Player pl in PluginManager.Manager.Server.GetPlayers())
				if (pl.TeamRole.Role == role)
					count++;
			return count;
		}

		public static bool IsPlayerWhitelisted(Player player, string[] whitelist)
		{
			foreach (string rank in whitelist)
			{
				if (player.GetRankName().ToLower() == rank.ToLower())
				{
					return true;
				}
			}
			return false;
		}

		public static string[] SetWhitelist(string whitelistName)
		{
			string[] allWhitelist = plugin.GetConfigList("ca_all_whitelist");
			string[] whitelist;
			if (allWhitelist.Length > 0)
				whitelist = allWhitelist;
			else
				whitelist = plugin.GetConfigList(whitelistName);
			for (int i = 0; i < whitelist.Length; i++)
				whitelist[i] = whitelist[i].Replace(" ", "");
			return whitelist;
		}

		public static bool IsVoiceLine(string str)
		{
			foreach (NineTailedFoxAnnouncer.VoiceLine vl in ann.voiceLines)
			{
				if (vl.apiName.ToUpper() == str.ToUpper())
					return true;
			}
			return false;
		}

		public static string GetNonValidText(string[] text)
		{
			foreach (string str in text)
			{
				string word = str;
				if (word.IndexOf(".") != -1)
				{
					word = word.Replace(" .", "");
				}

				if (!IsVoiceLine(word) && !roundVariables.Contains(word.ToLower()))
				{
					return word;
				}
			}
			return null;
		}

		public static string StringArrayToString(string[] array, int startPos)
		{
			string saveText = null;
			if (array.Length > 0)
			{
				for (int i = startPos; i < array.Length; i++)
				{
					saveText += array[i];
					if (i != array.Length - 1)
						saveText += " ";
				}
			}
			return saveText;
		}

		public static bool DoesKeyExistInFile(string filePath, string key)
		{
			string[] currentText = File.ReadAllLines(filePath);

			if (currentText.Length > 0)
			{
				foreach (string str in currentText)
				{
					if (str.Split(':')[0].ToLower() == key.ToLower())
					{
						return true;
					}
				}
			}
			return false;
		}

		public static string GetValueOfKey(string filePath, string key)
		{
			string[] keys = File.ReadAllLines(filePath);
			if (keys.Length > 0)
			{
				foreach (string str in keys)
				{
					if (str.Split(':')[0].ToLower() == key.ToLower())
					{
						return str.Substring(str.IndexOf(':') + 2);
					}
				}
			}
			return null;
		}

		public static int AddLineToFile(string filePath, string key, string value)
		{
			string[] currentText = File.ReadAllLines(filePath);

			if (DoesKeyExistInFile(filePath, key))
			{
				return -1;
			}

			File.AppendAllText(filePath, key + ": " + value + Environment.NewLine);
			return 1;
		}

		public static int RemoveLineFromFile(string[] lines, string removeString, string filePath)
		{
			List<string> newText = new List<string>();
			int val = lines.Length;
			int count = 0;
			foreach (string str in lines)
			{
				if (str.Split(':')[0] != removeString)
				{
					newText.Add(str);
					count++;
				}
			}

			if (val == count)
			{
				return -1;
			}

			File.WriteAllText(filePath, String.Empty);

			foreach (string str in newText.ToArray())
			{
				File.AppendAllText(filePath, str + Environment.NewLine);
			}
			return 1;
		}

		public static string ReplaceVariables(string input)
		{
			RoundStats stats = PluginManager.Manager.Server.Round.Stats;
			int minutes = (int)(PluginManager.Manager.Server.Round.Duration / 60), duration = PluginManager.Manager.Server.Round.Duration;

			input = input.Replace("$scp_alive", stats.SCPAlive.ToString());
			input = input.Replace("$scp_start", stats.SCPStart.ToString());
			input = input.Replace("$scp_dead", stats.SCPDead.ToString());
			input = input.Replace("$scp_zombies", stats.Zombies.ToString());
			input = input.Replace("$classd_alive", stats.ClassDAlive.ToString());
			input = input.Replace("$classd_escape", stats.ClassDEscaped.ToString());
			input = input.Replace("$classd_start", stats.ClassDStart.ToString());
			input = input.Replace("$scientists_alive", stats.ScientistsAlive.ToString());
			input = input.Replace("$scientists_escape", stats.ScientistsEscaped.ToString());
			input = input.Replace("$scientists_start", stats.ScientistsStart.ToString());
			input = input.Replace("$scp_kills", stats.SCPKills.ToString());
			input = input.Replace("$grenade_kills", stats.GrenadeKills.ToString());
			input = input.Replace("$mtf_alive", stats.NTFAlive.ToString());
			input = input.Replace("$ci_alive", stats.CiAlive.ToString());
			input = input.Replace("$tutorial_alive", CountRoles(Role.TUTORIAL).ToString());
			input = input.Replace("$round_duration", (duration < 60) ? duration + ((duration == 1) ? " second" : " seconds") : minutes + ((minutes == 1) ? " minute" : " minutes") + " and " + (duration - (minutes * 60)) + ((duration - (minutes * 60) == 1) ? " second" : " seconds"));

			string[] words = input.Split(' ');

			for (int i = 0; i < words.Length; i++)
			{ 
				if (Int32.TryParse(words[i], out int a))
				{
					if (a > 20)
					{
						words[i] = ann.ConvertNumber(a).ToString();
					}
				}
			}

			return StringArrayToString(words, 0);
		}

		public static string SpacePeriods(string input)
		{
			string[] words = input.Split(' ');
			for (int i = 0; i < words.Length; i++)
			{
				int index = words[i].IndexOf(".");
				if (index != -1)
				{
					if (index == 0)
						words[i] = words[i].Replace(".", ". ");
					else
						words[i] = words[i].Replace(".", " .");
				}
			}
			return StringArrayToString(words, 0);
		}

		public static string HandleNumbers(string input)
		{
			string[] words = input.Split(' ');
			for (int i = 0; i < words.Length; i++)
			{
				if (Int32.TryParse(words[i], out int a))
				{
					if (a > 20)
					{
						string b = ann.ConvertNumber(a).Replace("  ", " ");
						words[i] = b.Substring(0, b.Length - 1);
					}
				}
			}
			return StringArrayToString(words, 0);
		}

		public static int LevenshteinDistance(string s, string t)
		{
			int n = s.Length;
			int m = t.Length;
			int[,] d = new int[n + 1, m + 1];

			if (n == 0)
			{
				return m;
			}

			if (m == 0)
			{
				return n;
			}

			for (int i = 0; i <= n; d[i, 0] = i++)
			{
			}

			for (int j = 0; j <= m; d[0, j] = j++)
			{
			}

			for (int i = 1; i <= n; i++)
			{
				for (int j = 1; j <= m; j++)
				{
					int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

					d[i, j] = Math.Min(
						Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
						d[i - 1, j - 1] + cost);
				}
			}
			return d[n, m];
		}

		public static Player GetPlayer(string args, out Player playerOut)
		{
			int maxNameLength = 31, LastnameDifference = 31;
			Player plyer = null;
			string str1 = args.ToLower();
			foreach (Player pl in PluginManager.Manager.Server.GetPlayers(str1))
			{
				if (!pl.Name.ToLower().Contains(args.ToLower())) { goto NoPlayer; }
				if (str1.Length < maxNameLength)
				{
					int x = maxNameLength - str1.Length;
					int y = maxNameLength - pl.Name.Length;
					string str2 = pl.Name;
					for (int i = 0; i < x; i++)
					{
						str1 += "z";
					}
					for (int i = 0; i < y; i++)
					{
						str2 += "z";
					}
					int nameDifference = CustomAnnouncements.LevenshteinDistance(str1, str2);
					if (nameDifference < LastnameDifference)
					{
						LastnameDifference = nameDifference;
						plyer = pl;
					}
				}
				NoPlayer:;
			}
			playerOut = plyer;
			return playerOut;
		}

		public static float CalculateDuration(string tts)
		{
			float num = 0f;
			string[] array = tts.Split(new char[]{' '});
			string[] array2 = array;
			foreach (string text in array2)
			{
				NineTailedFoxAnnouncer.VoiceLine[] array4 = ann.voiceLines;
				foreach (NineTailedFoxAnnouncer.VoiceLine voiceLine in array4)
				{
					if (text.ToUpper() == voiceLine.apiName.ToUpper())
					{
						num += voiceLine.length;
					}
				}
			}
			return num;
		}
	}
}
