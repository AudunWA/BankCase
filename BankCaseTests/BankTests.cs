using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankCase.Tests
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        public void TestAccountCannotBeCreatedWithoutEnoughFunds()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(0)
            };
            Assert.ThrowsException<ArgumentException>(() => bank.CreateAccount(person, new Money(50)));
        }

        [TestMethod]
        public void TestAccountHasCorrectValuesAfterCreation()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(75)
            };
            Account account = bank.CreateAccount(person, new Money(50));

            string expectedAccountName = $"{person.FirstName} {person.LastName} 1";

            Assert.AreEqual(new Money(50), account.Balance);
            Assert.AreEqual(person, account.Owner);
            Assert.AreEqual(expectedAccountName, account.Name);
        }

        [TestMethod]
        public void TestFundsGetsTransferredWhenCreatingAccount()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(75)
            };
            Account account = bank.CreateAccount(person, new Money(50));

            Assert.AreEqual(new Money(25), person.AvailableFunds);
            Assert.AreEqual(new Money(50), account.Balance);
        }

        [TestMethod]
        public void TestGetEmptyArrayWhenCustomerHasNoAccounts()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(0)
            };

            Account[] accounts = bank.GetAccountsForCustomer(person);
            Assert.AreEqual(0, accounts.Length);
        }

        [TestMethod]
        public void TestGetArrayOfAccountsForACustomer()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(500)
            };

            Account account1 = bank.CreateAccount(person, new Money(100));
            Account account2 = bank.CreateAccount(person, new Money(50));
            Account account3 = bank.CreateAccount(person, new Money(300));

            Account[] accounts = bank.GetAccountsForCustomer(person);

            Assert.AreEqual(3, accounts.Length);
            Assert.AreEqual(account1, accounts[0]);
            Assert.AreEqual(account2, accounts[1]);
            Assert.AreEqual(account3, accounts[2]);
        }

        [TestMethod]
        public void TestPersonCannotDepositWithoutEnoughFunds()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(0)
            };

            Account account = bank.CreateAccount(person, new Money(0));

            Assert.ThrowsException<ArgumentException>(() => bank.Deposit(account, new Money(1)));
        }

        [TestMethod]
        public void TestBalancesGetsUpdatedWhenDepositing()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(200)
            };

            Account account = bank.CreateAccount(person, new Money(100));

            bank.Deposit(account, new Money(50));

            Assert.AreEqual(new Money(150), account.Balance);
            Assert.AreEqual(new Money(50), person.AvailableFunds);
        }

        [TestMethod]
        public void TestPersonCannotWithdrawWithoutEnoughFunds()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(0)
            };

            Account account = bank.CreateAccount(person, new Money(0));

            Assert.ThrowsException<ArgumentException>(() => bank.Withdraw(account, new Money(1)));
        }

        [TestMethod]
        public void TestBalancesGetsUpdatedWhenWithdrawing()
        {
            Bank bank = new Bank();
            Person person = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(200)
            };

            Account account = bank.CreateAccount(person, new Money(100));

            bank.Withdraw(account, new Money(50));

            Assert.AreEqual(new Money(50), account.Balance);
            Assert.AreEqual(new Money(150), person.AvailableFunds);
        }

        [TestMethod]
        public void TestPersonCannotTransferWithoutEnoughFunds()
        {
            Bank bank = new Bank();
            Person person1 = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(0)
            };
            Person person2 = new Person("Kari", "Nordmann")
            {
                AvailableFunds = new Money(0)
            };

            Account person1Account = bank.CreateAccount(person1, new Money(0));
            Account person2Account = bank.CreateAccount(person2, new Money(0));

            Assert.ThrowsException<ArgumentException>(() => bank.Transfer(person1Account, person2Account, new Money(1)));
        }

        [TestMethod]
        public void TestBalancesGetsUpdatedWhenTransfer()
        {
            Bank bank = new Bank();
            Person person1 = new Person("Ola", "Nordmann")
            {
                AvailableFunds = new Money(200)
            };
            Person person2 = new Person("Kari", "Nordmann")
            {
                AvailableFunds = new Money(50)
            };

            Account person1Account = bank.CreateAccount(person1, new Money(200));
            Account person2Account = bank.CreateAccount(person2, new Money(50));

            bank.Transfer(person1Account, person2Account, new Money(25));
            Assert.AreEqual(new Money(175), person1Account.Balance);
            Assert.AreEqual(new Money(75), person2Account.Balance);
        }
    }
}