namespace Domain.Common;

public readonly struct Result<TValue, TError>
{
    public readonly TValue Value;
    public readonly TError Error;
    public bool Success { get; }

    private Result(TValue v, TError e, bool success)
    {
        Value = v;
        Error = e;
        Success = success;
    }

    public static Result<TValue, TError> Ok(TValue v)
    {
        return new(v, default, true);
    }

    public static Result<TValue, TError> Err(TError e)
    {
        return new(default, e, false);
    }

    public static implicit operator Result<TValue, TError>(TValue v) => new(v, default, true);
    public static implicit operator Result<TValue, TError>(TError e) => new(default, e, false);

    public R Match<R>(Func<TValue, R> success, Func<TError, R> failure) => Success ? success(Value) : failure(Error);
}
