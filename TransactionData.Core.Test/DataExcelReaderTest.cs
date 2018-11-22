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
    public class DataExcelReaderTest
    {
        //[TestMethod]
        //public void ValidateExcelTransactions()
        //{
            ////Arrange 
            //var fileName = "transactionsTest.xlsx";
            //var resourceName = $"c:\\temp\\{fileName}";

            //IExcelDataReader excelReader;

            //FileStream stream = File.Open(resourceName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            //excelReader = ExcelReaderFactory.CreateReader(stream);

            ////Act
            //var mockTransactionDataProvider = A.Fake<ITransactionDataProvider>();
            //var mockTransactionProcess = A.Fake<ITransactionProcess>();
            //var transactionProcessor = new TransactionProcess(new Iso4217DataProvider(), mockTransactionDataProvider);
            //var testTransaction = new DataExcelReader(mockTransactionProcess);

            //A.CallTo(() => mockTransactionDataProvider.Save(A<TransactionModel>._)).Returns(1);
          
            //var errorMessages = testTransaction.ProcessExcelFile(excelReader, true);
            ////Assert 
            //Assert.IsTrue(errorMessages.Count > 0);

        //}
    }
}
