namespace Application.Client.Common.PasswordHasher
{
    public interface IPasswordHasherService
    {
        (string passwordHash, string passwordSalt) Create(string password, string globalSalt);
        bool Verify(string enteredPassword, string storedPassword, string storedSalt, string globalSalt);
    }
}