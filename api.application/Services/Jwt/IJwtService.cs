using api.domain;

namespace api.application;

public interface IJwtService
{
    string GenerateToken(User user);
}