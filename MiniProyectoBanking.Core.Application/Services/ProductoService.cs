using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniProyectoBanking.Core.Application.Dtos.Account;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Application.ViewModels.Usuarios;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Services
{
    public class ProductoService : GenericService<SaveProductoViewModel, ProductoViewModel, Producto>, IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _usuarioViewModel;
        private readonly IMapper _mapper;

        public ProductoService(IProductoRepository productoRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(productoRepository, mapper)
        {
            _productoRepository = productoRepository;
            _httpContextAccessor = httpContextAccessor;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");
            _mapper = mapper;
        }

        public async Task<bool> ExisteNumeroCuenta(string numeroCuenta)
        {
            return await _productoRepository.ExisteNumeroCuenta(numeroCuenta);
        }

        public override async Task<SaveProductoViewModel> Add(SaveProductoViewModel vm)
        {
            vm.ClienteId = vm.ClienteId;
            return await base.Add(vm);
        }

        public async Task<List<ProductoViewModel>> GetAllByClienteId
            (string clienteId)
        {
            var productos = await _productoRepository.GetAllAsync();
            var productosCliente = productos.Where(p => p.ClienteId == clienteId).ToList();
            return _mapper.Map<List<ProductoViewModel>>(productosCliente);
        }

        public async Task<List<ProductoViewModel>> GetAllByUserSession
            (string clienteId)
        {
            clienteId = _usuarioViewModel.Id;
            var productos = await _productoRepository.GetAllAsync();
            var productosCliente = productos.Where(p => p.ClienteId == clienteId).ToList();
            return _mapper.Map<List<ProductoViewModel>>(productosCliente);
        }

        public async Task<List<ProductoViewModel>> GetAllCuentas(string clienteId)
        {
            clienteId = _usuarioViewModel.Id;
            var productos = await _productoRepository.GetAllAsync();
            var productosCliente = productos.Where(p => p.ClienteId == clienteId && p.TipoCuenta == "Cuenta de ahorro").ToList();
            return _mapper.Map<List<ProductoViewModel>>(productosCliente);
        }

        public async Task<int> GetTotalProductos()
        {
            return await _productoRepository.CountProductos();
        }

        public async Task<ProductoViewModel> GetByNumeroCuenta(string numeroCuenta)
        {
            var producto = await _productoRepository.GetByNumeroCuenta(numeroCuenta);
            return _mapper.Map<ProductoViewModel>(producto);
        }




    }
}
