using System;
using TransactionData.Core.Interfaces;
using TransactionData.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using System.Collections.Generic;
using TransactionData.Core.Messeges;
using System.Linq;
using System.IO;
using ExcelDataReader;

namespace TransactionData.Core.Test
{
    [TestClass]
    public class TransactionProcessTest
    {

        [TestMethod]
        public void Validate_Required_Account_Transaction()
        {
            //Arrange 
            var testTransactionModel = new TransactionModel()
            {
                Account = "",
                Description = "Description",
                CurrencyCode = "USD",
                Amount = "1"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.ValidateExcelContent(testTransactionModel);

            //Assert 
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void Validate_Required_Description_Transaction()
        {
            //Arrange 
            var testTransactionModel = new TransactionModel()
            {
                Account = "Account",
                Description = "",
                CurrencyCode = "USD",
                Amount = "1"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.ValidateExcelContent(testTransactionModel);

            //Assert 
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_Required_CurrencyCode_Transaction()
        {
            //Arrange 
            var testTransactionModel = new TransactionModel()
            {
                Account = "Account",
                Description = "Description",
                CurrencyCode = "",
                Amount = "1"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.ValidateExcelContent(testTransactionModel);

            //Assert 
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_Valid_CurrencyCode_Transaction()
        {
            //Arrange 
            var testTransactionModel = new TransactionModel()
            {
                Account = "Account",
                Description = "Description",
                CurrencyCode = "EFD~%",
                Amount = "1"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.ValidateExcelContent(testTransactionModel);

            //Assert 
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_Required_Amount_Transaction()
        {
            //Arrange 
            var testTransactionModel = new TransactionModel()
            {
                Account = "Account",
                Description = "Description",
                CurrencyCode = "USD",
                Amount = ""
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.ValidateExcelContent(testTransactionModel);

            //Assert 
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_Valid_Amount_Transaction()
        {
            //Arrange 
            var testTransactionModel = new TransactionModel()
            {
                Account = "Account",
                Description = "Description",
                CurrencyCode = "USD",
                Amount = "%$@£$%"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.ValidateExcelContent(testTransactionModel);

            //Assert 
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_Correct_Transaction()
        {
            //Arrange 
            var testTransactionModel = new TransactionModel()
            {
                Account = "Account",
                Description = "Description",
                CurrencyCode = "USD",
                Amount = "800"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.ValidateExcelContent(testTransactionModel);

            //Assert 
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FailedSavingTransaction__Should_Return_An_Error()
        {
            //Arrange 
            List<TransactionModel> testTransactionModelList = new List<TransactionModel>();

            var testTransactionModel = new TransactionModel()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = "5"
            };
            testTransactionModelList.Add(testTransactionModel);

            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            A.CallTo(() => transactionDataProvider.Save(testTransactionModel)).Returns(0);
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.Process(testTransactionModelList);

            //Assert 
            Assert.IsTrue(result.Any(m => m.IsErrored));
        }

        [TestMethod]
        public void ValidSavingTransaction_Should_Not_Return_An_Error()
        {
            //Arrange 
            List<TransactionModel> testTransactionModelList = new List<TransactionModel>();

            var testTransactionModel = new TransactionModel()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = "5"
            };
            testTransactionModelList.Add(testTransactionModel);

            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            A.CallTo(() => transactionDataProvider.Save(testTransactionModel)).Returns(1);
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.Process(testTransactionModelList);

            //Assert 
            Assert.IsFalse(result.Any(m => m.IsErrored));
        }

       

        [TestMethod]
        public void Validate_Excel_File_NoAccount()
        {
            //Arrange 
            var fileName = "transactions_no_account.xlsx";
            var resourceName = $"c:\\temp\\{fileName}";
            
            IExcelDataReader excelReader;

            FileStream stream = File.Open(resourceName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            excelReader = ExcelReaderFactory.CreateReader(stream);

            //Act
            var mockTransactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), mockTransactionDataProvider);

            var errorMessages = testTransaction.ValidateExcelFirstRow(excelReader);
            //Assert 
            Assert.IsTrue(errorMessages.Count > 0);
            Assert.AreEqual($"In uploaded file the first column should be Account", errorMessages[0].Message);
        }

        [TestMethod]
        public void Validate_Excel_File_NoAmount()
        {
            //Arrange 
            var fileName = "transactions_no_amount.xlsx";
            var resourceName = $"c:\\temp\\{fileName}";

            IExcelDataReader excelReader;

            FileStream stream = File.Open(resourceName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            excelReader = ExcelReaderFactory.CreateReader(stream);

            //Act
            var mockTransactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcess(new Iso4217DataProvider(), mockTransactionDataProvider);

            var errorMessages = testTransaction.ValidateExcelFirstRow(excelReader);
            //Assert 
            Assert.IsTrue(errorMessages.Count > 0);
            Assert.AreEqual($"In uploaded file the last column should be Amount", errorMessages[0].Message);
        }
    }
}
