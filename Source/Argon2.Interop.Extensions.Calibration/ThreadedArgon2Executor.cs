using System.Diagnostics;

namespace Argon2.Interop;

/// <summary>
/// A threaded Argon2 executor which will execute Argon2 many times on multiple
/// threads for a fixed duration, averaging the duration of each execution.
/// </summary>
public class ThreadedArgon2Executor : Argon2Executor
{
    private readonly uint _threads;
    private readonly TimeSpan _duration;

    /// <summary>
    /// Creates a new ThreadedArgon2Executor instance with the given thread count, execution duration, and Argon2 provider.
    /// </summary>
    /// <param name="threads">The thread count.</param>
    /// <param name="duration">The execution duration.</param>
    /// <param name="provider">The provider.</param>
    public ThreadedArgon2Executor(uint threads, TimeSpan duration, Argon2Provider provider) : base(provider)
    {
        _threads = threads;
        _duration = duration;
    }

    /// <summary>
    /// Executes Argon2 many times across the number of threads specified when this executor was constructed.
    /// </summary>
    /// <remarks>
    /// Argon2 will be executed across several threads until the maximum execution duration has been reached.
    /// The execution duration returned represents the average execution duration from all executions across
    /// all threads. Due to the multi-threaded nature of this executor, the total execution duration may
    /// exceed the maximum execution duration defined in the options, as this method will not return until
    /// all threads have completed all execution requests.
    /// </remarks>
    /// <param name="password">The password.</param>
    /// <param name="salt">The salt.</param>
    /// <param name="options">The options.</param>
    /// <returns>The duration.</returns>
    public override TimeSpan Execute(byte[] password, byte[] salt, Argon2Options options)
    {
        var threads = new Thread?[_threads];
        var durations = new List<TimeSpan>();

        var startTime = DateTimeOffset.UtcNow;
        var endTime = startTime.Add(_duration);

        while (DateTimeOffset.UtcNow < endTime)
        {
            for (var i = 0; i < threads.Length; i++)
            {
                var thread = threads[i];

                if (thread is not { IsAlive: true })
                {
                    threads[i] = new Thread(() =>
                    {
                        durations.Add(ExecuteThread(password, salt, options, Provider));
                    });
                    
                    threads[i]!.Priority = ThreadPriority.Highest;
                    threads[i]!.Start();
                }
            }
        }

        while (threads.Any(x => x.IsAlive))
        {
            Thread.Sleep(500);
        }

        var averageTicks = 0L;
        
        foreach (var duration in durations)
        {
            averageTicks = averageTicks == 0 ? duration.Ticks : (averageTicks + duration.Ticks) / 2;
        }

        return new TimeSpan(averageTicks);
    }

    private static TimeSpan ExecuteThread(byte[] password, byte[] salt, Argon2Options options, Argon2Provider provider)
    {
        var stopwatch = Stopwatch.StartNew();

        provider.GetArgon2(options).Hash(password, salt);
        
        stopwatch.Stop();

        return stopwatch.Elapsed;
    }
}