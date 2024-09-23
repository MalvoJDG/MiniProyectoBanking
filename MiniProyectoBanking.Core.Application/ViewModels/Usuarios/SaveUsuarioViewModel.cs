﻿using MiniProyectoBanking.Core.Application.Enums;
using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.ViewModels.Usuarios
{
    public class SaveUsuarioViewModel
    {

        [Required(ErrorMessage = "Debe colocar su nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe colocar la cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Debe colocar el correo")]
        [EmailAddress]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre de usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debe colocar la contraseña")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContraseña { get; set; }

        [Required(ErrorMessage = "Debe colocar el tipo de usuario")]
        public Roles Tipo { get; set; }
        public bool Estado { get; set; }
        public decimal? Monto { get; set; }

         public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}
