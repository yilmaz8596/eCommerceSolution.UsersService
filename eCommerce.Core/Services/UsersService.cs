using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;


namespace eCommerce.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }
        public async Task<AuthenticationResponse?> LoginUser(LoginRequest loginRequest)
        {
            AppUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<AuthenticationResponse>(user) with
            {
                Success = true,
                Token = "token"
            };
        }

        public async Task<AuthenticationResponse?> RegisterUser(RegisterRequest registerRequest)
        {
            AppUser user = _mapper.Map<AppUser>(registerRequest);

            AppUser? createdUser = await _usersRepository.AddUser(user);

            if (createdUser == null)
            {
                return null;
            }

            return _mapper.Map<AuthenticationResponse>(createdUser) with
            {
                Success = true,
                Token = "token"
            };
        }

    }
}
