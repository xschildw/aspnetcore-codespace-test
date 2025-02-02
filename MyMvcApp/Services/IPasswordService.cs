namespace MyMvcApp.Services
{
    public interface IPasswordService
    {
        /// <summary>
        /// Generates a password based on the user's last name, birth year, and favorite color.
        /// </summary>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="birthYear">The user's birth year.</param>
        /// <param name="favoriteColor">The user's favorite color.</param>
        /// <returns>A generated password as a string.</returns>
        string GeneratePassword(string lastName, string birthYear, string favoriteColor);
    }
}