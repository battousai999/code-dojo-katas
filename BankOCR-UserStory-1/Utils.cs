using System;
using System.Collections.Generic;
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
    }
}
