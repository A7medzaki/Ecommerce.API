using Store.Data.Entities.IdentityEntities;

namespace Store.Service.Services.TokenService
{
    public interface ITokenService
    {
        public string GenerateToken(AppUser appUser); 
    }
}
