using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TransactionData.Core.Test
{
    [TestClass]
    public class Iso4217DataProviderTest
    {
        [TestMethod]
        public void CheckEmptyIso4217()
        {
            //Arrange 
            var iso = string.Empty;
            //Act
            var testIsoService = new Iso4217DataProvider();
            var result = testIsoService.ValidateCode(iso);

            //Assert 
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckWrongIso4217()
        {
            //Arrange 
            var iso = "USZ";
            //Act
            var testIsoService = new Iso4217DataProvider();
            var result = testIsoService.ValidateCode(iso);

            //Assert 
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckCorrectIso4217()
        {
            //Arrange 
            var iso = "USD";
            //Act
            var testIsoService = new Iso4217DataProvider();
            var result = testIsoService.ValidateCode(iso);

            //Assert 
            Assert.IsTrue(result);
        }
    }
}
