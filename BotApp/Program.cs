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
        JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true
        };

        var pooler = new SpotsPoller(TimeSpan.FromSeconds(10));
        pooler.NewSpots += (object? sender, List<Spot> newSpots) =>
        {
            Console.WriteLine(JsonSerializer.Serialize(newSpots, jsonOptions));
        };

        pooler.Start();

        Console.Read(); // Keep alive

        pooler.Stop();
    }
}
