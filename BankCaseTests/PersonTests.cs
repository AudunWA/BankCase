using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankCase.Tests
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void TestGetNextAccountName()
        {
            Person person = new Person("Ola", "Nordmann");
            string accountNameTemplate = $"{person.FirstName} {person.LastName} ";

            Assert.AreEqual(accountNameTemplate + 1, person.GetNextAccountName());
            Assert.AreEqual(accountNameTemplate + 2, person.GetNextAccountName());
            Assert.AreEqual(accountNameTemplate + 3, person.GetNextAccountName());
        }
    }
}