using System;

namespace BankCase
{
    /// <summary>
    /// Represents an positive amount of money
    /// </summary>
    public struct Money
    {
        /// <summary>
        /// Creates a new money container.
        /// The amount needs to be positive
        /// </summary>
        /// <param name="amount">The amount of money to contain</param>
        public Money(decimal amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException($"{nameof(amount)} cannot be negative");

            Amount = amount;
        }

        /// <summary>
        /// The amount of money contained
        /// </summary>
        public decimal Amount { get; }

        public static bool operator >(Money m1, Money m2)
        {
            return m1.Amount > m2.Amount;
        }

        public static bool operator <(Money m1, Money m2)
        {
            return m1.Amount < m2.Amount;
        }

        public static Money operator +(Money m1, Money m2)
        {
            return new Money(m1.Amount + m2.Amount);
        }

        public static Money operator -(Money m1, Money m2)
        {
            return new Money(m1.Amount - m2.Amount);
        }

        public static bool operator ==(Money m1, Money m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Money m1, Money m2)
        {
            return !m1.Equals(m2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Money))
                return false;

            Money money = (Money)obj;
            return money.Amount == Amount;
        }
    }
}