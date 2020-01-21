using System;
using System.Collections.Generic;
using System.Text;

namespace Battousai999.CodeDojoKata.BankOcr.UserStory1
{
    public class ScannedFigureBlock
    {
        private Func<string, byte> firstLineEvaluator;
        private Func<string, byte> secondLineEvaluator;
        private Func<string, byte> thirdLineEvaluator;

        public string FirstLine { get; private set; }
        public string SecondLine { get; private set; }
        public string ThirdLine { get; private set; }

        public ScannedFigureBlock(string firstLine, string secondLine, string thirdLine)
        {
            if ((firstLine?.Length ?? 0) != 3)
                throw new ArgumentException("Must have a length of 3", nameof(firstLine));

            if ((secondLine?.Length ?? 0) != 3)
                throw new ArgumentException("Must have a length of 3", nameof(secondLine));

            if ((thirdLine?.Length ?? 0) != 3)
                throw new ArgumentException("Must have a length of 3", nameof(thirdLine));

            this.FirstLine = firstLine;
            this.SecondLine = secondLine;
            this.ThirdLine = thirdLine;

            Func<char, bool> isUnderscore = ch => ch == '_' || ch == ' ';
            Func<char, bool> isPipe = ch => ch == '|' || ch == ' ';
            Func<char, bool> isSpace = ch => ch == ' ';
            Func<int, Func<char, byte>> toPowerOf = power => ch => (byte)(ch != ' ' ? (1 << power) : 0);
            Func<char, byte> ignore = ch => 0;

            firstLineEvaluator = BuildLineEvaluator(isSpace, isUnderscore, isSpace, ignore, toPowerOf(0), ignore);
            secondLineEvaluator = BuildLineEvaluator(isPipe, isUnderscore, isPipe, toPowerOf(1), toPowerOf(2), toPowerOf(3));
            thirdLineEvaluator = BuildLineEvaluator(isPipe, isUnderscore, isPipe, toPowerOf(4), toPowerOf(5), toPowerOf(6));
        }

        private Func<string, byte> BuildLineEvaluator(Func<char, bool> isFirstCharValid,
            Func<char, bool> isSecondCharValid,
            Func<char, bool> isThirdCharValid,
            Func<char, byte> getFirstCharValue,
            Func<char, byte> getSecondCharValue,
            Func<char, byte> getThirdCharValue)
        {
            return line =>
            {
                var firstChar = line[0];
                var secondChar = line[1];
                var thirdChar = line[2];

                if (!isFirstCharValid(firstChar))
                    throw new InvalidOperationException($"Invalid character ({firstChar}) found in block.");

                if (!isSecondCharValid(secondChar))
                    throw new InvalidOperationException($"Invalid character ({secondChar}) found in block.");

                if (!isThirdCharValid(thirdChar))
                    throw new InvalidOperationException($"Invalid character ({thirdChar}) found in block.");

                return (byte)(getFirstCharValue(firstChar) + getSecondCharValue(secondChar) + getThirdCharValue(thirdChar));
            };
        }

        public bool IsEmpty()
        {
            return (String.IsNullOrWhiteSpace(FirstLine) &&
                String.IsNullOrWhiteSpace(SecondLine) &&
                String.IsNullOrWhiteSpace(ThirdLine));
        }

        public byte ToByte()
        {
            return (byte)(firstLineEvaluator(FirstLine) +
                secondLineEvaluator(SecondLine) +
                thirdLineEvaluator(ThirdLine));
        }
    }
}
