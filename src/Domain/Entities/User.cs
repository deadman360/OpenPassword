namespace OpenPassword.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string? Username { get; set; }

    public string? Email { get; set; }
    
    public string? PublicKey { get; set; }
    public Role? Role { get; set; }
}
