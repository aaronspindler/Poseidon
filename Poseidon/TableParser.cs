#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#endregion

namespace Poseidon
{
    /*
     * From https://stackoverflow.com/a/19353995
     */

    /// <summary>
    ///     Table parser
    /// </summary>
    public static class TableParser
    {
        /// <summary>
        /// </summary>
        /// <param name="values"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="valueSelectors"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToStringTable<T>(
            this IEnumerable<T> values,
            string[] columnHeaders,
            params Func<T, object>[] valueSelectors)
        {
            return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        }

        /// <summary>
        /// </summary>
        /// <param name="values"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="valueSelectors"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToStringTable<T>(
            this T[] values,
            string[] columnHeaders,
            params Func<T, object>[] valueSelectors)
        {
            Debug.Assert(columnHeaders.Length == valueSelectors.Length);

            var arrValues = new string[values.Length + 1, valueSelectors.Length];

            // Fill headers
            for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                arrValues[0, colIndex] = columnHeaders[colIndex];

            // Fill table rows
            for (var rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
                    .Invoke(values[rowIndex - 1]).ToString();

            return ToStringTable(arrValues);
        }

        /// <summary>
        ///     Makes the actual string table
        /// </summary>
        /// <param name="arrValues">Array values</param>
        /// <returns>String Table</returns>
        public static string ToStringTable(this string[,] arrValues)
        {
            var maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            var sb = new StringBuilder();
            for (var rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    var cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    sb.Append(" | ");
                    sb.Append(cell);
                }

                // Print end of line
                sb.Append(" | ");
                sb.AppendLine();

                // Print splitter
                if (rowIndex == 0)
                {
                    sb.AppendFormat(" |{0}| ", headerSpliter);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Calculates the maximum column width
        /// </summary>
        /// <returns>The max columns width.</returns>
        /// <param name="arrValues">Arr values.</param>
        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            for (var rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                var newLength = arrValues[rowIndex, colIndex].Length;
                var oldLength = maxColumnsWidth[colIndex];

                if (newLength > oldLength)
                    maxColumnsWidth[colIndex] = newLength;
            }

            return maxColumnsWidth;
        }
    }
}