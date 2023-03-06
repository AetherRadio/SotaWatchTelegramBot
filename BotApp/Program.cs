// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

using System.Globalization;
using System.Resources;

namespace AetherRadio.SotaWatchTelegramBot.BotApp;

internal class Program
{
    static void Main()
    {
        // Get the user's preferred language and make a CultureInfo
        // TODO: allow this to be configurable
        string lang = CultureInfo.CurrentCulture.Name;
        CultureInfo cInfo = new(lang);

        // Load the appropriate resource file
        ResourceManager rManager = new("AetherRadio.SotaWatchTelegramBot.BotApp.Strings", typeof(Program).Assembly);

        // Actual "business logic"

        MessageBuilder mBuilder = new(rManager, cInfo);

        var poller = new SpotsPoller(TimeSpan.FromSeconds(20));
        poller.NewSpots += (object? sender, IEnumerable<Spot> newSpots) =>
        {
            foreach (string message in mBuilder.MakeMessagesFromSpots(newSpots))
            {
                Console.WriteLine(message);
            };
        };

        poller.Start();

        Console.Read(); // Keep alive

        poller.Stop();
    }
}
