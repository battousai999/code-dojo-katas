using System;
using System.Collections.Generic;
using System.Linq;

namespace Battousai999.CodeDojoKata.BankOcr.UserStory1
{
    class Program
    {
        static void Main(string[] _)
        {
            // Test case #1
            var lines = FixTestCase(@". _  _  _  _  _  _  _  _  _ 
                                      .| || || || || || || || || |
                                      .|_||_||_||_||_||_||_||_||_|");

            DisplayTestResults(lines, "000000000");

            lines = FixTestCase(@".
                                  .  |  |  |  |  |  |  |  |  |
                                  .  |  |  |  |  |  |  |  |  |");

            DisplayTestResults(lines, "111111111");

            lines = FixTestCase(@". _  _  _  _  _  _  _  _  _ 
                                  . _| _| _| _| _| _| _| _| _|
                                  .|_ |_ |_ |_ |_ |_ |_ |_ |_ ");

            DisplayTestResults(lines, "222222222");

            lines = FixTestCase(@". _  _  _  _  _  _  _  _  _ 
                                  . _| _| _| _| _| _| _| _| _|
                                  . _| _| _| _| _| _| _| _| _|");

            DisplayTestResults(lines, "333333333");

            lines = FixTestCase(@".
                                  .|_||_||_||_||_||_||_||_||_|
                                  .  |  |  |  |  |  |  |  |  |");

            DisplayTestResults(lines, "444444444");

            lines = FixTestCase(@". _  _  _  _  _  _  _  _  _ 
                                  .|_ |_ |_ |_ |_ |_ |_ |_ |_ 
                                  . _| _| _| _| _| _| _| _| _|");

            DisplayTestResults(lines, "555555555");

            lines = FixTestCase(@". _  _  _  _  _  _  _  _  _ 
                                  .|_ |_ |_ |_ |_ |_ |_ |_ |_ 
                                  .|_||_||_||_||_||_||_||_||_|");

            DisplayTestResults(lines, "666666666");

            lines = FixTestCase(@". _  _  _  _  _  _  _  _  _ 
                                  .  |  |  |  |  |  |  |  |  |
                                  .  |  |  |  |  |  |  |  |  |");

            DisplayTestResults(lines, "777777777");

            lines = FixTestCase(@". _  _  _  _  _  _  _  _  _ 
                                  .|_||_||_||_||_||_||_||_||_|
                                  .|_||_||_||_||_||_||_||_||_|");

            DisplayTestResults(lines, "888888888");

            lines = FixTestCase(@". _  _  _  _  _  _  _  _  _ 
                                  .|_||_||_||_||_||_||_||_||_|
                                  . _| _| _| _| _| _| _| _| _|");

            DisplayTestResults(lines, "999999999");

            lines = FixTestCase(@".    _  _     _  _  _  _  _ 
                                  .  | _| _||_||_ |_   ||_||_|
                                  .  ||_  _|  | _||_|  ||_| _|");

            DisplayTestResults(lines, "123456789");
        }

        public static void DisplayTestResults(IEnumerable<string> lines, string expectedValue)
        {
            var results = ScannedFigureDecoder.Decode(lines)
                .Select(y => new DigitFigure(y.ToByte()))
                .ToText();

            string matchText = (results == expectedValue ? "passed" : $"failed - expected '{expectedValue}'");

            Console.WriteLine($"{results} ({matchText})");
        }

        public static IEnumerable<string> FixTestCase(string text, bool includeTrailingNewline = true)
        {
            return text
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(x => x.TrimStart().Substring(1))
                .Concat(includeTrailingNewline ? String.Empty.ToSingleton() : Enumerable.Empty<string>());
        }
    }
}
