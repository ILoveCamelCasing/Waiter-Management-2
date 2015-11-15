using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace WaiterManagement.Common.Security
{
    /// <summary>
    /// Klasa haszująca oraz sprawdzająca hasła użytkowników systemu.
    /// <remarks>Używa metodę PBKDF2-SHA1. Oparte na rozwiązaniu na https://crackstation.net/hashing-security.htm#aspsourcecode </remarks>
    /// </summary>
    public static class HashUtility
    {
        #region Private Const Fields
        /// <summary>
        /// Rozmiar, w bajtach, ziarnka soli
        /// </summary>
        private const int SALT_BYTE_SIZE = 16;
        /// <summary>
        /// Rozmiar, w bajtach, hasha
        /// </summary>
        private const int HASH_BYTE_SIZE = 24;
        /// <summary>
        /// Liczba iteracji haszowania PBKDF2
        /// </summary>
        private const int PBKDF2_ITERATIONS = 500;

        // Hasła są składowane w postaci LICZBA_ITERACJI:ZIARNKO_SOLI:HASH_HASLA
        private const int ITERATION_INDEX = 0;
        private const int SALT_INDEX = 1;
        private const int PBKDF2_INDEX = 2;
        private const char DELIMITER = ':';
        #endregion

        #region Private Methods
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2DerivedBytes = new Rfc2898DeriveBytes(password, salt);
            pbkdf2DerivedBytes.IterationCount = iterations;
            return pbkdf2DerivedBytes.GetBytes(outputBytes);
        }

        private static bool SlowEquals(byte[] hashA, byte[] hashB)
        {
            uint diff = (uint)hashA.Length ^ (uint)hashB.Length;
            for (int i = 0; i < hashA.Length && i < hashB.Length; i++)
                diff |= (uint)(hashA[i] ^ hashB[i]);
            return diff == 0;
        }

        /// <summary>
        /// Przycina/Dopełnia string tak, aby miał długość 16
        /// </summary>
        /// <param name="stringToConvert"></param>
        /// <returns></returns>
        private static string CreateBase64String(string stringToConvert)
        {
            if (stringToConvert.Length == 16)
                return stringToConvert;
            else if (stringToConvert.Length > 16)
                return stringToConvert.Substring(0, 16);
            else
                return stringToConvert.PadRight(16, stringToConvert[stringToConvert.Length - 1]);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generowanie pierwszego hasha, używającego jako ziarnka soli string uzyskany z logina
        /// </summary>
        public static string CreateFirstHash(string password, string login)
        {
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(login))
                return String.Empty;

            login = CreateBase64String(login);
            login = login.ToLower();
            byte[] salt = Convert.FromBase64String(login);

            byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

            return String.Format("{0}{1}{2}{1}{3}", PBKDF2_ITERATIONS, DELIMITER, Convert.ToBase64String(salt), Convert.ToBase64String(hash)); 
        }

        public static string CreateSecondHash(string firstHash)
        {
            // Generowanie ziarenka soli
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            cryptoServiceProvider.GetBytes(salt);

            byte[] hash = PBKDF2(firstHash, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

            return String.Format("{0}{1}{2}{1}{3}", PBKDF2_ITERATIONS, DELIMITER, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public static bool ValidatePassword(string firstHash, string secondHash)
        {
            string[] split = secondHash.Split(new char[] { DELIMITER });
            int iterations = Int32.Parse(split[ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            byte[] testHash = PBKDF2(firstHash, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }
        #endregion
    }
}
