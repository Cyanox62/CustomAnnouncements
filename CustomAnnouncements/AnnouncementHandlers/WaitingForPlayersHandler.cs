using Smod2;
using System.Threading;

namespace CustomAnnouncements
{
	class WaitingForPlayersHandler
	{
		public WaitingForPlayersHandler(Plugin plugin, string text)
		{
			bool playedAnnouncement = false;
			while (!playedAnnouncement)
			{
				if (plugin.Server.GetPlayers().Count > 0)
				{
					plugin.pluginManager.Server.Map.AnnounceCustomMessage(text);
					playedAnnouncement = true;
				}
				Thread.Sleep(1000);
			}
		}
	}
}
