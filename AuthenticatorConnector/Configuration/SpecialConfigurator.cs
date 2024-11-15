using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace AuthenticatorConnector.Configuration
{
    public static class EncryptedJsonConfigurationExtensions
    {
        public static IConfigurationBuilder AddEncryptedJsonFile(this IConfigurationBuilder builder, string path, string privateKey)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("File path must be a non-empty string.", nameof(path));

            if (string.IsNullOrEmpty(privateKey))
                throw new ArgumentException("Private key must be provided.", nameof(privateKey));

            var source = new EncryptedJsonConfigurationSource
            {
                Path = path,
                Optional = false,
                ReloadOnChange = false,
                //PrivateKey = privateKey
            };

            builder.Add(source);
            return builder;
        }
    }
    public class EncryptedJsonConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new EncryptedJsonConfigurationProvider(this);
        }
    }
        public class EncryptedJsonConfigurationProvider : FileConfigurationProvider
        {
            string _privateKey => "DesingboticTools";
            public EncryptedJsonConfigurationProvider(EncryptedJsonConfigurationSource source) : base(source) {
            
            }

            public override void Load(Stream stream)
            {
                try
                {
                    using var reader = new StreamReader(stream);
                    string encryptedContent = reader.ReadToEnd();
                    string decryptedContent = Encryptor.Encryptor.DecryptString(encryptedContent, _privateKey);
                    Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    using var doc = JsonDocument.Parse(decryptedContent);
                    VisitElement(doc.RootElement);
                }
                catch (Exception e)
                {
                    throw new FormatException("Could not parse the encrypted JSON configuration file.", e);
                }
            }
            private void VisitElement(JsonElement element, string parentPath = "")
            {
                switch (element.ValueKind)
                {
                    case JsonValueKind.Object:
                        foreach (var property in element.EnumerateObject())
                        {
                            string path = string.IsNullOrEmpty(parentPath) ? property.Name : $"{parentPath}:{property.Name}";
                            VisitElement(property.Value, path);
                        }
                        break;

                    case JsonValueKind.Array:
                        int index = 0;
                        foreach (var item in element.EnumerateArray())
                        {
                            string path = $"{parentPath}:{index}";
                            VisitElement(item, path);
                            index++;
                        }
                        break;

                    default:
                        Data[parentPath] = element.ToString();
                        break;
                }
            }
        }
}
