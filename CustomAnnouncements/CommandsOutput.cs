using Smod2.Commands;

namespace CustomAnnouncements
{
	class CommandsOutput : ICommandHandler
	{
		public CommandsOutput() {}

		public string GetCommandDescription()
		{
			return "Creates custom CASSIE announcements.";
		}

		public string GetUsage()
		{
			return "(CA / CUSTOMANNOUNCEMENTS)";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			return new string[] { "CUSTOM ANNOUNCEMENTS COMMANDS:",
				"(CA / CUSTOMANNOUNCEMENTS)",
				"(CDA / COUNTDOWNANNOUNCEMENT) (START) (END) (TEXT)",
				"(TA / TEXTANNOUNCEMENT) (TEXT)",
				"(MTFA / MTFANNOUNCEMENT) (SCPS LEFT) (MTF NUMBER) (MTF LETTER)",
				"(SCPA / SCPANNOUNCEMENT) (SCP NUMBER)",
				"(PA / PRESETANNOUNCEMENT) (SAVE / LOAD / REMOVE / LIST) (NAME)",
				"(TIA / TIMEDANNOUNCEMENT) (SAVE / REMOVE / LIST) (TIME) (TEXT)"
			};
		}
	}
}
