using AutoFixture.Xunit2;
using Domain.Entity.PostServer.Post;
using Domain.Exceptions.Rules;

namespace Tests.Domain.Entity;

public class PostAnswerTests
{
    [Theory]
    [AutoData]
    public void Constructor_WithValidParameters_CreatesPostAnswer(
        string content,
        string userId,
        string userDisplayName,
        Guid postQuestionId
    )
    {
        // Act
        var postAnswer = new PostAnswer(content, userId, userDisplayName, postQuestionId);

        // Assert
        postAnswer.Should().NotBeNull();
        postAnswer.Id.Should().NotBeEmpty();
        postAnswer.Content.Should().Be(content);
        postAnswer.UserId.Should().Be(userId);
        postAnswer.UserDisplayName.Should().Be(userDisplayName);
        postAnswer.CreatedAt.Should().NotBe(default(DateTime));
        postAnswer.IsAccepted.Should().BeFalse();
        postAnswer.PostQuestionId.Should().Be(postQuestionId);
        postAnswer.PostQuestion.Should().BeNull();
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData("   ")]
    public void Constructor_WithInvalidContent_ThrowsEntityRuleException(
        string? content,
        string userId,
        string userDisplayName,
        Guid postQuestionId
    )
    {
        // Act & Assert
        Action action = () => new PostAnswer(content!, userId, userDisplayName, postQuestionId);
        Assert.Throws<EntityRuleException>(action);
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData("   ")]
    public void Constructor_WithInvalidUserId_ThrowsEntityRuleException(
        string? userId,
        string content,
        string userDisplayName,
        Guid postQuestionId
    )
    {
        // Act & Assert
        Action action = () => new PostAnswer(content, userId!, userDisplayName, postQuestionId);
        Assert.Throws<EntityRuleException>(action);
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData("   ")]
    public void Constructor_WithInvalidUserDisplayName_ThrowsEntityRuleException(
        string? userDisplayName,
        string content,
        string userId,
        Guid postQuestionId
    )
    {
        // Act & Assert
        Action action = () => new PostAnswer(content, userId, userDisplayName!, postQuestionId);
        Assert.Throws<EntityRuleException>(action);
    }

    [Theory]
    [InlineAutoData("")]
    [InlineAutoData("   ")]
    public void Constructor_WithInvalidPostQuestionId_ThrowsEntityRuleException(
        string? postQuestionId,
        string content,
        string userId,
        string userDisplayName
    )
    {
        // Act & Assert
        Action action = () => new PostAnswer(content, userId, userDisplayName, Guid.Empty);
        Assert.Throws<EntityRuleException>(action);
    }

    [Theory]
    [AutoData]
    public void Constructor_WithEmptyGuidPostQuestionId_ThrowsEntityRuleException(
        string content,
        string userId,
        string userDisplayName
    )
    {
        // Arrange
        var postQuestionId = Guid.Empty;

        // Act & Assert
        Action action = () => new PostAnswer(content, userId, userDisplayName, postQuestionId);
        Assert.Throws<EntityRuleException>(action);
    }

    [Theory]
    [AutoData]
    public void ChangeAccept_TogglesIsAccepted(
        string content,
        string userId,
        string userDisplayName,
        Guid postQuestionId
    )
    {
        // Arrange
        var postAnswer = new PostAnswer(content, userId, userDisplayName, postQuestionId);
        var initialStatus = postAnswer.IsAccepted;

        // Act
        postAnswer.ChangeAccept();

        // Assert
        postAnswer.IsAccepted.Should().Be(!initialStatus);

        // Act again
        postAnswer.ChangeAccept();

        // Assert
        postAnswer.IsAccepted.Should().Be(initialStatus);
    }
}
