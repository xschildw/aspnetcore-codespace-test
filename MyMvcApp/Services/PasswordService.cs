using System;

namespace MyMvcApp.Services
{
    public class PasswordService : IPasswordService
    {
        /// <summary>
        /// Generates a password by concatenating a random prefix of the last name and a random prefix of the favorite color,
        /// where each prefix's length is a random integer between 3 and the length of the corresponding string (if at least 3 characters),
        /// and then appending the birth year to the result.
        /// All parameters are strings.
        /// The final password is returned in lower-case.
        /// </summary>
        public string GeneratePassword(string lastName, string birthYear, string favoriteColor)
        {
            // Ensure that null values don't cause an exception.
            lastName = lastName ?? string.Empty;
            favoriteColor = favoriteColor ?? string.Empty;
            birthYear = birthYear ?? string.Empty;

            var random = new Random();

            // Determine the substring length for lastName.
            int lastNameLength = lastName.Length;
            int lastNameCut = lastNameLength >= 3 ? random.Next(3, lastNameLength + 1) : lastNameLength;

            // Determine the substring length for favoriteColor.
            int favColorLength = favoriteColor.Length;
            int favColorCut = favColorLength >= 3 ? random.Next(3, favColorLength + 1) : favColorLength;

            // Extract the prefixes.
            string trimmedLastName = lastName.Substring(0, lastNameCut);
            string trimmedFavColor = favoriteColor.Substring(0, favColorCut);

            // Concatenate the prefixes and append the birth year.
            string result = trimmedLastName + trimmedFavColor + birthYear;

            return result.ToLowerInvariant();
        }
    }
}