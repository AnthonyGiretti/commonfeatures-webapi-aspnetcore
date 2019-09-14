using AutoFixture;
using AutoMapper;
using AutoMapper.Configuration;
using ExpectedObjects;
using FluentAssertions;
using WebApiDemo.Enum;
using WebApiDemo.Mapping;
using WebApiDemo.Models;
using Xunit;

namespace UnitTestsDemo
{
    /*
    public class MappingTests
    {
        public class UserEntityTests
        {
            public UserEntityTests()
            {
                var mappings = new MapperConfigurationExpression();
                mappings.AddProfile<MyMappingProfiles>();
                Mapper.Reset();
                Mapper.Initialize(mappings);
            }

            [Fact]
            public void WhenUserEntityIsWellSet_UserIsWellMapped()
            {
                // Arrange
                var entity = new UserEntity
                {
                    Gender = Gender.Mister,
                    Id = "1",
                    FirstName = "Anthony",
                    LastName = "Giretti"
                };

                // Act
                var user = Mapper.Map<User>(entity);

                // Assert
                Assert.IsType<User>(user);
                Assert.Equal(user.Gender, entity.Gender.ToString());
                Assert.Equal(user.SIN, entity.Id);
                Assert.Equal(user.FirstName, entity.FirstName.ToUpper());
                Assert.Equal(user.LastName, entity.LastName.ToUpper());
            }
        }
        
    }
    */


    public class MappingTests
    {
        public class UserEntityTests
        {
            private readonly Fixture _fixture;

            public UserEntityTests()
            {
                var mappings = new MapperConfigurationExpression();
                mappings.AddProfile<MyMappingProfiles>();
                Mapper.Reset();
                Mapper.Initialize(mappings);

                _fixture = new Fixture();
            }

            [Fact]
            public void WhenUserEntityIsWellSet_UserIsWellMapped()
            {
                // Arrange
                var entity = _fixture.Create<UserEntity>();

                var expected = new User
                {
                    Gender = entity.Gender.ToString(),
                    FirstName = entity.FirstName.ToUpper(),
                    LastName = entity.LastName.ToUpper(),
                    SIN = entity.Id
                }.ToExpectedObject();

                // Act
                var user = Mapper.Map<User>(entity);

                // Assert
                expected.ShouldEqual(user);
                //expected.Should().BeEquivalentTo(user, options => options.ComparingByMembers<User>());
            }
        }
    }
}
