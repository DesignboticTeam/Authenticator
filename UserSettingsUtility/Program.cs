using Connector.Encryptor;
class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: EncryptConfig <inputFile> <outputFile> <key>");
            return;
        }

        string inputFile = args[0];
        string outputFile = args[1];
        string key = args[2];

        if (!Encryptor.KeyIsValid(key))
        {
            Console.WriteLine("Invalid key size - must be 16, 24, or 32 bytes for AES.");
            return;
        }

        try
        {
            string plainContent = File.ReadAllText(inputFile);
            string encryptedContent = Encryptor.EncryptString(plainContent, key);
            File.WriteAllText(outputFile, encryptedContent);
            Console.WriteLine($"Encryption successful. Encrypted file saved to {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during encryption: {ex.Message}");
        }
    }
}