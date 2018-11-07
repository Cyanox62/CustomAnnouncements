using System;
using System.IO;

namespace CustomAnnouncements
{
	class Announcement
	{
		private string Usage;
		private string SetMessage;
		private string ClearMessage;
		private string FilePath;

		public Announcement(string Usage, string SetMessage, string ClearMessage, string FilePath)
		{
			this.Usage = Usage;
			this.SetMessage = SetMessage;
			this.ClearMessage = ClearMessage;
			this.FilePath = FilePath;
		}

		public string[] SetAnnouncement(string text)
		{
			if (text.Length > 0)
			{
				string testText = CustomAnnouncements.GetNonValidText(CustomAnnouncements.SpacePeriods(text).Split(' '));
				if (testText != null)
				{
					return new string[] { "Error: phrase \"" + testText + "\" is not in text to speech." };
				}
			}
			else
			{
				return new string[] { Usage };
			}

			File.WriteAllText(FilePath, String.Empty);
			File.AppendAllText(FilePath, text);

			return new string[] { SetMessage };
		}

		public string[] ClearAnnouncement()
		{
			File.WriteAllText(FilePath, String.Empty);
			return new string[] { ClearMessage };
		}
	}
}
