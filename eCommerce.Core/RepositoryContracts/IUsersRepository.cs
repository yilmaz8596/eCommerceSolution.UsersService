


using eCommerce.Core.Entities;

namespace eCommerce.Core.RepositoryContracts
{
    public interface IUsersRepository
    {
        Task<AppUser?> AddUser(AppUser user);

        Task<AppUser?> GetUserByEmailAndPassword(string email, string password);
    }
}
