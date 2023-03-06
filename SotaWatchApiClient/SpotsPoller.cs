// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

namespace AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

public class SpotsPoller
{
    public event EventHandler<List<Spot>>? NewSpots;

    private static readonly object lockObj = new();

    private Timer CallbackTimer { get; init; }
    private TimeSpan CallbackTimeSpan { get; init; }
    private uint NSpots { get; init; }
    private ulong LastSpotId { get; set; } = 0;

    public SpotsPoller(TimeSpan timeSpan, uint nSpots = 20)
    {
        NSpots = nSpots;
        CallbackTimeSpan = timeSpan;
        CallbackTimer = new(SpotsPoolerCallback, null, Timeout.InfiniteTimeSpan, CallbackTimeSpan);
    }

    protected virtual void OnNewSpots(List<Spot> e)
    {
        NewSpots?.Invoke(this, e);
    }

    public void Start()
    {
        CallbackTimer.Change(TimeSpan.Zero, CallbackTimeSpan);
    }

    public void Stop()
    {
        CallbackTimer.Change(Timeout.InfiniteTimeSpan, CallbackTimeSpan);
        // TODO: What do I need to do to ensure gracefully stopping?
    }

    private void SpotsPoolerCallback(object? state)
    {
        // Prevent overlapping execution by checking if the method is already running
        if (Monitor.TryEnter(lockObj))
        {
            try
            {
                var incomingSpots = ApiClient.QuerySpots(NSpots).Result;
                if (incomingSpots != null)
                {
                    CheckForNewSpots(incomingSpots);
                }
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }
    }

    private void CheckForNewSpots(List<Spot> incomingSpots)
    {
        var newSpots = incomingSpots.Where(spot => UlongIsGreaterThan(spot.Id, LastSpotId)).OrderBy(spot => spot.Id);
        if (newSpots.Any())
        {
            OnNewSpots(newSpots.ToList());
            LastSpotId = newSpots.Last().Id;
        }
    }

    private static bool UlongIsGreaterThan(ulong newValue, ulong oldValue)
    {
        // Check if newValue is greater than oldValue, accounting for ulong wrap around.
        if (newValue > oldValue)
        {
            return (newValue - oldValue) < (ulong.MaxValue / 2);
        }
        else
        {
            return (oldValue - newValue) > (ulong.MaxValue / 2);
        }
    }
}
