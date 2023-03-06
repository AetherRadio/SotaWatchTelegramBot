// This file is part of Aether Radio's SOTA Watch Telegram Bot.
// SPDX-License-Identifier: Apache-2.0
// SPDX-FileCopyrightText: 2023 Rui Oliveira <ruimail24@gmail.com>

namespace AetherRadio.SotaWatchTelegramBot.SotaWatchApiClient;

public class SpotsPooler
{
    public event EventHandler<List<Spot>>? NewSpots;

    private static readonly object lockObj = new();

    private Timer CallbackTimer { get; init; }
    private TimeSpan CallbackTimeSpan { get; init; }
    private uint NSpots { get; init; }
    private int LastSpotId { get; set; } = 0;

    public SpotsPooler(TimeSpan timeSpan, uint nSpots = 20)
    {
        NSpots = nSpots;
        CallbackTimeSpan = timeSpan;
        CallbackTimer = new(SpotsPoolerCallback, this, Timeout.InfiniteTimeSpan, CallbackTimeSpan);
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
    }

    private static void SpotsPoolerCallback(object? state)
    {
        if (state == null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        // Prevent overlapping execution by checking if the method is already running
        if (Monitor.TryEnter(lockObj))
        {
            try
            {
                var self = (SpotsPooler)state;

                var incomingSpots = ApiClient.QuerySpots(self.NSpots).Result;
                if (incomingSpots != null)
                {
                    self.CheckForNewSpots(incomingSpots);
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
        var newSpots = incomingSpots.Where(spot => spot.Id > LastSpotId).OrderBy(spot => spot.Id);
        if (newSpots.Any())
        {
            OnNewSpots(newSpots.ToList());
            LastSpotId = newSpots.Last().Id;
        }
    }
}
