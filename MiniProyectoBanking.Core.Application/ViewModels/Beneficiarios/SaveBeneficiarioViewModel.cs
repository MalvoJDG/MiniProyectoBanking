﻿using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.ViewModels.Beneficiarios
{
    public class SaveBeneficiarioViewModel
    {
        public int Id { get; set; }
        public string? Nombre{ get; set; }
        public string? Apellido { get; set; }
        public string NumeroCuenta { get; set; }
        public string? ClienteId { get; set; }
    }
}
