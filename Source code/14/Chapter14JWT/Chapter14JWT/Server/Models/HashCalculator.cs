namespace Chapter14JWT.Server.Models;

public class HashCalculator : IDisposable
{
    /// <summary>
    /// Converts to encript.
    /// </summary>
    /// <value>
    /// To encript.
    /// </value>
    public string ToEncript { get; init; }
    /// <summary>
    /// Initializes a new instance of the <see cref="HashCalculator"/> class.
    /// </summary>
    /// <param name="toEncrypt">To encrypt.</param>
    public HashCalculator(string toEncrypt)
    {
        ToEncript = toEncrypt;
    }
    /// <summary>
    /// Calculates the hash.
    /// </summary>
    /// <returns></returns>
    public string Calculate()
    {
        string hash = string.Empty;
        hash = GetStringSha256Hash(ToEncript);
        hash = Base64Encode(hash);
        hash = Intercalate(hash);

        return hash;
    }
    /// <summary>
    /// Intercalates the current hash.
    /// </summary>
    /// <param name="toProceed">To proceed.</param>
    /// <returns></returns>
    private string Intercalate(string toProceed)
    {
        var firstHalf = string.Empty;
        var secondHalf = string.Empty;

        for (int i = 0; i < toProceed.Length; i++)
        {
            if (i % 2 == 0)
            {
                firstHalf += toProceed[i];
            }
        }
        for (int i = 0; i < toProceed.Length; i++)
        {
            if (i % 2 != 0)
            {
                secondHalf += toProceed[i];
            }
        }
        return firstHalf + secondHalf;
    }
    /// <summary>
    /// Gets the string sha256 hash.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns></returns>
    private string GetStringSha256Hash(string text)
    {
        var internediate = string.Empty;
        if (String.IsNullOrEmpty(text)) return internediate;
        using (var sha = new System.Security.Cryptography.SHA256Managed())
        {
            byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(textData);
            internediate = BitConverter.ToString(hash).Replace("-", String.Empty);
        }
        return internediate;
    }
    /// <summary>
    /// Base64s encoder of a string.
    /// </summary>
    /// <param name="plainText">The plain text.</param>
    /// <returns></returns>
    private string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    /// <summary>
    /// Base64s decode of a string.
    /// </summary>
    /// <param name="base64EncodedData">The base64 encoded data.</param>
    /// <returns></returns>
    private string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    #region IDisposable
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool v)
    { }
    #endregion
}
