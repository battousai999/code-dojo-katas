using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MoreLinq;

namespace Battousai999.CodeDojoKata.BankOcr.UserStory1
{
    public static class ScannedFigureDecoder
    {

        public static IEnumerable<ScannedFigureBlock> Decode(IEnumerable<string> lines)
        {
            // Try to take 5 lines (to see if there are too many)
            var blockLines = lines.Take(5).ToList();

            if (blockLines.Count < 3 || blockLines.Count > 4)
                throw new ArgumentException("There must be either 3 or 4 lines", nameof(lines));

            if (blockLines.Count == 4 && !String.IsNullOrWhiteSpace(blockLines[3]))
                throw new ArgumentException("The fourth line (if included) must be empty or only contain whitespace", nameof(lines));

            blockLines = blockLines.Take(3).ToList();

            // Helper function to convert IEnumerable<char> to string
            Func<IEnumerable<char>, string> concat = col =>
                col.Aggregate(new StringBuilder(), (acc, x) => acc.Append(x), acc => acc.ToString());

            // Convert List<string> into List<List<"3 character string">> (the inner List<"3 character string">
            // represents a "block"—3 lists, one for each row of the digit, of 3 characters—that is, a 3 x 3 block)
            var blockRows = blockLines.Select(x => x.Batch(3).Select(y => concat(y).PadRight(3, ' '))).ToList();

            // Convert previous list (List<List<string>>) into List<Block>
            return blockRows[0]
                .ZipLongest(
                    blockRows[1],
                    blockRows[2],
                    (row1, row2, row3) => new ScannedFigureBlock(row1 ?? "   ", row2 ?? "   ", row3 ?? "   "))
                .Where(x => !x.IsEmpty())
                .ToList();
        }
    }
}
