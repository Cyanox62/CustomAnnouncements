using System.Collections.Generic;
using Smod2.API;

namespace CustomAnnouncements
{
	class RoleConversions
	{
		public static Dictionary<Role, string> RoleConversionDict = new Dictionary<Role, string>()
		{
			{ Role.CHAOS_INSURGENCY, "chaosinsurgency" },
			{ Role.CLASSD, "classd" },
			{ Role.FACILITY_GUARD, "facility guard" },
			{ Role.NTF_CADET, "ninetailedfox" },
			{ Role.NTF_COMMANDER, "ninetailedfox" },
			{ Role.NTF_LIEUTENANT, "ninetailedfox" },
			{ Role.NTF_SCIENTIST, "ninetailedfox" },
			{ Role.SCIENTIST, "scientist" },
		};
	}
}
