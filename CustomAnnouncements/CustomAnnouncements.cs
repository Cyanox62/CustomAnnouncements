using System;
using Smod2;
using Smod2.API;
using Smod2.Attributes;
using System.IO;

namespace CustomAnnouncements
{
    [PluginDetails(
    author = "Cyanox",
    name = "CustomAnnouncements",
    description = "Makes custom CASSIE announcements",
    id = "cyan.custom.announcements",
    version = "0.1",
    SmodMajor = 3,
    SmodMinor = 0,
    SmodRevision = 0
    )]
    public class CustomAnnouncements : Plugin
    {
		public static NineTailedFoxAnnouncer ann;
		public static string filePath = "C:/Users/Infer/Desktop/savedAnnouncements.txt";

		public override void OnDisable() {}

		public override void OnEnable()
		{
			if (!File.Exists(filePath))
				using (new StreamWriter(File.Create(filePath))) { }
		}

        public override void Register()
        {
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_countdown_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the countdown command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_text_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the text command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_mtf_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the mtf command."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ca_scp_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the scp command."));
			//this.AddConfig(new Smod2.Config.ConfigSetting("ca_save_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the save command."));
			//this.AddConfig(new Smod2.Config.ConfigSetting("ca_load_whitelist", new string[] { "owner", "admin" }, Smod2.Config.SettingType.LIST, true, "Defines what ranks are allowed to use the load command."));

			this.AddCommands(new string[] { "countdownannouncement", "cda" }, new CountdownCommand(this));
			this.AddCommands(new string[] { "textannouncement", "ta" }, new CustomTextCommand(this));
			this.AddCommands(new string[] { "mtfannouncement", "mtfa" }, new NTFAnnouncementCommand(this));
			this.AddCommands(new string[] { "scpannouncement", "scpa" }, new SCPEliminationCommand(this));
			this.AddCommands(new string[] { "saveannouncement", "sa" }, new SaveAnnouncement(this));
			this.AddCommands(new string[] { "loadannouncement", "la" }, new LoadAnnouncement(this));
			this.AddCommands(new string[] { "presetannouncement", "pa" }, new PresetAnnouncements(this));
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

		public static bool IsVoiceLine(string str)
		{
			foreach (NineTailedFoxAnnouncer.VoiceLine vl in ann.voiceLines)
			{
				if (vl.apiName == str.ToUpper())
					return true;
			}
			return false;
		}
	}
}
