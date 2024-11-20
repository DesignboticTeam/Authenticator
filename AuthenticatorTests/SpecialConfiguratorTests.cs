using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticatorConnector.Encryptor;
using AuthenticatorConnector.Configuration;
using FluentAssertions;

namespace AuthenticatorTests
{
    public class SpecialConfiguratorTests
    {

        private const string EncryptionKey = "DesingboticTools"; // 16 bytes

        [Fact]
        public void Load_ShouldDecryptAndParseConfigurationSuccessfully()
        {
            // Arrange
            string plainContent = @"
        {
            ""Firebase"": {
                ""ApiKey"": ""your_firebase_api_key_here"",
                ""AuthDomain"": ""your_project_id.firebaseapp.com"",
                ""DatabaseURL"": ""https://your_project_id.firebaseio.com""
            },
            ""AppSettings"": {
                ""Setting1"": ""Value1"",
                ""Setting2"": ""Value2""
            }
        }";

            // Encrypt the plain content
            string encryptedContent = Encryptor.EncryptString(plainContent, EncryptionKey);

            // Write encrypted content to a MemoryStream to simulate a file
            using var encryptedStream = new MemoryStream();
            using (var writer = new StreamWriter(encryptedStream, leaveOpen: true))
            {
                writer.Write(encryptedContent);
            }
            encryptedStream.Position = 0;

            // Create the custom configuration provider
            var provider = new EncryptedJsonConfigurationProvider(new CustomConfigurationSource());

            // Act
            provider.Load(encryptedStream);

            // Assert
            provider.TryGet("Firebase:ApiKey", out string apiKey).Should().BeTrue();
            apiKey.Should().Be("your_firebase_api_key_here");

            provider.TryGet("AppSettings:Setting1", out string setting1).Should().BeTrue();
            setting1.Should().Be("Value1");
        }
    }
}
