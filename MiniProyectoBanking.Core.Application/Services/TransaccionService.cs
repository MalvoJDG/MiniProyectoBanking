using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniProyectoBanking.Core.Application.Dtos.Account;
using MiniProyectoBanking.Core.Application.Helpers;
using MiniProyectoBanking.Core.Application.Interfaces.Repositories;
using MiniProyectoBanking.Core.Application.Interfaces.Services;
using MiniProyectoBanking.Core.Application.ViewModels.Transacciones;
using MiniProyectoBanking.Core.Domain.Entities;

namespace MiniProyectoBanking.Core.Application.Services
{
    public class TransaccionService : GenericService<SaveTransaccionViewModel, TransaccionViewModel, Transaccion>, ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _usuarioViewModel;
        private readonly IProductoRepository _productoRepository;
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;

        public TransaccionService(ITransaccionRepository transaccionRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IProductoRepository productoRepository, IProductoService productoService) : base(transaccionRepository, mapper)
        {
            _transaccionRepository = transaccionRepository;
            _httpContextAccessor = httpContextAccessor;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("usuario");
            _mapper = mapper;
            _productoRepository = productoRepository;
            _productoService = productoService;
        }

        public async Task<List<TransaccionService>> GetAllCuentas(string clienteId)
        {
            clienteId = _usuarioViewModel.Id;
            var productos = await _productoRepository.GetAllAsync();
            var productosCliente = productos.Where(p => p.ClienteId == clienteId && p.TipoCuenta == "Cuenta de ahorro").ToList();
            return _mapper.Map<List<TransaccionService>>(productosCliente);
        }
        public async Task Transferir(SaveTransaccionViewModel vm)
        {
            // Lógica de transferencia
            var cuentaOrigen = await _productoRepository.GetByNumeroCuenta(vm.CuentaOrigenId);
            var cuentaDestino = await _productoRepository.GetByNumeroCuenta(vm.CuentaDestinoId);

            if (cuentaOrigen == null || cuentaDestino == null)
            {
                throw new Exception("Una de las cuentas no existe.");
            }

            if (cuentaOrigen.Monto < vm.Monto)
            {
                throw new Exception("Fondos insuficientes en la cuenta de origen.");
            }

            cuentaOrigen.Monto -= vm.Monto;
            cuentaDestino.Monto += vm.Monto;

            await _productoRepository.UpdateSinId(cuentaOrigen);
            await _productoRepository.UpdateSinId(cuentaDestino);
        }

        public async Task<int> GetTotalTransacciones()
        {
            return await _transaccionRepository.CountTransacciones();
        }

        public async Task<int> GetTotalTransaccionesHoy()
        {
            return await _transaccionRepository.CountTransaccionesHoy();
        }

    }
}
