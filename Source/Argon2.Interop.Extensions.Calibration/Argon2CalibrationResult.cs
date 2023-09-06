namespace Argon2.Interop;

/// <summary>
/// The result returned from Argon2 calibration.
/// </summary>
/// <param name="ExecutionResults">All results from various Argon2 executions performed during calibration.</param>
/// <param name="BestResult">The best Argon2 execution result produced during calibration.</param>
public record Argon2CalibrationResult(ICollection<Argon2ExecutionResult> ExecutionResults, Argon2ExecutionResult? BestResult);