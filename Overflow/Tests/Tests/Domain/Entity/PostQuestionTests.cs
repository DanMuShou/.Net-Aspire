using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entity;
using Domain.Entity.PostServer.Post;
using Domain.Exceptions.Rules;
using System.Collections.Generic;

namespace Tests.Domain.Entity;

public class PostQuestionTests
{
    [Theory]
    [AutoData]
    public void Constructor_WithValidParameters_CreatesPostQuestion(
        string title,
        string content,
        string askerId,
        string askerDisplayName,
        List<string> tagSlugs
    )
    {
        // Act
        var postQuestion = new PostQuestion(title, content, askerId, askerDisplayName, tagSlugs);

        // Assert
        postQuestion.Should().NotBeNull();
        postQuestion.Id.Should().NotBeEmpty();
        postQuestion.Title.Should().Be(title);
        postQuestion.Content.Should().Be(content);
        postQuestion.AskerId.Should().Be(askerId);
        postQuestion.AskerDisplayName.Should().Be(askerDisplayName);
        postQuestion.CreateAt.Should().NotBe(default(DateTime));
        postQuestion.UpdateAt.Should().BeNull();
        postQuestion.ViewCount.Should().Be(0);
        postQuestion.TagSlugs.Should().BeEquivalentTo(tagSlugs);
        postQuestion.HasAcceptedAnswer.Should().BeFalse();
        postQuestion.Votes.Should().Be(0);
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData("   ")]
    public void Constructor_WithInvalidTitle_ThrowsEntityRuleException(string? title, List<string> tagSlugs)
    {
        // Arrange
        var content = "This is a test question content";
        var askerId = "user123";
        var askerDisplayName = "Test User";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() =>
            new PostQuestion(title!, content, askerId, askerDisplayName, tagSlugs)
        );
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData("   ")]
    public void Constructor_WithInvalidContent_ThrowsEntityRuleException(string? content, List<string> tagSlugs)
    {
        // Arrange
        var title = "Test Question";
        var askerId = "user123";
        var askerDisplayName = "Test User";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() =>
            new PostQuestion(title, content!, askerId, askerDisplayName, tagSlugs)
        );
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData("   ")]
    public void Constructor_WithInvalidAskerId_ThrowsEntityRuleException(string? askerId, List<string> tagSlugs)
    {
        // Arrange
        var title = "Test Question";
        var content = "This is a test question content";
        var askerDisplayName = "Test User";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() =>
            new PostQuestion(title, content, askerId!, askerDisplayName, tagSlugs)
        );
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData("   ")]
    public void Constructor_WithInvalidAskerDisplayName_ThrowsEntityRuleException(
        string? askerDisplayName,
        List<string> tagSlugs
    )
    {
        // Arrange
        var title = "Test Question";
        var content = "This is a test question content";
        var askerId = "user123";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() =>
            new PostQuestion(title, content, askerId, askerDisplayName!, tagSlugs)
        );
    }

    [Theory]
    [AutoData]
    public void View_IncrementsViewCount(
        string title,
        string content,
        string askerId,
        string askerDisplayName,
        List<string> tagSlugs
    )
    {
        // Arrange
        var postQuestion = new PostQuestion(title, content, askerId, askerDisplayName, tagSlugs);
        var initialViewCount = postQuestion.ViewCount;

        // Act
        postQuestion.View();

        // Assert
        postQuestion.ViewCount.Should().Be(initialViewCount + 1);
    }

    [Theory]
    [AutoData]
    public void ChangeAcceptedAnswer_TogglesHasAcceptedAnswer(
        string title,
        string content,
        string askerId,
        string askerDisplayName,
        List<string> tagSlugs
    )
    {
        // Arrange
        var postQuestion = new PostQuestion(title, content, askerId, askerDisplayName, tagSlugs);
        var initialStatus = postQuestion.HasAcceptedAnswer;

        // Act
        postQuestion.ChangeAcceptedAnswer();

        // Assert
        postQuestion.HasAcceptedAnswer.Should().NotBe(initialStatus);

        // Act again
        postQuestion.ChangeAcceptedAnswer();

        // Assert
        postQuestion.HasAcceptedAnswer.Should().Be(initialStatus);
    }

    [Theory]
    [AutoData]
    public void AddVotes_IncrementsVotes(
        string title,
        string content,
        string askerId,
        string askerDisplayName,
        List<string> tagSlugs
    )
    {
        // Arrange
        var postQuestion = new PostQuestion(title, content, askerId, askerDisplayName, tagSlugs);
        var initialVotes = postQuestion.Votes;

        // Act
        postQuestion.AddVotes();

        // Assert
        postQuestion.Votes.Should().Be(initialVotes + 1);
    }

    [Theory]
    [AutoData]
    public void Update_SetsUpdateAtToCurrentTime(
        string title,
        string content,
        string askerId,
        string askerDisplayName,
        List<string> tagSlugs
    )
    {
        // Arrange
        var postQuestion = new PostQuestion(title, content, askerId, askerDisplayName, tagSlugs);

        // Act
        postQuestion.Update();

        // Assert
        postQuestion.UpdateAt.Should().NotBeNull();
        postQuestion.UpdateAt.Should().BeOnOrBefore(DateTime.UtcNow);
    }
}