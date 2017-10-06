using System;
using System.Collections.Generic;
using System.Linq;

namespace BankCase
{
    /// <summary>
    /// Represents a bank
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// A list of all the accounts held by this bank
        /// </summary>
        private readonly List<Account> accounts = new List<Account>();

        /// <summary>
        /// Creates an account for a given customer, with an initial deposit
        /// </summary>
        /// <param name="customer">The target customer</param>
        /// <param name="initialDeposit">The initial deposit of the account. Has to be withdrawable from the customer</param>
        /// <returns>The account created and added to the bank</returns>
        public Account CreateAccount(Person customer, Money initialDeposit)
        {
            if(customer.AvailableFunds < initialDeposit)
                throw new ArgumentException($"{nameof(initialDeposit)} is less than {nameof(customer.AvailableFunds)}");

            customer.AvailableFunds -= initialDeposit;

            Account account = new Account(customer.GetNextAccountName(), customer, initialDeposit);
            accounts.Add(account);

            return account;
        }

        /// <summary>
        /// Finds all the accounts owned by a given customer
        /// </summary>
        /// <param name="customer">The target customer</param>
        /// <returns>An array of all the accounts owned by the given customer</returns>
        public Account[] GetAccountsForCustomer(Person customer)
        {
            return accounts.Where(account => account.Owner == customer).ToArray();
        }

        /// <summary>
        /// Deposits a given amount of money into an account, from the owner's fund.
        /// The owner needs to have enough money in their fund to complete the transaction
        /// </summary>
        /// <param name="to">The account that is being deposited into</param>
        /// <param name="amount">The amount of money to deposit</param>
        public void Deposit(Account to, Money amount)
        {
            // Check if owner has enough funds to deposit
            if(to.Owner.AvailableFunds < amount)
                throw new ArgumentException($"{nameof(to.Owner.AvailableFunds)} is less than {nameof(amount)}");

            to.Owner.AvailableFunds -= amount;
            to.Balance += amount;
        }

        /// <summary>
        /// Withdraws a given amount of money from an account, into the owner's fund.
        /// The account needs to have enough money in it to complete the transaction
        /// </summary>
        /// <param name="from">The account that is being withdrawn from</param>
        /// <param name="amount">The amount of money to withdraw</param>
        public void Withdraw(Account from, Money amount)
        {
            // Check if account has enough funds to withdraw
            if (from.Balance < amount)
                throw new ArgumentException($"{nameof(from.Balance)} is less than {nameof(amount)}");

            from.Balance -= amount;
            from.Owner.AvailableFunds += amount;
        }

        /// <summary>
        /// Transfers money from one account to another
        /// The from-account needs to have enough money in it to complete the transaction
        /// </summary>
        /// <param name="from">The from-account</param>
        /// <param name="to">The to-account</param>
        /// <param name="amount">The amount of money to transfer</param>
        public void Transfer(Account from, Account to, Money amount)
        {
            // Check if account has enough funds to transfer
            if (from.Balance < amount)
                throw new ArgumentException($"{nameof(from.Balance)} is less than {nameof(amount)}");

            from.Balance -= amount;
            to.Balance += amount;
        }
    }
}
