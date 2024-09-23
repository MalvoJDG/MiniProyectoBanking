using MiniProyectoBanking.Core.Application.Dtos.Account;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;

namespace MiniProyectoBanking.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<int> CountUsuariosByEstado(bool estado);
        Task<List<UserDto>> GetAllUsuarios();
        Task<UserDto> GetByIdAsync(string userId);
        Task<string> GetUserTypeAsync(string userId);
        Task<ConfirmEmailResponse> ConfirmUserEmailAsync(EditUsuarioViewModel vm);
        Task<RegisterResponse> EditUserAsync(RegisterRequest request, string origin);
        Task SignOutAsync();
    }
}
