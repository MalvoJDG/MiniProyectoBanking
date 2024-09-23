﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.ViewModels.Usuarios
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe colocar su nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe colocar la contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}
