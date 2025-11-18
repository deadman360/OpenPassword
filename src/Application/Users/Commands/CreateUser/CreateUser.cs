using OpenPassword.Application.Common.Interfaces;
using OpenPassword.Domain.Enums;
using OpenPassword.Domain.ValueObjects;

namespace OpenPassword.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<string> 
{
public string? Username { get; init; }
public string? Email { get; init; }
public string? Password { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IApplicationDbContext _context;
    
    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        
        var entity = new Domain.Entities.User
        {
            Username = request.Username,
            Email = request.Email,
            Role = Role.User, 
        };
        
        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id!;
    }
}
