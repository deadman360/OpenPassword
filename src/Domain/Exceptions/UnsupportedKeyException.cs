namespace OpenPassword.Domain.Exceptions;

public class UnsupportedKeyException : Exception
{
    public UnsupportedKeyException(string value) : base($"value \"{value}\" is unsupported.")
    {
    }
}
