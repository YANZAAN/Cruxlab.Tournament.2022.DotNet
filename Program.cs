using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Cruxlab.Tournament._2022.DotNet
{
    /// <summary>
    ///     Entity for password info extraction
    /// </summary>
    public struct Password
    {
        public char symbol;
        public Tuple<int, int> range;
        public string password;
    }

    public class Program
    {
        /// <summary>
        ///     Analyzer of password validity
        /// </summary>
        /// <param name="entry">Password entity to analyze</param>
        /// <returns>True if password is valid, false otherwise</returns>
        public static bool IsValid(Password entry)
        {
            var symbolsCount = entry.password.Count(element => element == entry.symbol);

            return entry.range.Item1 <= symbolsCount && symbolsCount <= entry.range.Item2;
        }

        /// <summary>
        ///     Function for valid passwords count computing
        /// </summary>
        /// <param name="enumerable">Passwords enumerable</param>
        /// <returns>Integer valid passwords count</returns>
        public static int GetValidPasswordCount(IEnumerable<Password> enumerable)
        {
            var validPasswordsCount = enumerable
                .Select(IsValid)
                .Count(value => value == true);

            return validPasswordsCount;
        }

        public static void Main(string[] args)
        {
            var passwordRows = Enumerable.Empty<string>();
            try
            {
                passwordRows = File.ReadLines("password-persistence.txt");
            }
            catch (Exception)
            {
                throw;
            }

            var passwords = passwordRows
                .Select(row => row.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(list =>
                {
                    var rangeArray = list[1].Split('-');
                    return new Password
                    {
                        symbol = list[0][0],
                        range = Tuple.Create(int.Parse(rangeArray[0]), int.Parse(rangeArray[1])),
                        password = list[2]
                    };
                });

            var validPasswordCount = GetValidPasswordCount(passwords);

            Console.WriteLine($"Valid password count: {validPasswordCount}");
        }
    }
}
