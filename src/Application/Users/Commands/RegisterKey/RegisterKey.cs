using System.Security.Cryptography;
using NBitcoin;
using OpenPassword.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.Users.Commands.RegisterKey;

public class RegisterKeyCommand : IRequest<string>
{
    public string? UserId { get; init; }
    
    public class RegisterKeyCommandHandler : IRequestHandler<RegisterKeyCommand, string>
    {
        private readonly IApplicationDbContext _context;
        
        public RegisterKeyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
    
        public async Task<string> Handle(RegisterKeyCommand request, CancellationToken cancellationToken)
        {
            var mnemonicPhrase = new Mnemonic(Wordlist.English, WordCount.Twelve);
            string phrase = mnemonicPhrase.ToString();
            
            byte[] seed = mnemonicPhrase.DeriveSeed();
            byte[] privateKeyBytes = seed.Take(32).ToArray();
            using var userPrivateKey = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP256);
            userPrivateKey.ImportECPrivateKey(privateKeyBytes, out _);
            
            
            byte[] publicKeyBytes = userPrivateKey.PublicKey.ExportSubjectPublicKeyInfo();
            string publicKeyBase64 = Convert.ToBase64String(publicKeyBytes);
            
            var entity = await _context.Users.FindAsync(request.UserId, cancellationToken);
            
            entity!.PublicKey = Convert.ToBase64String(publicKeyBytes);
            
            //TODO: Add Domain Event
            entity!.DomainEvents.Updated();
            
            await _context.SaveChangesAsync(cancellationToken);
            return Convert.ToBase64String(privateKeyBytes);
        }
    }
}
