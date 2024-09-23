using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Transacciones;
using MiniProyectoBanking.Middlewares;

namespace MiniProyectoBanking.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class TransferenciaController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IProductoService _productoService;
        private readonly ITransaccionService _transaccionService;


        public TransferenciaController(ValidateUserSession validateUserSession, IProductoService productoService, ITransaccionService transaccionService)
        {
            _validateUserSession = validateUserSession;
            _productoService = productoService;
            _transaccionService = transaccionService;
        }
        public async Task<IActionResult> Index(string clienteId)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var productos = await _productoService.GetAllCuentas(clienteId);
            return View(productos);
        }

        public IActionResult SaveTransferencia(string cuentaOrigenId)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var model = new SaveTransaccionViewModel
            {
                CuentaOrigenId = cuentaOrigenId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTransferencia(SaveTransaccionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ModelErrors"] = errors;
                return View(vm);
            }

            try
            {
                await _transaccionService.Transferir(vm);
                return RedirectToAction("Index", "Transferencia");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }
    }
}
