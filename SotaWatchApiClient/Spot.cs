// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using System.Text.Json.Serialization;

namespace AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

/**
 * <summary>
 * Map the JSON objects returned by the SOTA Watch API to C# objects.
 * Source: https://api2.sota.org.uk/docs/index.html
 * </summary>
*/
public record class Spot
{
    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("userID")]
    public required int UserId { get; init; }

    [JsonPropertyName("timeStamp")]
    public required DateTimeOffset TimeStamp { get; init; }

    [JsonPropertyName("comments")]
    public required string Comments { get; init; }

    [JsonPropertyName("callsign")]
    public required string Callsign { get; init; }

    [JsonPropertyName("associationCode")]
    public required string? AssociationCode { get; init; }

    [JsonPropertyName("summitCode")]
    public required string SummitCode { get; init; }

    [JsonPropertyName("activatorCallsign")]
    public required string ActivatorCallsign { get; init; }

    [JsonPropertyName("activatorName")]
    public required string ActivatorName { get; init; }

    [JsonPropertyName("frequency")]
    public required float Frequency { get; init; }

    [JsonPropertyName("mode")]
    public required string Mode { get; init; }

    [JsonPropertyName("summitDetails")]
    public required string SummitDetails { get; init; }

    [JsonPropertyName("highlightColor")]
    public required string HighlightColor { get; init; }
}
