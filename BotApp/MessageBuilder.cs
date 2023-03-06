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

    public MessageBuilder(ResourceManager stringsResourceManager, CultureInfo localeCultureInfo)
    {
        StringsResourceManager = stringsResourceManager;
        LocaleCultureInfo = localeCultureInfo;
    }

    public IEnumerable<string> MakeMessagesFromSpots(IEnumerable<Spot> spots)
    {
        return spots.Select(spot => MakeMessageFromSpot(spot));
    }

    private string MakeMessageFromSpot(Spot spot)
    {
        string resourceString = StringsResourceManager.GetString("SpotMessage", LocaleCultureInfo)
                                ?? throw new InvalidOperationException("String not found in resources.");

        return string.Format(resourceString,
                             spot.ActivatorCallsign,
                             spot.AssociationCode,
                             spot.SummitCode,
                             spot.Frequency,
                             spot.Mode);

    }
}
