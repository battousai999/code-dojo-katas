using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Battousai999.CodeDojoKata.BankOcr.UserStory1
{
    public static class Utils
    {
        public static IEnumerable<T> ToSingleton<T>(this T value)
        {
            yield return value;
        }

        public static string ToText(this IEnumerable<DigitFigure> col)
        {
            return String.Join("", col.Select(x => x.ToChar()));
        }

        public static IEnumerable<string> ToLines(this StreamReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> col, Action<T> action)
        {
            if (col == null)
                throw new ArgumentNullException(nameof(col));

            foreach (var item in col)
            {
                action(item);
            }
        }
    }
}
