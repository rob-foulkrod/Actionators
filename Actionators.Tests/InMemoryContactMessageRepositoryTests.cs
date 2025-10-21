using Xunit;
using Actionators.Web.Models;
using Actionators.Web.Repositories;

namespace Actionators.Tests;

public class InMemoryContactMessageRepositoryTests
{
    [Fact]
    public async Task AddAsync_AddsMessageSuccessfully()
    {
        // Arrange
        var repository = new InMemoryContactMessageRepository();
        var message = new ContactMessage
        {
            Name = "Test User",
            Email = "test@example.com",
            Subject = "Test Subject",
            Message = "Test message content"
        };

        // Act
        await repository.AddAsync(message);
        var allMessages = await repository.GetAllAsync();

        // Assert
        Assert.Single(allMessages);
        Assert.Equal("Test User", allMessages.First().Name);
        Assert.True(message.Id > 0);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllMessages()
    {
        // Arrange
        var repository = new InMemoryContactMessageRepository();
        var message1 = new ContactMessage
        {
            Name = "User 1",
            Email = "user1@example.com",
            Subject = "Subject 1",
            Message = "Message 1"
        };
        var message2 = new ContactMessage
        {
            Name = "User 2",
            Email = "user2@example.com",
            Subject = "Subject 2",
            Message = "Message 2"
        };

        // Act
        await repository.AddAsync(message1);
        await repository.AddAsync(message2);
        var allMessages = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, allMessages.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectMessage()
    {
        // Arrange
        var repository = new InMemoryContactMessageRepository();
        var message = new ContactMessage
        {
            Name = "Test User",
            Email = "test@example.com",
            Subject = "Test Subject",
            Message = "Test message"
        };

        // Act
        await repository.AddAsync(message);
        var retrievedMessage = await repository.GetByIdAsync(message.Id);

        // Assert
        Assert.NotNull(retrievedMessage);
        Assert.Equal("Test User", retrievedMessage.Name);
        Assert.Equal("test@example.com", retrievedMessage.Email);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenMessageNotFound()
    {
        // Arrange
        var repository = new InMemoryContactMessageRepository();

        // Act
        var retrievedMessage = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(retrievedMessage);
    }

    [Fact]
    public async Task DeleteAsync_RemovesMessage_ReturnsTrue()
    {
        // Arrange
        var repository = new InMemoryContactMessageRepository();
        var message = new ContactMessage
        {
            Name = "Test User",
            Email = "test@example.com",
            Subject = "Test Subject",
            Message = "Test message"
        };

        // Act
        await repository.AddAsync(message);
        var result = await repository.DeleteAsync(message.Id);
        var allMessages = await repository.GetAllAsync();

        // Assert
        Assert.True(result);
        Assert.Empty(allMessages);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenMessageNotFound()
    {
        // Arrange
        var repository = new InMemoryContactMessageRepository();

        // Act
        var result = await repository.DeleteAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddAsync_AssignsIncrementingIds()
    {
        // Arrange
        var repository = new InMemoryContactMessageRepository();
        var message1 = new ContactMessage
        {
            Name = "User 1",
            Email = "user1@example.com",
            Subject = "Subject 1",
            Message = "Message 1"
        };
        var message2 = new ContactMessage
        {
            Name = "User 2",
            Email = "user2@example.com",
            Subject = "Subject 2",
            Message = "Message 2"
        };

        // Act
        await repository.AddAsync(message1);
        await repository.AddAsync(message2);

        // Assert
        Assert.Equal(1, message1.Id);
        Assert.Equal(2, message2.Id);
    }
}
