namespace HomeBankingNet8V3.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool VerifyPassword(string passwordHash, string inputPassword);
    }
}