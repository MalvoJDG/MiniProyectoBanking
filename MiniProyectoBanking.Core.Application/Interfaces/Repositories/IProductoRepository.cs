﻿using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.Interfaces.Repositories
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        Task<bool> ExisteNumeroCuenta(string numeroCuenta);
        Task<int> CountProductos();
        Task<Producto> GetByNumeroCuenta(string numeroCuenta);
    }
}
