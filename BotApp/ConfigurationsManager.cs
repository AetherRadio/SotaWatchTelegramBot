// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using System.Configuration;

namespace AetherRadio.SotaWatchTelegramBot.BotApp;

internal class ConfigurationsManager
{
    public string TelegramToken { get; init; }
    public IEnumerable<string> TelegramChats { get; init; }
    public IEnumerable<string> AssociationWhitelist { get; init; }
    public string Locale { get; init; }

    public ConfigurationsManager()
    {
        TelegramToken = ConfigurationManager.AppSettings["TelegramToken"]
                        ?? throw new InvalidOperationException("TelegramToken not found in settings.");
        TelegramChats = ConfigurationManager.AppSettings["TelegramChats"]?.Split(";")
                        ?? throw new InvalidOperationException("TelegramToken not found in settings.");
        AssociationWhitelist = ConfigurationManager.AppSettings["AssociationWhitelist"]?.Split(";")
                               ?? throw new InvalidOperationException("AssociationWhitelist not found in settings.");
        Locale = ConfigurationManager.AppSettings["Locale"] ?? "en-US";
    }

}
