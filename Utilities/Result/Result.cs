namespace Utilities;

public record Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string ErrorMessage => IsFailure
        ? _errorMessage!
        : throw new InvalidOperationException("Can't read error message on a successful result.");

    private readonly string? _errorMessage;

    protected Result(bool isSuccess, string? errorMessage) 
    {
        if (!isSuccess && string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new ArgumentException("A failed result must have an error message.", nameof(errorMessage));
        }

        IsSuccess = isSuccess;
        _errorMessage = errorMessage;
    }

    public static Result Succeed() => new(true, null);

    public static Result Fail(string errorMessage) => new(false, errorMessage);
}

public record Result<T> : Result
{
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Can't read the value of a failed result.");

    private readonly T? _value;

    private Result(bool isSuccess, string? errorMessage, T? value) 
        : base(isSuccess, errorMessage)
    {
        _value = value;
    }

    public static Result<T> Succeed(T value) => new(true, null, value);

    public static new Result<T> Fail(string errorMessage) => new(false, errorMessage, default);

    public static implicit operator Result<T>(T value) => Succeed(value);
}
