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
				"(CD / COUNTDOWN) (START) (END) (TEXT)",
				"(TA / TEXTANNOUNCEMENT) (TEXT)",
				"(MTFA / MTFANNOUNCEMENT) (SCPS LEFT) (MTF NUMBER) (MTF LETTER)",
				"(SCPA / SCPANNOUNCEMENT) (SCP NUMBER)",
				"(PR / PRESET) (SAVE / LOAD / REMOVE / LIST) (NAME) (TEXT)",
				"(TI / TIMER) (SET / REMOVE / LIST) (TIME) (TEXT)",
				"(CS / CHAOSSPAWN) (SET / CLEAR) (TEXT)",
				"(RE / ROUNDEND) (SET / CLEAR) (TEXT)",
				"(PA / PLAYERANNOUNCEMENT) (SAVE / REMOVE / LIST) (STEAMID) (TEXT)"
			};
		}
	}
}
