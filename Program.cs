using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Cruxlab.Tournament
{
    public struct PasswordValidationModel
    {
        public string password;
        /// <summary>
        ///     Symbol which should be present in password
        /// </summary>
        public char symbol;
        /// <summary>
        ///     Count range requirement
        /// </summary>
        public (int, int) range;
    }

    public class Program
    {
        /// <summary>
        ///     Check password model for validity
        /// </summary>
        /// <param name="model">Password validation model</param>
        /// <returns>True if password is valid, false otherwise</returns>
        public static bool IsValid(PasswordValidationModel model)
        {
            var symbolsCount = model.password.Count(@char => @char == model.symbol);

            return model.range.Item1 <= symbolsCount && symbolsCount <= model.range.Item2;
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
                    return new PasswordValidationModel
                    {
                        symbol = list[0].First(),
                        range = (int.Parse(rangeArray[0]), int.Parse(rangeArray[1])),
                        password = list[2]
                    };
                });

            var validPasswordCount = passwords.Count(IsValid);

            Console.WriteLine($"Valid password count: {validPasswordCount}");
        }
    }
}
