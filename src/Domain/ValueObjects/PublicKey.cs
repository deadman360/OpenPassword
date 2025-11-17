using System.Text.RegularExpressions;

namespace OpenPassword.Domain.ValueObjects;

public class PublicKey(string value) : ValueObject
{
    private static readonly Regex SshRsaPattern = new(
        @"^ssh-rsa\s+[A-Za-z0-9+/]+={0,2}(\s+.+)?$", 
        RegexOptions.Compiled);
    
    public static PublicKey From(string value)
    {
        var key = new PublicKey(value);

        if (!SshRsaPattern.IsMatch(key.Value))
        {
            throw new UnsupportedKeyException(value);
        }

        return key;
    }
    
    public string Value { get; private set; } = string.IsNullOrWhiteSpace(value)?"":value;
    
    public static implicit operator string(PublicKey key)
    {
        return key.ToString();
    }
    
    public static explicit operator PublicKey(string value)
    {
        return From(value);
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
