using System;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.Attributes;
using System.Collections.Generic;

namespace CustomAnnouncements
{
    [PluginDetails(
    author = "Cyanox",
    name = "CustomAnnouncements",
    description = "Makes custom CASSIE announcements",
    id = "cyan.custom.announcements",
    version = "0.1",
    SmodMajor = 3,
    SmodMinor = 0,
    SmodRevision = 0
    )]
    public class CustomAnnouncements : Plugin
    {

        public override void OnDisable()
        {
        }

        public override void OnEnable()
        {

        }

        public override void Register()
        {
            //this.AddEventHandlers(new EventHandler(this, this));
            this.AddCommands(new string[] { "mtfannouncement", "mtfa" }, new NTFAnnouncementCommand(this));
            this.AddCommands(new string[] { "scpannouncement", "scpa" }, new SCPEliminationCommand(this));
        }
    }
}
