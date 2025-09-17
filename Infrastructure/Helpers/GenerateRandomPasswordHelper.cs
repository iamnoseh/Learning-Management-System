namespace Infrastructure.Helpers;

public static class GenerateRandomPasswordHelper
{
    public static string GeneratePassword()
    {
        var digits = "0123456789";
        var lettes = "ABCD";
        var password = new char[8];
        Random random = new Random();
        for (int i = 0; i < 8; i++)
        {
            password[i] = digits[random.Next(digits.Length)];
        }

        for (int i = 0; i < random.Next(8); i++)
        {
            int index = random.Next(random.Next(lettes.Length));
            password[i] = lettes[index];
        }
        
        return new string(password);
    }
}