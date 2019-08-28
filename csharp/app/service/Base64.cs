namespace advancedbackend.services
{
    public interface IBase64
    {
        string Base64Encode(string plainText);
    }

    public class Base64 : IBase64
    {
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}