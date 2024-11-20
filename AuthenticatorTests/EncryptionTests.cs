using AuthenticatorConnector.Encryptor;

namespace AuthenticatorTests
{
    public class EncryptionTests
    {
        [Fact]
        public void EnceryptionTest()
        {
            var stringToEncrypt = "TestString";
            var keyToEncrypt = "77170F78341C479DAE2526FFC0993511";

            var encryptedString = Encryptor.EncryptString(stringToEncrypt, keyToEncrypt);

            var decrypedString = Encryptor.DecryptString(encryptedString, keyToEncrypt);

            Assert.Equal(stringToEncrypt, decrypedString);
        }

        // Tests for GetKeyLength
        [Theory]
        [InlineData("1234567890123456", 16)] // 16-byte key (valid for AES-128)
        [InlineData("123456789012345678901234", 24)] // 24-byte key (valid for AES-192)
        [InlineData("12345678901234567890123456789012", 32)] // 32-byte key (valid for AES-256)
        [InlineData("shortkey", 8)] // 8-byte key (invalid length)
        public void GetKeyLength_ShouldReturnCorrectLength(string key, int expectedLength)
        {
            // Act
            double result = Encryptor.GetKeyLength(key);

            // Assert
            Assert.Equal(expectedLength, result);
        }

        // Tests for KeyIsValid
        [Theory]
        [InlineData("1234567890123456", true)] // 16 bytes (valid)
        [InlineData("123456789012345678901234", true)] // 24 bytes (valid)
        [InlineData("12345678901234567890123456789012", true)] // 32 bytes (valid)
        [InlineData("shortkey", false)] // 8 bytes (invalid)
        [InlineData("123456789012345678901234567890123456", false)] // 36 bytes (invalid)
        public void KeyIsValid_ShouldIdentifyValidAndInvalidKeys(string key, bool expectedIsValid)
        {
            // Act
            bool result = Encryptor.KeyIsValid(key);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        // Tests for EncryptString
        [Fact]
        public void EncryptString_WithValidKey_ShouldEncryptSuccessfully()
        {
            // Arrange
            string plainText = "Hello, World!";
            string key = "77170F78341C479DAE2526FFC0993511"; // 32-byte key

            // Act
            string encryptedText = Encryptor.EncryptString(plainText, key);

            // Assert
            Assert.False(string.IsNullOrEmpty(encryptedText)); // Encrypted text should not be null or empty
        }

        [Fact]
        public void EncryptString_WithInvalidKey_ShouldThrowException()
        {
            // Arrange
            string plainText = "Hello, World!";
            string invalidKey = "shortkey"; // Invalid key length (not 16, 24, or 32 bytes)

            // Act & Assert
            Exception ex = Assert.Throws<Exception>(() => Encryptor.EncryptString(plainText, invalidKey));
            Assert.Equal("Invalid Key size - must be 16, 24, or 32 bytes for AES.", ex.Message);
        }

        // Tests for DecryptString
        [Fact]
        public void DecryptString_WithValidKey_ShouldReturnOriginalText()
        {
            // Arrange
            string plainText = "Hello, World!";
            string key = "77170F78341C479DAE2526FFC0993511"; // 32-byte key

            // Act
            string encryptedText = Encryptor.EncryptString(plainText, key);
            string decryptedText = Encryptor.DecryptString(encryptedText, key);

            // Assert
            Assert.Equal(plainText, decryptedText); // Decrypted text should match original plain text
        }

        [Fact]
        public void DecryptString_WithInvalidKey_ShouldThrowException()
        {
            // Arrange
            string plainText = "Hello, World!";
            string validKey = "77170F78341C479DAE2526FFC0993511"; // 32-byte key
            string invalidKey = "shortkey"; // Invalid key length (not 16, 24, or 32 bytes)

            string encryptedText = Encryptor.EncryptString(plainText, validKey);

            // Act & Assert
            Exception ex = Assert.Throws<Exception>(() => Encryptor.DecryptString(encryptedText, invalidKey));
            Assert.Equal("Invalid Key size - must be 16, 24, or 32 bytes for AES.", ex.Message);
        }
    }
}