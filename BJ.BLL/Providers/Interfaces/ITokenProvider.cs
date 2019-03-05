using BJ.Entities;

namespace BJ.BLL.Providers.Interfaces
{
    public interface ITokenProvider
    {
        string GenerateJwtToken(string email, User user);
    }
}
