// This file is part of Lisbeth.Bot project
//

using System;
using JetBrains.Annotations;
using MikyM.Common.Utilities_Net5.Results.Errors.Bases;

namespace MikyM.Common.Utilities_Net5.Results.Errors
{
    /// <summary>
    /// Represents an error arising from an argument being outside of an expected range.
    /// </summary>
    /// <param name="Name">The name of the argument.</param>
    /// <param name="message">The error message.</param>
    /// <remarks>Used in place of <see cref="ArgumentOutOfRangeException"/>.</remarks>
    public record ArgumentOutOfRangeError
    (
        [InvokerParameterName] string Name,
        string message = "Value was outside of the expected range"
    ) : ArgumentError(Name, message);
}