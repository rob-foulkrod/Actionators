using Xunit;
using System.ComponentModel.DataAnnotations;
using Actionators.Web.Models;

namespace Actionators.Tests;

public class ContactMessageValidationTests
{
    private List<ValidationResult> ValidateModel(ContactMessage model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }

    [Fact]
    public void ContactMessage_WithValidData_PassesValidation()
    {
        // Arrange
        var message = new ContactMessage
        {
            Name = "John Doe",
            Email = "john@example.com",
            Subject = "Valid Subject",
            Message = "This is a valid message with sufficient length"
        };

        // Act
        var results = ValidateModel(message);

        // Assert
        Assert.Empty(results);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ContactMessage_WithInvalidName_FailsValidation(string name)
    {
        // Arrange
        var message = new ContactMessage
        {
            Name = name,
            Email = "john@example.com",
            Subject = "Valid Subject",
            Message = "This is a valid message with sufficient length"
        };

        // Act
        var results = ValidateModel(message);

        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("invalid-email")]
    [InlineData("@example.com")]
    [InlineData("user@")]
    public void ContactMessage_WithInvalidEmail_FailsValidation(string email)
    {
        // Arrange
        var message = new ContactMessage
        {
            Name = "John Doe",
            Email = email,
            Subject = "Valid Subject",
            Message = "This is a valid message with sufficient length"
        };

        // Act
        var results = ValidateModel(message);

        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains("Email"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Hi")]
    public void ContactMessage_WithInvalidSubject_FailsValidation(string subject)
    {
        // Arrange
        var message = new ContactMessage
        {
            Name = "John Doe",
            Email = "john@example.com",
            Subject = subject,
            Message = "This is a valid message with sufficient length"
        };

        // Act
        var results = ValidateModel(message);

        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains("Subject"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Short")]
    public void ContactMessage_WithInvalidMessage_FailsValidation(string messageText)
    {
        // Arrange
        var message = new ContactMessage
        {
            Name = "John Doe",
            Email = "john@example.com",
            Subject = "Valid Subject",
            Message = messageText
        };

        // Act
        var results = ValidateModel(message);

        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains("Message"));
    }

    [Fact]
    public void ContactMessage_NameTooLong_FailsValidation()
    {
        // Arrange
        var message = new ContactMessage
        {
            Name = new string('A', 101),
            Email = "john@example.com",
            Subject = "Valid Subject",
            Message = "This is a valid message with sufficient length"
        };

        // Act
        var results = ValidateModel(message);

        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
    }

    [Fact]
    public void ContactMessage_SubjectTooLong_FailsValidation()
    {
        // Arrange
        var message = new ContactMessage
        {
            Name = "John Doe",
            Email = "john@example.com",
            Subject = new string('A', 201),
            Message = "This is a valid message with sufficient length"
        };

        // Act
        var results = ValidateModel(message);

        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains("Subject"));
    }

    [Fact]
    public void ContactMessage_MessageTooLong_FailsValidation()
    {
        // Arrange
        var message = new ContactMessage
        {
            Name = "John Doe",
            Email = "john@example.com",
            Subject = "Valid Subject",
            Message = new string('A', 1001)
        };

        // Act
        var results = ValidateModel(message);

        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains("Message"));
    }
}
