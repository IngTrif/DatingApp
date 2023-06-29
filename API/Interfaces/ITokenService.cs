

using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
       string CreateToken(AppUser user);//like a contract between interface and inplementation 
    }
}