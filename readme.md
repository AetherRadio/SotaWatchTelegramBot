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

The easiest way to run the bot is to use `docker compose`.
If you're knowledgeable with `dotnet`, feel free to build and run the
application in a standalone way.

### How to run with `docker compose`

First, you need to create your own bot by talking to the `@BotFather` bot on
Telegram. See [this link](https://sendpulse.com/knowledge-base/chatbot/telegram/create-telegram-chatbot)
for more information.

Don't forget to add the bot to the channels you want it to be in.

Once you have your bot, `git clone` or download and extract this repository on
your machine.

Then, copy the file `BotApp.dll.config.sample`, renaming it to
`BotApp.dll.config`. In Linux you can do this by running:

```bash
cp BotApp.dll.config.sample BotApp.dll.config
```

Then, open the file `BotApp.dll.config` and fill in the values. Put the token
for your bot in `TelegramToken`. `TelegramChats` is for a semicolon-separated
list of chat IDs that the bot will send messages to. I couldn't find a good way
to get the chat ID, but I noticed that when visiting the `/getUpdates` API
endpoint of the bot (e.g. `https://api.telegram.org/bot<token>/getUpdates`),
I could see the chat ID in the JSON response. Example:

```json
      "my_chat_member": {
        "chat": {
          "id": -1000012345678,
          "title": "Some channel name",
          "type": "supergroup"
        },
```

Where `id` is the number you need to put in `TelegramChats`, minus sign
included.

Once you have the configuration file ready, you can run the bot by running:

```bash
docker compose up -d --build
```

Older versions of `docker` might not have the `compose` subcommand.
Replace `docker compose` with `docker-compose` in that case.

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
