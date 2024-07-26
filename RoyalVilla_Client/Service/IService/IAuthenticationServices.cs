using Models;

namespace RoyalVilla_Client.Service.IService
{

    public interface IAuthenticationServices
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration);

        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication);

        Task Logout();
    }
}
