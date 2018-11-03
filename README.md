# CustomAnnouncements

A plugin that allows anyone with access to the Remote Admin console ability to make custom CASSIE announcements.

| Command        | Value Type | Value Type | Value Type | Description |
| :-------------: | :---------: | :---------: | :---------: | :------ |
| MTFA / MTFANNOUNCEMENT | SCPS LEFT | MTF NUMBER | MTF LETTER | Announces a MTF squad entrance. |
| SCPA / SCPANNOUNCEMENT | SCP NUMBER | | | Announces a SCP death. |
| TA / TEXTANNOUNCEMENT | TEXT | | | Create a custom announcement, view the wiki for all possible words. |
| CDA / COUNTDOWNANNOUNCEMENT | START NUMBER | END NUMBER | TEXT | Create a countdown with the option of saying something at the end of the countdown. |

| Config        | Value Type | Default | Description |
| :-------------: | :---------: | :---------: |:------ |
| ca_mtf_whitelist | List | owner, admin | Determintes which ranks are allowed to use the `MTFA / MTFANNOUNCEMENT` command. |
| ca_scp_whitelist | List | owner, admin | Determintes which ranks are allowed to use the `SCPA / SCPANNOUNCEMENT` command. |
| ca_ta_whitelist | List | owner, admin | Determintes which ranks are allowed to use the `TA / TEXTANNOUNCEMENT` command. |
| ca_cda_whitelist | List | owner, admin | Determintes which ranks are allowed to use the `CDA / COUNTDOWNANNOUNCEMENT` command. |
