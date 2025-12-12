using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;


namespace eCommerce.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperDbContext _dbContext;

        public UsersRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AppUser?> AddUser(AppUser user)
        {
            // Generate a new GUID for the user 
             user.UserID = Guid.NewGuid();

            string query = "INSERT INTO public.\"Users\" (\"UserID\", \"Email\", \"PersonName\",\"Gender\",\"Password\") VALUES(@UserID,@Email,@PersonName,@Gender,@Password)";
           int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);

            if(rowCountAffected > 0) {
            return user;
            } 
            else {
                return null;
            }
        }

        public async Task<AppUser?> GetUserByEmailAndPassword(string email, string password)
        {

            string query = "SELECT * FROM public.\"Users\" WHERE \"Email\" = @Email AND \"Password\" = @Password";

            var parameters = new { Email = email, Password = password };

            AppUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<AppUser>(query,parameters);

            return user;
        }
    }
}
