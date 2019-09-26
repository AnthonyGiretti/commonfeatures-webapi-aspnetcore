using AutoFixture;
using ExpectedObjects;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("Sample of bad practice")]
        [Fact]
        public void TestIfGetTransactionsByYearWorks()
        {
            DataTable dataTable = new DataTable("MyTable");
            DataColumn idColumn = new DataColumn("id", typeof(int));
            DataColumn amountColumn = new DataColumn("amount", typeof(decimal));
            DataColumn dateColumn = new DataColumn("date", typeof(DateTime));

            dataTable.Columns.Add(idColumn);
            dataTable.Columns.Add(amountColumn);
            dataTable.Columns.Add(dateColumn);

            DataRow newRow = dataTable.NewRow();
            newRow["id"] = 1;
            newRow["amount"] = 10.3m;
            newRow["date"] = new DateTime(2018, 10, 20);
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["id"] = 2;
            newRow["amount"] = 42.1m;
            newRow["date"] = new DateTime(2018, 04, 12);
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["id"] = 3;
            newRow["amount"] = 5.6m;
            newRow["date"] = new DateTime(2018, 07, 2);
            dataTable.Rows.Add(newRow);

            var transactionRepositoryMock = Substitute.For<ITransactionRepository>();
            transactionRepositoryMock.GetTransactionsByYear(Arg.Any<int>()).Returns(x => dataTable);

            var service = new TransactionService(transactionRepositoryMock);

            // Act
            var result = service.GetTransactionsByYear(2010);

            // Assert
            Assert.Equal(1, result[0].TransactionId);
            Assert.Equal(10.3m, result[0].TransactionAmount);
            Assert.Equal("2018-10-20", result[0].TransactionDate.ToString("yyy-MM-dd"));
            Assert.Equal(2, result[1].TransactionId);
            Assert.Equal(42.1m, result[1].TransactionAmount);
            Assert.Equal("2018-04-12", result[1].TransactionDate.ToString("yyy-MM-dd"));
            Assert.Equal(3, result[2].TransactionId);
            Assert.Equal(5.6m, result[2].TransactionAmount);
            Assert.Equal("2018-07-02", result[2].TransactionDate.ToString("yyy-MM-dd"));

            dataTable = new DataTable("MyTable");

            result = service.GetTransactionsByYear(2010);
            Assert.Empty(result);
        }

       
        public void TestIfGetTransactionsByIdWorks()
        {

        }

        public class GetTransactionsByYearTests
        {
            private readonly ITransactionRepository _transactionRepositoryMock;
            private readonly List<TransactionData> _transactionData;
            private const int _year = 2010;

            public GetTransactionsByYearTests()
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

        public class GetTransactionByIdTests
        {
            private readonly ITransactionRepository _transactionRepositoryMock;
            private readonly List<TransactionData> _transactionData;
            private const int _id = 2;

            public GetTransactionByIdTests()
            {
                var fixture = new Fixture();
                _transactionRepositoryMock = Substitute.For<ITransactionRepository>();
                _transactionData = fixture.CreateMany<TransactionData>(1).ToList();
            }

            [Fact]
            public void WhenGetTransactionsByIdReturnADataTable_ShouldReturnOneTransactionAndParametersWellUsed()
            {
                // Arrange
                var service = new TransactionService(_transactionRepositoryMock);
                var dataTable = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(_transactionData));
                var expectedResult = new Transaction
                {
                    TransactionId = _transactionData[0].Id,
                    TransactionDate = _transactionData[0].Date,
                    TransactionAmount = _transactionData[0].Amount,
                }.ToExpectedObject();


                _transactionRepositoryMock.GetTransactionById(Arg.Any<int>()).Returns(x => dataTable);


                // Act
                var result = service.GetTransactionById(_id);

                // Assert
                expectedResult.ShouldEqual(result);
                _transactionRepositoryMock.Received(1).GetTransactionById(Arg.Is(_id));
            }

            [Fact]
            public void WhenGetTransactionsByIdReturnAEmptyDataTable_ShouldReturnNullAndParametersWellUsed()
            {
                // Arrange
                var service = new TransactionService(_transactionRepositoryMock);
                var dataTable = new DataTable("MyTable");

                _transactionRepositoryMock.GetTransactionById(Arg.Any<int>()).Returns(x => dataTable);

                // Act
                var result = service.GetTransactionById(_id);

                // Assert
                result.Should().BeNull();
                _transactionRepositoryMock.Received(1).GetTransactionById(Arg.Is(_id));
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