namespace EYD.Data.Interfaces
{
    public interface ICrypto
    {
        string Crypto(string content, string password, string salt);
    }
}