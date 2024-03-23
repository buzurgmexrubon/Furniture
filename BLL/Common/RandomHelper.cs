namespace BLL.Common;

internal static class RandomHelper
{
  public static string GenerateRandomPassword()
  {
    const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    const string numbers = "0123456789";
    Random random = new();
    string password = string.Empty;

    for (int i = 0; i < 4; i++)
    {
      password += letters[random.Next(letters.Length)];
    }

    for (int i = 0; i < 4; i++)
    {
      password += numbers[random.Next(numbers.Length)];
    }

    return password;
  }
}