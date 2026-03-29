namespace Utilities;

public static class ResultExtensions
{
    public static Result<Target> ToFailureOf<Target>(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Can't convert successful result to failed result.");
        }

        return Result<Target>.Fail(result.ErrorMessage);
    }
}
