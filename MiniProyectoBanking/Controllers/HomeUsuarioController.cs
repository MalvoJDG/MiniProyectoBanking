using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Middlewares;
using System.Diagnostics;

namespace MiniProyectoBanking.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class HomeUsuarioController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IProductoService _productoService;
            

        public HomeUsuarioController(ValidateUserSession validateUserSession, IProductoService productoService)
        {
            _validateUserSession = validateUserSession;
            _productoService = productoService;
        }
        public async Task<IActionResult> Index(string clienteId)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var productos = await _productoService.GetAllByUserSession(clienteId);
            return View(productos);
        }
    }
}
