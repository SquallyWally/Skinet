using System.Text;

using Core.Interfaces;

using Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using NSubstitute;

namespace Infrastructure.Tests.Services;

[TestFixture]
public class TokenServiceTests
{
    private IConfiguration _config;

    private SymmetricSecurityKey _key;

    private ITokenService _sut;

    [SetUp]
    public void Setup()
    {
        _config = Substitute.For<IConfiguration>();

        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"] ?? throw new InvalidOperationException()));

        _sut = new TokenService(_config);
    }
}
