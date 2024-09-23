using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Dtos.Account;

namespace MiniProyectoBanking.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;  

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (usuarioViewModel == null) 
            { 
                return false;
            }

            return true;
        }


        public string GetUserType()
        {
            AuthenticationResponse usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");
            if (usuarioViewModel != null)
            {
                // Suponiendo que tienes un campo `Tipo` en `AuthenticationResponse`
                return usuarioViewModel.Roles.FirstOrDefault(); // Retorna el primer rol como tipo de usuario
            }
            return null;
        }
    }
}
