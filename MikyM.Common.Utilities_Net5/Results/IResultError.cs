using JetBrains.Annotations;

namespace MikyM.Common.Utilities_Net5.Results
{
    /// <summary>
    /// Represents an error returned by a result.
    /// </summary>
    [PublicAPI]
    public interface IResultError
    {
        /// <summary>
        /// Gets the human-readable error message.
        /// </summary>
        string Message { get; }
    }
}