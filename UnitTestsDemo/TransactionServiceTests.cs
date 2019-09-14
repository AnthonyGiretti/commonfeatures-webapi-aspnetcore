using AutoFixture;
using ExpectedObjects;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApiDemo.Models;
using WebApiDemo.Repositories;
using WebApiDemo.Services;
using Xunit;

namespace UnitTestsDemo
{
    public class TransactionServiceTests
    {
        public class GetsTransactionsByYearTests
        {
            private readonly ITransactionRepository _transactionRepositoryMock;
            private readonly List<TransactionData> _transactionData;
            private const int _year = 2010;

            public GetsTransactionsByYearTests()
            {
                var fixture = new Fixture();
                _transactionRepositoryMock = Substitute.For<ITransactionRepository>();
                _transactionData = fixture.CreateMany<TransactionData>(3).ToList();
            }

            [Fact]
            public void WhenGetTransactionsByYearReturnADataTable_ShouldReturnAListOfTransactionsAndParametersWellUsed()
            {
                // Arrange
                var service = new TransactionService(_transactionRepositoryMock);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(_transactionData));
                var expectedResult = new List<Transaction>
                {
                    new Transaction
                    {
                        TransactionId = _transactionData[0].Id,
                        TransactionDate = _transactionData[0].Date,
                        TransactionAmount = _transactionData[0].Amount,
                    },
                    new Transaction
                    {
                        TransactionId = _transactionData[1].Id,
                        TransactionDate = _transactionData[1].Date,
                        TransactionAmount = _transactionData[1].Amount,
                    },new Transaction
                    {
                        TransactionId = _transactionData[2].Id,
                        TransactionDate = _transactionData[2].Date,
                        TransactionAmount = _transactionData[2].Amount,
                    }
                }.ToExpectedObject();

                _transactionRepositoryMock.GetTransactionsByYear(Arg.Any<int>()).Returns(x => dataTable);
                

                // Act
                var result = service.GetTransactionsByYear(_year);

                // Assert
                expectedResult.ShouldEqual(result);
                _transactionRepositoryMock.Received(1).GetTransactionsByYear(Arg.Is(_year));
            }

            [Fact]
            public void WhenGetTransactionsByYearReturnAEmptyDataTable_ShouldReturnAnEmptyListOfTransactionsAndParametersWellUsed()
            {
                // Arrange
                var service = new TransactionService(_transactionRepositoryMock);
                var dataTable = new DataTable("MyTable");
                var expectedResult = new List<Transaction>().ToExpectedObject();

                _transactionRepositoryMock.GetTransactionsByYear(Arg.Any<int>()).Returns(x => dataTable);

                // Act
                var result = service.GetTransactionsByYear(_year);

                // Assert
                expectedResult.ShouldEqual(result);
                _transactionRepositoryMock.Received(1).GetTransactionsByYear(Arg.Is(_year));
            }
        }

        public class TransactionData
        {
            public int Id { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
        }
    }
}