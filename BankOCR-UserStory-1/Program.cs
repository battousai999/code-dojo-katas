using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using static Battousai.Utils.ConsoleUtils;

namespace Battousai999.CodeDojoKata.BankOcr.UserStory1
{
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
                        .Select(lineSet =>
                        {
                            return ScannedFigureDecoder.Decode(lineSet)
                                .Select(x => new DigitFigure(x.ToByte()))
                                .ToText();
                        })
                        .ForEach(Console.WriteLine);
                }
            }, false, false);
        }
    }
}
