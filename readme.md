<!--
  This file is part of Aether Radio's SOTA Watch Telegram Bot.
  SPDX-License-Identifier: CC0-1.0
  SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>
-->
# Aether Radio's SOTA Watch Telegram Bot

**This project is currently under _slow_ development.**

The goal of this project is to make a Telegram bot that will send out
notifications when a SOTA summit is activated.

The goal is for the bot to be able be in SOTA-related channels and message
summit spots and alerts according to defined filters.

## Running

The easiest way to run the bot is to use a session manager, like `tmux` or
`screen`. This is because the application keeps alive by listening to the
terminal.

You can start the application by running:

```bash
tmux new-session -s sota-bot-session
```

And inside the session, run:

```bash
./BotApp
```

And then exit the session by pressing `Ctrl + B` and then `D`.

## Create container (WIP)

In the `BotApp` directory, run:

```bash
dotnet publish --os linux --arch x64 /t:PublishContainer -c Release
```

## Licensing information

The project is licensed under the Apache License, Version 2.0. See the
[LICENSES/Apache-2.0.txt](./LICENSES/Apache-2.0.txt) file for details.

Some files are licensed under the Creative Commons CC0 1.0 Universal (CC0 1.0)
Public Domain Dedication license. See the
[LICENSES/CC0-1.0.txt](./LICENSES/CC0-1.0.txt) file for details.
Files under this license are mostly configuration files that do not constitute
"source code" per se.

Files without an SPDX license identifier are licensed under the aforementioned
Apache License, Version 2.0 license.
