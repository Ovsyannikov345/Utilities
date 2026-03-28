using FluentAssertions;

namespace Result.Tests;

public sealed class ResultExtensionsTests
{
    [Fact]
    public void ToFailureOf_WhenCalledOnFailedResult_ReturnsGenericFailedResult()
    {
        // Arrange.
        var originalErrorMessage = "Database connection failed.";
        var baseResult = Result.Fail(originalErrorMessage);

        // Act.
        var genericResult = baseResult.ToFailureOf<int>();

        // Assert.
        genericResult.IsSuccess.Should().BeFalse();
        genericResult.IsFailure.Should().BeTrue();
        genericResult.ErrorMessage.Should().Be(originalErrorMessage);
    }

    [Fact]
    public void ToFailureOf_WhenCalledOnSuccessfulResult_ThrowsInvalidOperationException()
    {
        // Arrange
        var baseResult = Result.Succeed();

        // Act
        var action = () => baseResult.ToFailureOf<DateTime>();

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't convert successful result to failed result.");
    }
}
