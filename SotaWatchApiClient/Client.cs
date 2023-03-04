// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using System.Net.Http.Json;

namespace AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

public class Client
{
    private static readonly HttpClient SotaHttpClient = new()
    {
        BaseAddress = new Uri("https://api2.sota.org.uk/api/")
    };

    public static async Task<List<Spot>?> QuerySpots(int nSpots)
    {
        var spots = await SotaHttpClient.GetFromJsonAsync<List<Spot>>($"spots/{nSpots}/?filter=all");

        return spots;
    }
}
