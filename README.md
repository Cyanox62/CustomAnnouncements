# CustomAnnouncements

A plugin that allows admins to make custom CASSIE announcements.

# Installation

**[Smod2](https://github.com/Grover-c13/Smod2) must be installed for this to work.**

Place the "CustomAnnouncements.dll" file in your sm_plugins folder.

# Features

Any command requiring a text input can use various [round data variables](https://github.com/Cyanox62/CustomAnnouncements/wiki/Round-Data-Variables-List) to use current data in the round. All normal words CASSIE can say can be found [here](https://github.com/Cyanox62/CustomAnnouncements/wiki/CASSIE-Phrases).

Any command using the remove keyword can use `all` or `*` to target all items. For instance, `timer remove all`.

All settings are saved in `%APPDATA%/SCP Secret Laboratory/CustomAnnouncements`.

| Command        | Value Type | Value Type | Value Type | Description |
| :-------------: | :---------: | :---------: | :---------: | :------ |
| CA / CUSTOMANNOUNCEMENTS | | | | Lists all commands. |
| MTFA / MTFANNOUNCEMENT | SCPS LEFT | MTF NUMBER | MTF LETTER | Announces a MTF squad entrance. |
| SCPA / SCPANNOUNCEMENT | SCP NUMBER | | | Announces a SCP death. |
| TA / TEXTANNOUNCEMENT | TEXT | | | Create a custom announcement, view the wiki for all possible words. |
| CD / COUNTDOWN | START NUMBER | END NUMBER | TEXT | Create a countdown with the option of saying something at the end of the countdown. |
| PR / PRESET | SAVE / LOAD / REMOVE / LIST | PRESET NAME | TEXT | Creates/saves/loads/removes/lists the user's custom presets. |
| TI / TIMER | SAVE / REMOVE / LIST | TIMER | TEXT | Creates/saves/removes/lists the user's set timers. Define a timer as an integer value being the number of seconds into a round the announcement will be played. Ex. `ti save 50 hello classd` will announce "hello classd" 50 seconds into the round. |
| CS / CHAOSSPAWN | SET / CLEAR | TEXT | | Sets an announcement to be played when chaos spawn. |
| RS / ROUNDSTART | SET / CLEAR | TEXT | | Sets an announcement to be played when the round starts. |
| RE / ROUNDEND | SET / CLEAR | TEXT | | Sets an announcement to be played when the round ends. |
| PA / PLAYERANNOUNCEMENT | SAVE / REMOVE / LIST | STEAMID64 | TEXT | Sets an announcement to be played when a certain player joins the server. |
| WP / WAITINGFORPLAYERS | SET / CLEAR | TEXT | | Sets an announcement to be played when the server begins waiting for players. Announcement will be played when the first player connects to ensure it isn't played before any players load in. |

| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| ca_mtf_whitelist | List | owner, admin | Determines which ranks are allowed to use the `MTFA / MTFANNOUNCEMENT` command. |
| ca_scp_whitelist | List | owner, admin | Determines which ranks are allowed to use the `SCPA / SCPANNOUNCEMENT` command. |
| ca_text_whitelist | List | owner, admin | Determines which ranks are allowed to use the `TA / TEXTANNOUNCEMENT` command. |
| ca_countdown_whitelist | List | owner, admin | Determines which ranks are allowed to use the `CD / COUNTDOWN` command. |
| ca_preset_whitelist | List | owner, admin | Determines which ranks are allowed to use the `PR / PRESET` command. |
| ca_timer_whitelist | List | owner, admin | Determines which ranks are allowed to use the `TI / TIMER` command. |
| ca_chaosspawn_whitelist | List | owner, admin | Determines which ranks are allowed to use the `CS / CHAOSSPAWN` command. |
| ca_roundstart_whitelist | List | owner, admin | Determines which ranks are allowed to use the `RS / ROUNDSTART` command. |
| ca_roundend_whitelist | List | owner, admin | Determines which ranks are allowed to use the `RE / ROUNDEND` command. |
| ca_player_whitelist | List | owner, admin | Determines which ranks are allowed to use the `PA / PLAYERANNOUNCEMENT` command. |
| ca_waitingforplayers_whitelist | List | owner, admin | Determines which ranks are allowed to use the `WP / WAITINGFORPLAYERS` command. |
