using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankCase.Tests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void TestCannotCreateNegativeMoney()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Money(-1));
        }
    }
}