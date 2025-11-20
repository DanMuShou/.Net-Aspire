using AutoFixture.Xunit2;
using Domain.Entity;
using Domain.Entity.PostServer.Post;
using Domain.Exceptions.Rules;

namespace Tests.Domain.Entity;

public class PostTagTests
{
    [Theory]
    [AutoData]
    public void Constructor_WithValidParameters_CreatesPostTag(
        string name,
        string slug,
        string description
    )
    {
        // Act
        var postTag = new PostTag(name, slug, description);

        // Assert
        postTag.Should().NotBeNull();
        postTag.Id.Should().NotBeEmpty();
        postTag.Name.Should().Be(name);
        postTag.Slug.Should().Be(slug);
        postTag.Description.Should().Be(description);
        postTag.CreateAt.Should().NotBe(default(DateTime));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidName_ThrowsEntityRuleException(string? name)
    {
        // Arrange
        var slug = "test-tag";
        var description = "This is a test tag";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() => new PostTag(name!, slug, description));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidSlug_ThrowsEntityRuleException(string? slug)
    {
        // Arrange
        var name = "Test Tag";
        var description = "This is a test tag";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() => new PostTag(name, slug!, description));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidDescription_ThrowsEntityRuleException(string? description)
    {
        // Arrange
        var name = "Test Tag";
        var slug = "test-tag";

        // Act & Assert
        Assert.Throws<EntityRuleException>(() => new PostTag(name, slug, description!));
    }
}
