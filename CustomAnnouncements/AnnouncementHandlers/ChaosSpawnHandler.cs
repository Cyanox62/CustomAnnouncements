using System.Threading;
using Smod2;

namespace CustomAnnouncements
{
	class ChaosSpawnHandler
	{
		public ChaosSpawnHandler(Plugin plugin, string[] message)
		{
			Thread.Sleep(50);
			string text = CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(CustomAnnouncements.StringArrayToString(message, 0)));
			plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
		}
	}
}
