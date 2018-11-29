using System.Threading;

namespace CustomAnnouncements
{
	class PlayerEscapeHandler
	{
		public PlayerEscapeHandler(string input)
		{
			RoundEventHandler.isPlaying = true;
			float time = CustomAnnouncements.CalculateDuration(input) + 6.3f;
			Thread.Sleep((int)(time * 1000));
			RoundEventHandler.isPlaying = false;
		}
	}
}
