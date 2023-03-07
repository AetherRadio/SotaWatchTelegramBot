// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

namespace AetherRadio.SotaWatchTelegramBot.TelegramApiClient;

public class MessageSender
{
    private readonly HttpClient httpClient;
    private IEnumerable<string> ChatIdsToSendTo { get; set; }

    public MessageSender(string botToken, IEnumerable<string> chatIdsToSendTo)
    {
        httpClient = new() { BaseAddress = new Uri($"https://api.telegram.org/bot{botToken}/") };
        ChatIdsToSendTo = chatIdsToSendTo;
    }

    public void Send(string message)
    {

        foreach (string channel in ChatIdsToSendTo)
        {
            string requestUrlSuffix = $"sendMessage?chat_id={channel}&text={message}";

            httpClient.GetAsync(requestUrlSuffix);

            // TODO: check response for errors
        }
    }
}
