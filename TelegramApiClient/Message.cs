// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

using System.Text.Json.Serialization;

namespace AetherRadio.SotaWatchTelegramBot.TelegramApiClient;

/**
 * <summary>
 * Map the Message JSON object of the Telegram API to C# objects.
 * Since I only want to send, this is the bare minimum required, and I'm not adding the rest for now.
 * Source: https://core.telegram.org/bots/api#sendmessage
 * </summary>
 */
internal record class Message
{
    [JsonPropertyName("chat_id")]
    public required string ChatId { get; init; }

    [JsonPropertyName("text")]
    public required string Text { get; init; }
}
