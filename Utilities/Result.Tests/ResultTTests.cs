using FluentAssertions;

namespace Result.Tests;

public sealed class ResultTTests
{
    [Fact]
    public void Succeed__WhenCalled_ReturnsSuccessfulResultWithValue()
    {
        // Arrange.
        var expectedValue = 42;

        // Act.
        var result = Result<int>.Succeed(expectedValue);

        // Assert.
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void Succeed_WhenAccessingErrorMessage_ThrowsInvalidOperationException()
    {
        // Arrange.
        var result = Result<string>.Succeed("Success data");

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
        var errorMessage = "Not found.";

        // Act.
        var result = Result<int>.Fail(errorMessage);

        // Assert.
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(errorMessage);
    }

    [Fact]
    public void Fail_WhenAccessingValue_ThrowsInvalidOperationException()
    {
        // Arrange.
        var result = Result<double>.Fail("Calculation error.");

        // Act.
        var action = () => result.Value;

        // Assert.
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't read the value of a failed result.");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Fail_WhenNullOrWhitespaceErrorMessage_ThrowsArgumentException(string? invalidErrorMessage)
    {
        // Act.
        var action = () => Result<bool>.Fail(invalidErrorMessage!);

        // Assert.
        action.Should().Throw<ArgumentException>()
            .WithMessage("*A failed result must have an error message.*");
    }
}
