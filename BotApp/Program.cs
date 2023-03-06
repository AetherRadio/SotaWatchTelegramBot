// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

using System.Text.Json;

namespace AetherRadio.SotaWatchTelegramBot.BotApp;

internal class Program
{
    static void Main()
    {
        JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true
        };

        var poller = new SpotsPoller(TimeSpan.FromSeconds(20));
        poller.NewSpots += (object? sender, List<Spot> newSpots) =>
        {
            Console.WriteLine(JsonSerializer.Serialize(newSpots, jsonOptions));
        };

        poller.Start();

        Console.Read(); // Keep alive

        poller.Stop();
    }
}
