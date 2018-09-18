using FluentAssertions;
using FluentValidation.Results;
using WebApiDemo.Validators;
using Xunit;
using System.Linq;

namespace UnitTestsDemo
{
    public class UserValidatorTests
    {
        [Fact]
        public void WhenAUserIsWellSet_ValidationShouldSucceeed()
        {
            // Arrange
            var validator = new UserValidator();
            var user = new WebApiDemo.Models.User
            {
                Gender = "test",
                SIN = "510390115",
                FirstName = "anthony",
                LastName = "giretti"
            };

            // Act
            ValidationResult validationResult = validator.Validate(user);

            // Assert
            validationResult
            .IsValid
            .Should()
            .BeTrue();
        }

        [Fact]
        public void WhenASINIsNotValid_ValidationShouldFail()
        {
            // Arrange
            var validator = new UserValidator();
            var user = new WebApiDemo.Models.User
            {
                Gender = "test",
                SIN = "123456789",
                FirstName = "anthony",
                LastName = "giretti"
            };

            // Act
            ValidationResult validationResult = validator.Validate(user);

            // Assert
            validationResult
            .IsValid
            .Should()
            .BeFalse();

            validationResult
            .Errors
            .Select(x=> x.ErrorMessage)
            .Should()
            .Contain("SIN (123456789) is not valid.");
        }
    }
}
