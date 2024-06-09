using System.Security.Cryptography;
using System.Text;

namespace Tools.Common.Extensions;

public static class HashUtilities
{
    public static string ComputeSha256(string rawData)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        StringBuilder builder = new StringBuilder();
        foreach (var @byte in bytes)
        {
            builder.Append(@byte.ToString("x2"));
        }
        return builder.ToString();
    }
}