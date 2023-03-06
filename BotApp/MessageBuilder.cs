// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

using System.Globalization;
using System.Resources;

namespace AetherRadio.SotaWatchTelegramBot.BotApp;

internal class MessageBuilder
{

    private ResourceManager StringsResourceManager { get; init; }
    private CultureInfo LocaleCultureInfo { get; init; }

    private static readonly string defaultSpotMessage = "[DEFAULT] {0} is activating in {1}/{2}, on frequency {3} MHz, mode {4}.";

    public MessageBuilder(ResourceManager stringsResourceManager, CultureInfo localeCultureInfo)
    {
        StringsResourceManager = stringsResourceManager;
        LocaleCultureInfo = localeCultureInfo;
    }

    public List<string> MakeMessagesFromSpots(List<Spot> spots)
    {
        return spots.Select(spot => MakeMessageFromSpot(spot)).ToList();
    }

    private string MakeMessageFromSpot(Spot spot)
    {
        string resourceString = StringsResourceManager.GetString("SpotMessage", LocaleCultureInfo)
                                ?? defaultSpotMessage;
        return string.Format(resourceString,
                             spot.ActivatorCallsign,
                             spot.AssociationCode,
                             spot.SummitCode,
                             spot.Frequency,
                             spot.Mode);

    }
}
