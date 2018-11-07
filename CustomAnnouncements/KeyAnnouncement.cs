using System;
using System.IO;
using Smod2;
using System.Collections.Generic;

namespace CustomAnnouncements
{
	class KeyAnnouncement
	{
		private string GetUsage;
		private string FilePath;

		public KeyAnnouncement(string GetUsage, string FilePath)
		{
			this.GetUsage = GetUsage;
			this.FilePath = FilePath;
		}

		public string[] SetVariable(string name, string input, string existsResponse, string successResponse)
		{
			string[] currentText = File.ReadAllLines(FilePath);

			if (input.Length > 0)
			{
				string text = CustomAnnouncements.GetNonValidText(CustomAnnouncements.SpacePeriods(input).Split(' '));
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
						text = str.Substring(str.IndexOf(':'));
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
			PluginManager.Manager.Server.Map.AnnounceCustomMessage(CustomAnnouncements.ReplaceVariables(CustomAnnouncements.SpacePeriods(text)));
			return new string[] { successResponse };
		}

		public string[] RemoveVariable(string name, string noneSavedResponse, string cantFindResponse, string removedAllResponse, string successResponse)
		{
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
			string[] current = File.ReadAllLines(FilePath);
			if (current.Length > 0)
			{
				return current;
			}
			return new string[] { noneSavedResponse };
		}
	}
}
