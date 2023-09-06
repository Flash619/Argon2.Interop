using System.Security.Cryptography;

namespace Argon2.Interop;

/// <summary>
/// A calibrator used to tweak Argon2 options for maximum strength and performance.
/// </summary>
/// <remarks>
/// The calibrator will always prioritize certain options over others. The calibrator will always attempt to use as much memory as possible, and will only increase the time cost once
/// the maximum memory cost has been met.The calibrator will try it's best to meet the maximum hash duration defined. Under certain circumstances no "best result" may be found if
/// no executions meet the requirements specified.
/// </remarks>
public class Argon2Calibrator
{
    private readonly Argon2CalibratorOptions _options;
    private readonly Argon2Executor _executor;

    /// <summary>
    /// Creates a new Argon2Calibrator instance with default options.
    /// </summary>
    public Argon2Calibrator() : this(new Argon2CalibratorOptions())
    {
    }
    
    /// <summary>
    /// Creates a new Argon2Calibrator instance with the provided options.
    /// </summary>
    /// <param name="options">The options.</param>
    public Argon2Calibrator(Argon2CalibratorOptions options)
    {
        _options = options;
        _executor = options.UseThreadedExecution ? new ThreadedArgon2Executor(_options.ExecutionThreadCount, _options.ExecutionDuration, new Argon2Provider()) : new Argon2Executor(new Argon2Provider());
    }

    /// <summary>
    /// Calibrates Argon2 based on the options provided, returning a calibration result.
    /// </summary>
    /// <returns>The calibration result.</returns>
    public Argon2CalibrationResult Calibrate()
    {
        var testedOptions = new HashSet<Argon2Options>();
        var executionResults = new List<Argon2ExecutionResult>();
        var password = RandomNumberGenerator.GetBytes((int) _options.PasswordSize);
        var salt = RandomNumberGenerator.GetBytes((int) _options.SaltSize);

        Argon2ExecutionResult? lastResult = default;
        
        do
        {
            var options = lastResult == default ? GetInitialOptions(_options) : GetNextOptions(_options, lastResult);

            if (testedOptions.Contains(options))
            {
                break;
            }
            
            var duration = _executor.Execute(password, salt, options);

            lastResult = new Argon2ExecutionResult(options, duration);
            
            testedOptions.Add(options);
            executionResults.Add(lastResult);
        } while (executionResults.Count < _options.MaxExecutions);

        var result = new Argon2CalibrationResult(executionResults, executionResults.Where(x => x.Duration < _options.MaxDuration).MaxBy(x => x.Duration));

        return result;
    }

    private static Argon2Options GetInitialOptions(Argon2CalibratorOptions calibratorOptions)
    {
        return new Argon2Options()
        {
            HashLength = calibratorOptions.HashLength,
            MemoryCost = calibratorOptions.MaxMemoryCost,
            Parallelism = calibratorOptions.Parallelism,
            TimeCost = calibratorOptions.MinTimeCost,
            Type = calibratorOptions.Type,
            Version = calibratorOptions.Version
        };
    }

    private static Argon2Options GetNextOptions(Argon2CalibratorOptions calibratorOptions, Argon2ExecutionResult lastResult)
    {
        var diff = Math.Abs(calibratorOptions.MaxDuration.TotalMilliseconds - lastResult.Duration.TotalMilliseconds);
        var diffPercent = (diff / calibratorOptions.MaxDuration.TotalMilliseconds) * 100;
        
        if (lastResult.Duration < calibratorOptions.MaxDuration)
        {
            if (lastResult.Options.MemoryCost < calibratorOptions.MaxMemoryCost)
            {
                return new Argon2Options(lastResult.Options)
                {
                    MemoryCost = Math.Min(calibratorOptions.MaxMemoryCost, lastResult.Options.MemoryCost + (uint) Math.Round((double)lastResult.Options.MemoryCost / 100 * diffPercent))
                };
            }
            else if (lastResult.Options.TimeCost < calibratorOptions.MaxTimeCost)
            {
                return new Argon2Options(lastResult.Options)
                {
                    TimeCost = Math.Min(lastResult.Options.TimeCost + 2, calibratorOptions.MaxTimeCost)
                };
            }
        }
        else
        {
            if (lastResult.Options.TimeCost > calibratorOptions.MinTimeCost)
            {
                return new Argon2Options(lastResult.Options)
                {
                    TimeCost = Math.Max(lastResult.Options.TimeCost - 1, calibratorOptions.MinTimeCost)
                };
            } else if (lastResult.Options.MemoryCost > calibratorOptions.MinMemoryCost)
            {
                return new Argon2Options(lastResult.Options)
                {
                    MemoryCost = Math.Min(calibratorOptions.MaxMemoryCost, lastResult.Options.MemoryCost - (uint) Math.Round((double) lastResult.Options.MemoryCost / 100 * Math.Min(diffPercent, 80)))
                };
            }
        }

        return lastResult.Options;
    }
}