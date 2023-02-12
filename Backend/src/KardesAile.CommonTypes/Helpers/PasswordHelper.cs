using System.Security.Cryptography;

namespace KardesAile.CommonTypes.Helpers;

public static class PasswordHelper
{
    /// <summary>
    /// Size of salt
    /// </summary>
    private const int SaltSize = 16;

    /// <summary>
    /// Size of hash
    /// </summary>
    private const int HashSize = 20;

    private const int Iterations = 10000;

    /// <summary>
    /// Creates a hash from a password with 10000 iterations
    /// </summary>
    /// <param name="password">the password</param>
    /// <returns>the hash</returns>
    public static string Hash(string password)
    {
        return Hash(password, Iterations);
    }

    /// <summary>
    /// Creates a hash from a password
    /// </summary>
    /// <param name="password">the password</param>
    /// <param name="iterations">number of iterations</param>
    /// <returns>the hash</returns>
    private static string Hash(string password, int iterations)
    {
        //create salt
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        //create hash
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        var hash = pbkdf2.GetBytes(HashSize);

        //combine salt and hash
        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        //convert to base64
        var base64Hash = Convert.ToBase64String(hashBytes);

        //format hash with extra information
        return base64Hash;
    }

    /// <summary>
    /// verify a password against a hash
    /// </summary>
    /// <param name="password">the password</param>
    /// <param name="hashedPassword">the hash</param>
    /// <returns>could be verified?</returns>
    public static bool Verify(string password, string hashedPassword)
    {
        //get hashbytes
        var hashBytes = Convert.FromBase64String(hashedPassword);

        //get salt
        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        //create hash with given salt
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
        byte[] hash = pbkdf2.GetBytes(HashSize);

        //get result
        for (var i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
            {
                return false;
            }
        }
        return true;
    }
}
