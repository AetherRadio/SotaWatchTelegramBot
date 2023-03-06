// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using System.Diagnostics;
using System.Net.Http.Json;

namespace AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

internal class ApiClient
{
    private static readonly HttpClient sotaHttpClient = new()
    {
        BaseAddress = new Uri("https://api2.sota.org.uk/api/")
    };

    /**
     * <summary>
     * Queries the SOTA Watch API for the <paramref name="nSpots"/> most recent spots.
     * </summary>
     * 
     * <param name="nSpots">
     * The number of latest spots to query. Must be <= 200.
     * While the SOTA Watch API has the option to use negative values corresponding to "hours before",
     * I choose not to implement that here.
     * </param>
     */
    internal static async Task<List<Spot>?> QuerySpots(uint nSpots)
    {
        Debug.Assert(nSpots <= 200, "nSpots must be <= 200");

        var spots = await sotaHttpClient.GetFromJsonAsync<List<Spot>>($"spots/{nSpots}/?filter=all");

        return spots;
    }
}
