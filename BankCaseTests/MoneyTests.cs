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

        [TestMethod]
        public void TestMoneyAddition()
        {
            Money money1 = new Money(25);
            Money money2 = new Money(30);
            Money result = money1 + money2;
            Assert.AreEqual(new Money(55), result);
        }

        [TestMethod]
        public void TestMoneySubstractionCannotHaveNegativeSum()
        {
            Money money1 = new Money(25);
            Money money2 = new Money(30);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => money1 - money2);
        }
    }
}