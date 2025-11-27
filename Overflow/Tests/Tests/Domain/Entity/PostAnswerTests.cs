using AutoFixture.Xunit2;
using Domain.Entity;
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
        string postQuestionId
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
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidContent_ThrowsEntityRuleException(string? content)
    {
        // Arrange
        var userId = "user123";
        var userDisplayName = "Test User";
        var postQuestionId = "question123";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() =>
            new PostAnswer(content!, userId, userDisplayName, postQuestionId)
        );
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidUserId_ThrowsEntityRuleException(string? userId)
    {
        // Arrange
        var content = "This is a test answer content";
        var userDisplayName = "Test User";
        var postQuestionId = "question123";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() =>
            new PostAnswer(content, userId!, userDisplayName, postQuestionId)
        );
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidUserDisplayName_ThrowsEntityRuleException(
        string? userDisplayName
    )
    {
        // Arrange
        var content = "This is a test answer content";
        var userId = "user123";
        var postQuestionId = "question123";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() =>
            new PostAnswer(content, userId, userDisplayName!, postQuestionId)
        );
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidPostQuestionId_ThrowsEntityRuleException(
        string? postQuestionId
    )
    {
        // Arrange
        var content = "This is a test answer content";
        var userId = "user123";
        var userDisplayName = "Test User";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() =>
            new PostAnswer(content, userId, userDisplayName, postQuestionId!)
        );
    }
}
