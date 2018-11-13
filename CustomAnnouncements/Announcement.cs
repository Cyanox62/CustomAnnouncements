using System;
using System.IO;
using Smod2;
using Smod2.API;
using Smod2.Commands;

namespace CustomAnnouncements
{
	class Announcement
	{
		private string GetUsage;
		private string FilePath;
		private string[] WhitelistConfig;

		public Announcement(string GetUsage, string WhitelistConfig, string FilePath = "")
		{
			this.GetUsage = GetUsage;
			this.FilePath = FilePath;
			this.WhitelistConfig = CustomAnnouncements.SetWhitelist(WhitelistConfig);
		}

		public bool CanRunCommand(ICommandSender sender)
		{
			if (sender is Player)
			{
				Player player = (Player)sender;
				if (!CustomAnnouncements.IsPlayerWhitelisted(player, WhitelistConfig))
				{
					return false;
				}
			}
			return true;
		}

		public string[] PlayCustomAnnouncement(string text, string successResponse)
		{
			if (text.Length > 0)
			{
				string testText = CustomAnnouncements.GetNonValidText(CustomAnnouncements.HandleNumbers(CustomAnnouncements.SpacePeriods(text)).Split(' '));
				if (testText != null)
				{
					return new string[] { "Error: phrase \"" + testText + "\" is not in text to speech." };
				}
				PluginManager.Manager.Server.Map.AnnounceCustomMessage(CustomAnnouncements.ReplaceVariables(CustomAnnouncements.HandleNumbers(CustomAnnouncements.SpacePeriods(text))));
				return new string[] { successResponse };
			}
			return new string[] { GetUsage };
		}

		public string[] PlayMTFAnnouncement(int scpsLeft, int mtfNumber, char mtfLetter, string successResponse)
		{
			PluginManager.Manager.Server.Map.AnnounceNtfEntrance(scpsLeft, mtfNumber, mtfLetter);
			return new string[] { successResponse };
		}

		public string[] PlaySCPAnnouncement(string scpNumber, string successResponse)
		{
			PluginManager.Manager.Server.Map.AnnounceScpKill(scpNumber);
			return new string[] { successResponse };
		}

		public string[] SetAnnouncement(string text, string successMessage)
		{
			if (FilePath == "")
				return new string[] { "Error: missing filepath." };

			if (text.Length > 0)
			{
				string testText = CustomAnnouncements.GetNonValidText(CustomAnnouncements.HandleNumbers(CustomAnnouncements.SpacePeriods(text)).Split(' '));
				if (testText != null)
				{
					return new string[] { "Error: phrase \"" + testText + "\" is not in text to speech." };
				}
			}
			else
			{
				return new string[] { GetUsage };
			}
			File.WriteAllText(FilePath, String.Empty);
			File.AppendAllText(FilePath, text);

			return new string[] { successMessage };
		}

		public string[] ViewAnnouncement(string notSetResponse)
		{
			if (FilePath == "")
				return new string[] { "Error: missing filepath." };

			string[] current = File.ReadAllLines(FilePath);
			if (current.Length > 0)
			{
				return current;
			}
			return new string[] { notSetResponse };
		}

		public string[] ClearAnnouncement(string ClearMessage)
		{
			File.WriteAllText(FilePath, String.Empty);
			return new string[] { ClearMessage };
		}

		public string[] SetVariable(string name, string input, string existsResponse, string successResponse)
		{
			if (FilePath == "")
				return new string[] { "Error: missing filepath." };

			string[] currentText = File.ReadAllLines(FilePath);

			if (input.Length > 0)
			{
				string text = CustomAnnouncements.GetNonValidText(CustomAnnouncements.HandleNumbers(CustomAnnouncements.SpacePeriods(input)).Split(' '));
				if (text != null)
				{
					return new string[] { "Error: phrase \"" + text + "\" is not in text to speech." };
				}
			}
			else
			{
				return new string[] { GetUsage };
			}

			int output = CustomAnnouncements.AddLineToFile(FilePath, name.ToString(), input);

			if (output == -1)
			{
				return new string[] { existsResponse };
			}

			return new string[] { successResponse };
		}

		public string[] LoadVariable(string name, string noneSavedResponse, string cantFindResponse, string successResponse)
		{
			string text = null;
			string[] currentText = File.ReadAllLines(FilePath);

			if (currentText.Length > 0)
			{
				foreach (string str in currentText)
				{
					if (str.Split(':')[0].ToLower() == name.ToLower())
					{
						text = str.Substring(str.IndexOf(':') + 2);
					}
				}
			}
			else
			{
				return new string[] { noneSavedResponse };
			}

			if (text == null)
			{
				return new string[] { cantFindResponse };
			}
			PlayCustomAnnouncement(text, successResponse);
			return new string[] { successResponse };
		}

		public string[] RemoveVariable(string name, string noneSavedResponse, string cantFindResponse, string removedAllResponse, string successResponse)
		{
			if (FilePath == "")
				return new string[] { "Error: missing filepath." };

			if (name.ToLower() == "all" || name == "*")
			{
				File.WriteAllText(FilePath, String.Empty);
				return new string[] { removedAllResponse };
			}

			string[] currentText = File.ReadAllLines(FilePath);

			if (currentText.Length > 0)
			{
				if (CustomAnnouncements.RemoveLineFromFile(currentText, name, FilePath) == -1)
				{
					return new string[] { cantFindResponse };
				}
				else
				{
					return new string[] { successResponse };
				}
			}
			else
			{
				return new string[] { noneSavedResponse };
			}
		}

		public string[] ListVariables(string noneSavedResponse)
		{
			if (FilePath == "")
				return new string[] { "Error: missing filepath." };

			string[] current = File.ReadAllLines(FilePath);
			if (current.Length > 0)
			{
				return current;
			}
			return new string[] { noneSavedResponse };
		}
	}
}
