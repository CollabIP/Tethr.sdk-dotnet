﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tethr.Sdk.Model;

namespace Tethr.Sdk.Heartbeat;

public sealed class TethrHeartbeatService(
    IOptionsMonitor<TethrHeartbeatOptions> options,
    TethrHeartbeat heartbeat,
    ILogger<TethrHeartbeatService> logger)
    : BackgroundService
{
    /// <summary>
    /// Indicates that our connection to Tethr is online, and we can send call to Tethr.
    /// </summary>
    public bool Online { get; private set; }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var initPeriod = TimeSpan.FromSeconds(options.CurrentValue.HeartbeatIntervalSeconds ?? 0);
        var enabled = true;

        if (initPeriod.TotalSeconds < 1)
        {
            initPeriod = TimeSpan.FromSeconds(10);
            enabled = false;
        }

        logger.SetHeartbeatIntervalSeconds((int)initPeriod.TotalSeconds);
        using var timer = new PeriodicTimer(initPeriod);

#if NET8_0_OR_GREATER
        using var option = options.OnChange(o =>
        {
            try
            {
                initPeriod = TimeSpan.FromSeconds(o.HeartbeatIntervalSeconds ?? 0);
                if (initPeriod.TotalSeconds > 1)
                {
                    enabled = true;
                }
                else
                {
                    initPeriod = TimeSpan.FromSeconds(10);
                    enabled = false;
                }

                // ReSharper disable once AccessToDisposedClosure
                if (timer.Period == initPeriod) return;
                // ReSharper disable once AccessToDisposedClosure
                timer.Period = initPeriod;
                logger.SetHeartbeatIntervalSeconds((int)initPeriod.TotalSeconds);
            }
            catch (Exception e)
            {
                logger.ErrorUpdatingOptions(e);
            }
        });
#endif

        var errorCount = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            await timer.WaitForNextTickAsync(stoppingToken).ConfigureAwait(false);
            if (!enabled) continue;

            try
            {
                var status = MonitorStatus.Healthy;
                var statusCallback = options.CurrentValue.StatusCallback;
                if (statusCallback != null)
                    status = statusCallback();
                
                await heartbeat.Send(status).ConfigureAwait(false);

                if (errorCount > 0)
                {
                    logger.HeartBeatReestablished();
                    Online = true;
                }
                
                errorCount = 0;
            }
            catch (Exception e)
            {
                errorCount++;
                if (errorCount == 1)
                {
                    Online = false;
                    logger.ErrorSendingHeartBeat(e);
                }
                else if (errorCount % 100 == 0)
                {
                    logger.ErrorSendingHeartBeat(e);
                }
            }
        }

        logger.HeartBeatStopped();
        try
        {
            await heartbeat.Send(MonitorStatus.Offline).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            logger.ErrorSendingHeartBeat(e);
        }
    }
}