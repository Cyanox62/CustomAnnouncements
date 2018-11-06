using System;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Threading;

namespace CustomAnnouncements
{
	class RoundEventHandler : IEventHandlerRoundStart, IEventHandlerRoundEnd
	{
		private Plugin plugin;

		public RoundEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnRoundStart(RoundStartEvent ev)
		{
			CustomAnnouncements.roundStarted = true;
			Thread TimerHandler = new Thread(new ThreadStart(() => new TimerAnnouncementsHandler(plugin)));
			TimerHandler.Start();
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			CustomAnnouncements.roundStarted = false;
		}
	}
}
