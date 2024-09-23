using MiniProyectoBanking.Core.Application.ViewModels.Productos;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Interfaces.Services
{
    public interface IProductoService : IGenericService<SaveProductoViewModel, ProductoViewModel, Producto>
    {
        Task<bool> ExisteNumeroCuenta(string numeroCuenta);
        Task<List<ProductoViewModel>> GetAllCuentas(string clienteId);
        Task<List<ProductoViewModel>> GetAllByClienteId(string clienteId);
        Task<List<ProductoViewModel>> GetAllByUserSession(string clienteId);
        Task<ProductoViewModel> GetByNumeroCuenta(string numeroCuenta);
        Task<int> GetTotalProductos();
    }
}
