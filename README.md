# CustomAnnouncements

A plugin that allows admins to make custom CASSIE announcements.

**[Smod2](https://github.com/Grover-c13/Smod2) must be installed for this to work.**

Any command requiring a text input can use various [server name variables](https://github.com/Cyanox62/CustomAnnouncements/wiki/Server-Info-Variables) to use current data in the round.

Any command using the remove keyword can use `all` or `*` to target all items. For instance, `timer remove all`.

All settings are saved in %APPDATA%/SCP Secret Laboratory/CustomAnnouncements.

| Command        | Value Type | Value Type | Value Type | Description |
| :-------------: | :---------: | :---------: | :---------: | :------ |
| CA / CUSTOMANNOUNCEMENTS | | | | Lists all commands. |
| MTFA / MTFANNOUNCEMENT | SCPS LEFT | MTF NUMBER | MTF LETTER | Announces a MTF squad entrance. |
| SCPA / SCPANNOUNCEMENT | SCP NUMBER | | | Announces a SCP death. |
| TA / TEXTANNOUNCEMENT | TEXT | | | Create a custom announcement, view the wiki for all possible words. |
| CD / COUNTDOWN | START NUMBER | END NUMBER | TEXT | Create a countdown with the option of saying something at the end of the countdown. |
| PR / PRESET | SAVE / LOAD / REMOVE / LIST | PRESET NAME | TEXT | Creates/saves/loads/removes/lists the user's custom presets. |
| TI / TIMER | SAVE / REMOVE / LIST | TIMER | TEXT | Creates/saves/removes/lists the user's set timers. Define a timer as an integer value being the number of seconds into a round the announcement will be played. Ex. `tia save 50 hello classd` will announce "hello classd" 50 seconds into the round. |
| CS / CHAOSSPAWN | SET / CLEAR | TEXT | | Sets an announcement to be played when chaos spawn. |
| RE / ROUNDEND | SET / CLEAR | TEXT | | Sets an announcement to be played when the round ends. |
| PA / PLAYERANNOUNCEMENT | SET / REMOVE / LIST | STEAMID64 | TEXT | Sets an announcement to be played when a certain player joins the server. |

| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| ca_mtf_whitelist | List | owner, admin | Determines which ranks are allowed to use the `MTFA / MTFANNOUNCEMENT` command. |
| ca_scp_whitelist | List | owner, admin | Determines which ranks are allowed to use the `SCPA / SCPANNOUNCEMENT` command. |
| ca_text_whitelist | List | owner, admin | Determines which ranks are allowed to use the `TA / TEXTANNOUNCEMENT` command. |
| ca_countdown_whitelist | List | owner, admin | Determines which ranks are allowed to use the `CD / COUNTDOWN` command. |
| ca_preset_whitelist | List | owner, admin | Determines which ranks are allowed to use the `PR / PRESET` command. |
| ca_timer_whitelist | List | owner, admin | Determines which ranks are allowed to use the `TI / TIMER` command. |
| ca_chaosspawn_whitelist | List | owner, admin | Determines which ranks are allowed to use the `CS / CHAOSSPAWN` command. |
| ca_roundend_whitelist | List | owner, admin | Determines which ranks are allowed to use the `RE / ROUNDEND` command. |
| ca_player_whitelist | List | owner, admin | Determines which ranks are allowed to use the `PA / PLAYERANNOUNCEMENT` command. |
