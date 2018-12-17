using FluentAssertions;
using FluentValidation.Results;
using WebApiDemo.Validators;
using Xunit;
using System.Linq;
using WebApiDemo.Models;
using WebApiDemo.Enum;
using AutoMapper.Configuration;
using AutoMapper;
using WebApiDemo.Mapping;
using ExpectedObjects;

namespace UnitTestsDemo
{
    public class UserMappingTests
    {
        public UserMappingTests()
        {
            var mappings = new MapperConfigurationExpression();
            mappings.AddProfile<MyMappingProfiles>();
            Mapper.Initialize(mappings);
        }

        [Fact]
        public void WhenUserEntityIsWellSet_UserIsWellMapped()
        {
            // Arrange
            var entity = new UserEntity
            {
                Gender = Gender.Mister,
                FirstName = "Anthony",
                LastName = "Giretti",
                Id = "123456789"
            };

            var expected = new User
            {
                Gender = "Mister",
                FirstName = "ANTHONY",
                LastName = "GIRETTI",
                SIN = "123456789"
            }.ToExpectedObject();

            // Act
            var user = Mapper.Map<User>(entity);

            // Assert
            expected.ShouldEqual(user);
        }
    }
}
