using FluentAssertions;

namespace Result.Tests;

public class ResultTests
{
    [Fact]
    public void Succeed_WhenCalled_ReturnsSuccessfulResult()
    {
        // Act.
        var result = Result.Succeed();

        // Assert.
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public void Succeed_WhenAccessingErrorMessage_ThrowsInvalidOperationException()
    {
        // Arrange.
        var result = Result.Succeed();

        // Act.
        var action = () => result.ErrorMessage;

        // Assert.
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't read error message on a successful result.");
    }

    [Fact]
    public void Fail_WhenCalled_ReturnsFailedResultWithErrorMessage()
    {
        // Arrange.
        var errorMessage = "Something went wrong.";

        // Act.
        var result = Result.Fail(errorMessage);

        // Assert.
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(errorMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Fail_WhenNullOrWhitespaceErrorMessage_ThrowsArgumentException(string? invalidErrorMessage)
    {
        // Act.
        var action = () => Result.Fail(invalidErrorMessage!);

        // Assert.
        action.Should().Throw<ArgumentException>()
            .WithMessage("*A failed result must have an error message.*");
    }
}
