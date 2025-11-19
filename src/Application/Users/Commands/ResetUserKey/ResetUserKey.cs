using OpenPassword.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.Users.Commands.ResetUserKey;

public class ResetUserKeyCommand : IRequest
{
    public string? UserId { get; init; }
}

public class ResetUserKeyCommandHandler : IRequestHandler<ResetUserKeyCommand>
    {

    private readonly IApplicationDbContext _context;

    public ResetUserKeyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ResetUserKeyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync([ request.UserId! ], cancellationToken);

        entity!.PublicKey = null;

        //TODO: Add Domain Event
        // entity!.DomainEvents.Updated();

        await _context.SaveChangesAsync(cancellationToken);
    }
}






