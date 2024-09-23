using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Dtos.Account;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Beneficiarios;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Middlewares;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MiniProyectoBanking.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class BeneficiarioController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IBeneficiarioService _beneficiarioService;
        private readonly IProductoService _productoService;
        private readonly IUsuarioService _usuarioService;
        private readonly AuthenticationResponse _usuarioViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BeneficiarioController(ValidateUserSession validateUserSession, IBeneficiarioService beneficiarioService, IUsuarioService usuarioService, IProductoService productoService, IHttpContextAccessor httpContextAccessor)
        {
            _validateUserSession = validateUserSession;
            _productoService = productoService;
            _beneficiarioService = beneficiarioService;
            _usuarioService = usuarioService;
            _httpContextAccessor = httpContextAccessor;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");


        }
        public async Task<IActionResult> Index(string beneficiarioId)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var beneficiarios = await _beneficiarioService.GetAllByUserSession(beneficiarioId);

            if (beneficiarios == null || !beneficiarios.Any())
            {
                TempData["ErrorMensaje"] = "No se encontraron beneficiarios para el cliente.";
                return View(new List<BeneficiarioViewModel>());
            }

            ViewBag.ClienteId = beneficiarioId;
            return View(beneficiarios);
        }

        public IActionResult SaveBeneficiario()
        {
            return View(new SaveBeneficiarioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveBeneficiario(SaveBeneficiarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ModelErrors"] = errors;
                return View(vm);
            }

            try
            {
                var producto = await _productoService.GetByNumeroCuenta(vm.NumeroCuenta);

                if (producto != null)
                {
                    var usuario = await _usuarioService.GetByIdAsync(producto.ClienteId);
                    vm.Nombre = usuario.Nombre;
                    vm.Apellido = usuario.Apellido;
                }
                else
                {
                    throw new Exception("No se encontró un producto con el número de cuenta proporcionado.");
                }

                vm.ClienteId = _usuarioViewModel.Id;
                await _beneficiarioService.Add(vm);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }


            var beneficiario = await _beneficiarioService.GetByIdSaveViewModel(id);
            if (beneficiario == null)
            {
                return NotFound();
            }

            ViewBag.ClienteId = beneficiario.ClienteId;
            return View(beneficiario);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["ErrorMensaje"] = "No tienes permiso para acceder a estas secciones, tienes que iniciar sesión.";
                return RedirectToAction("Index", "Login");
            }

            var Beneficiario = await _beneficiarioService.GetByIdSaveViewModel(id);

            await _beneficiarioService.Delete(id);

            return RedirectToAction("Index", new { clienteId = Beneficiario.ClienteId });
        }
    }
}
