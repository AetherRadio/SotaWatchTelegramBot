// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;
using AetherRadio.SotaWatchTelegramBot.TelegramApiClient;

using System.Globalization;
using System.Resources;

namespace AetherRadio.SotaWatchTelegramBot.BotApp;

internal class Program
{
    static void Main()
    {
        // Load the configuration
        ConfigurationsManager confManager = new();


        // Load the appropriate resource file
        ResourceManager rManager = new($"{typeof(Program).Namespace}.Strings", typeof(Program).Assembly);

        // Actual "business logic"

        CultureInfo cInfo = new(confManager.Locale);

        MessageBuilder mBuilder = new(rManager, cInfo, confManager.AssociationWhitelist);

        MessageSender mSender = new(confManager.TelegramToken, confManager.TelegramChats);

        var poller = new SpotsPoller(TimeSpan.FromSeconds(30));
        poller.NewSpots += (object? _, IEnumerable<Spot> newSpots) =>
        {
            foreach (string message in mBuilder.MakeMessagesFromSpots(newSpots))
            {
                Console.WriteLine(message);
                mSender.Send(message);
            };
        };

        // Keep alive system
        Console.WriteLine("Press Ctrl-C to exit.");
        ManualResetEvent waitHandle = new(false);

        // Set up signal handlers
        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            Console.WriteLine("Exiting...");
            waitHandle.Set();
            // Prevent the main thread from terminating the process
            eventArgs.Cancel = true;
        };

        poller.Start();

        // Block the main thread until a signal is received
        waitHandle.WaitOne();

        poller.Stop();
    }
}
