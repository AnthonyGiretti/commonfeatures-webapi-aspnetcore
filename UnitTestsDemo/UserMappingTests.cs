using AutoFixture;
using AutoMapper;
using AutoMapper.Configuration;
using ExpectedObjects;
using WebApiDemo.Enum;
using WebApiDemo.Mapping;
using WebApiDemo.Models;
using Xunit;

namespace UnitTestsDemo
{
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
            }
        }
    }
}
