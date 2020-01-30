using MoreLinq;
using System;
using System.IO;
using System.Linq;
using static Battousai.Utils.ConsoleUtils;

namespace Battousai999.CodeDojoKata.BankOcr.UserStory2
{
    // User Story 2
    // 
    // Having done that, you quickly realize that the ingenious machine is not in fact infallible. Sometimes it goes wrong 
    // in its scanning. The next step therefore is to validate that the numbers you read are in fact valid account numbers. A 
    // valid account number has a valid checksum. This can be calculated as follows:
    // 
    // account number:  3  4  5  8  8  2  8  6  5
    // position names:  d9 d8 d7 d6 d5 d4 d3 d2 d1
    // 
    // checksum calculation:
    // (d1+2*d2+3*d3+...+9*d9) mod 11 = 0
    // 
    // So now you should also write some code that calculates the checksum for a given number, and identifies if it is a valid 
    // account number.
    class Program
    {
        static void Main(string[] args)
        {
            RunLoggingExceptions(() =>
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Expected a filename as input.");
                    return;
                }

                using (var reader = File.OpenText(args.First()))
                {
                    reader.ToLines()
                        .Batch(4)
                        .Where(x => x.Count() >= 3)
                        .Select(lineSet =>
                        {
                            var text = ScannedFigureDecoder.Decode(lineSet)
                                .Select(x => new DigitFigure(x.ToByte()))
                                .ToText();

                            return new AccountNumber(text);
                        })
                        .ForEach(x => Console.WriteLine($"{x.Value} {(x.IsValid ? "" : "ERR")}"));
                }
            }, false, false);
        }
    }
}
