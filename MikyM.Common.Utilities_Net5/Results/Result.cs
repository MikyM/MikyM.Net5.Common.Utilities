using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MikyM.Common.Utilities_Net5.Results.Errors;

namespace MikyM.Common.Utilities_Net5.Results
{
    /// <inheritdoc />
    [PublicAPI]
    public readonly struct Result : IResult
    {
        /// <inheritdoc />
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess => this.Error is null;

        /// <inheritdoc />
        public IResult? Inner { get; }

        /// <inheritdoc />
        public IResultError? Error { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> struct.
        /// </summary>
        /// <param name="error">The error, if any.</param>
        /// <param name="inner">The inner result, if any.</param>
        private Result(IResultError? error, IResult? inner)
        {
            this.Error = error ?? inner?.Error;
            this.Inner = inner;
        }

        /// <inheritdoc />
        [MemberNotNullWhen(true, nameof(Inner))]
        public bool TryGetInner(out IResult? inner)
        {
            inner = this.Inner;
            return inner is not null;
        }

        /// <inheritdoc />
        [MemberNotNullWhen(true, nameof(Error))]
        public bool TryGetError(out IResultError? error)
        {
            error = this.Error;
            return error is not null;
        }

        /// <summary>
        /// Creates a new successful result.
        /// </summary>
        /// <returns>The successful result.</returns>
        public static Result FromSuccess()
            => new(default, default);

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <typeparam name="TError">The error type.</typeparam>
        /// <param name="error">The error.</param>
        /// <returns>The failed result.</returns>
        public static Result FromError<TError>(TError error) where TError : IResultError
            => new(error, default);

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <typeparam name="TError">The error type.</typeparam>
        /// <param name="error">The error.</param>
        /// <param name="inner">The inner error that caused this error, if any.</param>
        /// <returns>The failed result.</returns>
        public static Result FromError<TError>(TError error, IResult inner) where TError : IResultError
            => new(error, inner);

        /// <summary>
        /// Creates a new failed result from another result.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the base result.</typeparam>
        /// <param name="result">The error.</param>
        /// <returns>The failed result.</returns>
        public static Result FromError<TEntity>(Result<TEntity> result)
            => new(result.Error, result);

        /// <summary>
        /// Converts an error into a failed result.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The failed result.</returns>
        public static implicit operator Result(ResultError error)
            => new(error, default);

        /// <summary>
        /// Converts an exception into a failed result.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The failed result.</returns>
        public static implicit operator Result(Exception exception)
            => new(new ExceptionError(exception), default);
    }

    /// <inheritdoc />
    [PublicAPI]
    public readonly struct Result<TEntity> : IResult<TEntity>
    {
        /// <summary>
        /// Gets the entity returned by the result.
        /// </summary>
        [AllowNull]
        public TEntity? Entity { get; }

        /// <inheritdoc />
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess => this.Error is null;

        /// <inheritdoc />
        public IResult? Inner { get; }

        /// <inheritdoc />
        public IResultError? Error { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{TEntity}"/> struct.
        /// </summary>
        /// <param name="entity">The entity, if any.</param>
        /// <param name="error">The error, if any.</param>
        /// <param name="inner">The inner result, if any.</param>
        private Result(TEntity? entity, IResultError? error, IResult? inner)
        {
            this.Error = error ?? inner?.Error;
            this.Inner = inner;
            this.Entity = entity;
        }

        /// <inheritdoc />
        [MemberNotNullWhen(true, nameof(Entity))]
        public bool TryGetEntity(out TEntity? entity)
        {
            entity = this.Entity;
            return entity is not null;
        }

        /// <inheritdoc />
        [MemberNotNullWhen(true, nameof(Inner))]
        public bool TryGetInner(out IResult? inner)
        {
            inner = this.Inner;
            return inner is not null;
        }

        /// <inheritdoc />
        [MemberNotNullWhen(true, nameof(Error))]
        public bool TryGetError(out IResultError? error)
        {
            error = this.Error;
            return error is not null;
        }

        /// <summary>
        /// Determines whether the result contains a defined value; that is, it has a value, and the value is not null.
        /// </summary>
        /// <returns>true if the result contains a defined value; otherwise, false.</returns>
        [MemberNotNullWhen(true, nameof(Entity))]
        public bool IsDefined() => this.IsSuccess && this.Entity is not null;

        /// <summary>
        /// Determines whether the result contains a defined value; that is, it has a value, and the value is not null.
        /// </summary>
        /// <param name="entity">The entity, if it is defined.</param>
        /// <returns>true if the result contains a defined value; otherwise, false.</returns>
        [MemberNotNullWhen(true, nameof(Entity))]
        public bool IsDefined([NotNullWhen(true)] out TEntity? entity)
        {
            entity = default;

            if (!this.IsSuccess) return false;

            if (this.Entity is null) return false;

            entity = this.Entity;
            return true;
        }

        /// <summary>
        /// Creates a new successful result.
        /// </summary>
        /// <param name="entity">The returned entity.</param>
        /// <returns>The successful result.</returns>
        public static Result<TEntity> FromSuccess(TEntity entity) => new(entity, default, default);

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <typeparam name="TError">The error type.</typeparam>
        /// <param name="error">The error.</param>
        /// <returns>The failed result.</returns>
        public static Result<TEntity> FromError<TError>(TError error) where TError : IResultError
            => new(default, error, default);

        /// <summary>
        /// Creates a new failed result.
        /// </summary>
        /// <typeparam name="TError">The error type.</typeparam>
        /// <param name="error">The error.</param>
        /// <param name="inner">The inner error that caused this error, if any.</param>
        /// <returns>The failed result.</returns>
        public static Result<TEntity> FromError<TError>(TError error, IResult inner) where TError : IResultError
            => new(default, error, inner);

        /// <summary>
        /// Creates a new failed result from another result.
        /// </summary>
        /// <typeparam name="TOtherEntity">The entity type of the base result.</typeparam>
        /// <param name="result">The error.</param>
        /// <returns>The failed result.</returns>
        public static Result<TEntity> FromError<TOtherEntity>(Result<TOtherEntity> result)
            => new(default, result.Error, result);

        /// <summary>
        /// Creates a new failed result from another result.
        /// </summary>
        /// <param name="result">The error.</param>
        /// <returns>The failed result.</returns>
        public static Result<TEntity> FromError(Result result)
            => new(default, result.Error, result);

        /// <summary>
        /// Converts an entity into a successful result.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The successful result.</returns>
        public static implicit operator Result<TEntity>(TEntity? entity)
        {
            return new(entity, default, default);
        }

        /// <summary>
        /// Converts an error into a failed result.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The failed result.</returns>
        public static implicit operator Result<TEntity>(ResultError error)
        {
            return new(default, error, default);
        }

        /// <summary>
        /// Converts an exception into a failed result.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The failed result.</returns>
        public static implicit operator Result<TEntity>(Exception exception)
        {
            return new(default, new ExceptionError(exception), default);
        }
    }
}