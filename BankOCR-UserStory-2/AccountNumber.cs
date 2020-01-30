using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battousai999.CodeDojoKata.BankOcr.UserStory2
{
    public class AccountNumber
    {
        public string Value { get; private set; }
        public bool IsValid { get; private set; }

        public AccountNumber(string accountNumber)
        {
            if (accountNumber == null)
                throw new ArgumentNullException(nameof(accountNumber));

            if (accountNumber.Any(x => !Char.IsDigit(x)))
                throw new InvalidOperationException("Account number contained a non-digit value.");

            if (accountNumber.Length != 9)
                throw new InvalidOperationException("Account number did not contain nine digits.");

            this.Value = accountNumber;
            this.IsValid = (GetChecksum(accountNumber) == 0);
        }

        private int GetChecksum(string accountNumber)
        {
            Func<string, int> convert = str =>
            {
                if (!Int32.TryParse(str, out var value))
                    throw new InvalidOperationException("Invalid digit value.");

                return value;
            };

            var sum = accountNumber
                .Reverse()
                .Select((x, i) => convert(x.ToString()) * (i + 1))
                .Sum();

            return (sum % 11);
        }
    }
}
