// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

namespace AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

public class SpotsPooler
{
    private static readonly object lockObj = new();
    private static readonly Client client = new();


    private Timer CallbackTimer { get; init; }
    private TimeSpan CallbackTimeSpan { get; init; }
    private uint NSpots { get; init; }



    public SpotsPooler(TimeSpan timeSpan, uint nSpots = 20)
    {
        NSpots = nSpots;
        CallbackTimeSpan = timeSpan;
        CallbackTimer = new(SpotsPoolerCallback, this, Timeout.InfiniteTimeSpan, CallbackTimeSpan);
    }

    public void Start()
    {
        CallbackTimer.Change(TimeSpan.Zero, CallbackTimeSpan);
    }

    public void Stop()
    {
        CallbackTimer.Change(Timeout.InfiniteTimeSpan, CallbackTimeSpan);
    }

    private static void SpotsPoolerCallback(object? state)
    {
        if (state == null)
        {
            throw new ArgumentNullException($"{nameof(state)} cannot be null here.");
        }

        var Self = (SpotsPooler)state;

        // Prevent overlapping execution by checking if the method is already running
        if (Monitor.TryEnter(lockObj))
        {
            try
            {
                // TODO: logic
                // Will require the `state` object to pass `this`.
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }

    }
}
