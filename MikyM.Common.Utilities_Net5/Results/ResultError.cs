namespace MikyM.Common.Utilities_Net5.Results
{
    /// <summary>
    /// Acts as a base class for result errors.
    /// </summary>
    public abstract record ResultError(string Message) : IResultError;
}