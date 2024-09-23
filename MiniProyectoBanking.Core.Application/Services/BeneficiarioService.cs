using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniProyectoBanking.Core.Application.Dtos.Account;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Beneficiarios;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Application.ViewModels.Transacciones;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Services
{
    public class BeneficiarioService : GenericService<SaveBeneficiarioViewModel, BeneficiarioViewModel, Beneficiario>, IBeneficiarioService
    {
        private readonly IBeneficiarioRepository _beneficiarioRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _usuarioViewModel;
        private readonly IMapper _mapper;

        public BeneficiarioService(IBeneficiarioRepository beneficiarioRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IProductoRepository productoRepository, IUsuarioService usuarioService) : base(beneficiarioRepository, mapper)
        {
            _beneficiarioRepository = beneficiarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");
            _mapper = mapper;
            _productoRepository = productoRepository;
            _usuarioService = usuarioService;
        }

        public override async Task<SaveBeneficiarioViewModel> Add(SaveBeneficiarioViewModel vm)
        {
            // Obtén el producto asociado al número de cuenta
            var producto = await _productoRepository.GetByNumeroCuenta(vm.NumeroCuenta);


            if (producto != null)
            {
                var usuario = await _usuarioService.GetByIdAsync(producto.ClienteId);
                vm.Nombre = usuario.Nombre;
                vm.Apellido = usuario.Apellido;
            }
            else
            {
                // Manejar el caso cuando no se encuentra el producto
                throw new Exception("No se encontró un producto con el número de cuenta proporcionado.");
            }

            vm.ClienteId = _usuarioViewModel.Id;
            return await base.Add(vm);
        }


        public async Task<List<BeneficiarioViewModel>> GetAllByUserSession(string benifciarioId)
        {
            benifciarioId = _usuarioViewModel.Id;
            var benificario = await _beneficiarioRepository.GetAllAsync();
            var beneficiarioCliente = benificario.Where(p => p.ClienteId == benifciarioId).ToList();
            return _mapper.Map<List<BeneficiarioViewModel>>(beneficiarioCliente);
        }
    }
}
