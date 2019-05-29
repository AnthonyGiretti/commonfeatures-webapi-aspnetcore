using FluentAssertions;
using FluentValidation.Results;
using WebApiDemo.Validators;
using Xunit;
using System.Linq;
using AutoFixture;

namespace UnitTestsDemo
{
    public class UserValidatorTests
    {
        private readonly Fixture _fixture;
        private readonly UserValidator _validator;
        private const string _validSIN = "510390115";
        private const string _invalidSIN = "123456789";

        public UserValidatorTests()
        {
            _fixture = new Fixture();
            _validator = new UserValidator();
        }

        [Fact]
        public void WhenAUserIsWellSet_ValidationShouldSucceeed()
        {
            // Arrange
            var user = _fixture.Build<WebApiDemo.Models.User>()
                               .With(x => x.SIN, _validSIN)
                               .Create();

            // Act
            ValidationResult validationResult = _validator.Validate(user);

            // Assert
            validationResult
            .IsValid
            .Should()
            .BeTrue();
        }

        [Fact]
        public void WhenSINIsNotValid_ValidationShouldFail()
        {
            // Arrange
            var user = _fixture.Build<WebApiDemo.Models.User>()
                               .With(x => x.SIN, _invalidSIN)
                               .Create();

            // Act
            ValidationResult validationResult = _validator.Validate(user);

            // Assert
            validationResult
            .IsValid
            .Should()
            .BeFalse();

            validationResult
            .Errors
            .Select(x=> x.ErrorMessage)
            .Should()
            .Contain($"SIN ({_invalidSIN}) is not valid.");
        }
    }
}
