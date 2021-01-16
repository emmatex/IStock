namespace Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Entities.Identity.AppUser user);
    }
}
