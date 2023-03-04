// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

using System.Text.Json;

namespace AetherRadio.SotaWatchTelegramBot.BotApp;

internal class Program
{
    static void Main(string[] args)
    {
        var spots = Client.QuerySpots(10U).Result;
        if (spots == null)
        {
            Console.WriteLine("No spots found");
            return;
        }

        JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true
        };
        string spotsJson = JsonSerializer.Serialize(spots, jsonOptions);

        Console.WriteLine(spotsJson);
    }
}
