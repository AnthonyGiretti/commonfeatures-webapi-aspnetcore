using AutoFixture;
using ExpectedObjects;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using WebApiDemo.Helpers;
using WebApiDemo.Models;
using Xunit;
using static UnitTestsDemo.TransactionServiceTests;

namespace UnitTestsDemo
{
    public class DataRowHelpersTests
    {
        public class ToTransactionTests
        {
            private Fixture _fixture;
            public ToTransactionTests()
            {
                _fixture = new Fixture();
            }

            [Fact]
            public void WhenDataRowIsNotNull_ToTransactionShouldMapCorrectlyIntoTransactionObject()
            {
                // Arrange
                var transactionData = _fixture.CreateMany<TransactionData>(1).ToList();
                var dataTable = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(transactionData));
                var expectedResult = new Transaction
                {
                    TransactionId = transactionData[0].Id,
                    TransactionDate = transactionData[0].Date,
                    TransactionAmount = transactionData[0].Amount,
                }.ToExpectedObject();

                // Act
                var transaction = dataTable.Rows[0].ToTransaction();

                // Assert
                expectedResult.ShouldEqual(transaction);
            }
        }
    }
}