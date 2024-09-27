namespace Project_CL.Services.Encryption
{
    public class EncryptionService
    {
        // Method to hash a password
        public static string HashPassword(string password)
        {
            // Generate a salt and hash the password
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword; // Store this in the database
        }

        // Method to verify a password
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Verify if the entered password matches the stored hash
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }
    }
}
