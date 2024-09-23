using AutoMapper;
using MiniProyectoBanking.Core.Application.Dtos.Account;
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

namespace MiniProyectoBanking.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<EditUsuarioViewModel, SaveUsuarioViewModel>();
            CreateMap<SaveUsuarioViewModel, EditUsuarioViewModel>();

            CreateMap<SaveUsuarioViewModel, Usuario>();

            CreateMap<Usuario, UsuarioViewModel>();

            CreateMap<Usuario, SaveUsuarioViewModel>()
                .ForMember(dest => dest.ConfirmarContraseña, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Beneficiarios, opt => opt.Ignore())
                .ForMember(dest => dest.Productos, opt => opt.Ignore());

            CreateMap<Usuario, EditUsuarioViewModel>();
            CreateMap<EditUsuarioViewModel, Usuario>();

            CreateMap<Producto, ProductoViewModel>();

            CreateMap<ProductoViewModel, SaveProductoViewModel>();

            CreateMap<Producto, SaveProductoViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.TransaccionesOrigen, opt => opt.Ignore())
                .ForMember(dest => dest.TransaccionesDestino, opt => opt.Ignore());

            CreateMap<Transaccion, TransaccionViewModel>();

            CreateMap<Transaccion, SaveTransaccionViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CuentaOrigen, opt => opt.Ignore())
                .ForMember(dest => dest.CuentaDestino, opt => opt.Ignore());

            CreateMap<Beneficiario, BeneficiarioViewModel>();

            CreateMap<Beneficiario, SaveBeneficiarioViewModel>()
                .ReverseMap();

            #region Login
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Register
            CreateMap<SaveUsuarioViewModel, RegisterRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Correo))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.NombreUsuario))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Contraseña))
            .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.ConfirmarContraseña))
            .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Tipo));

            CreateMap<RegisterRequest, SaveUsuarioViewModel>()
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.ConfirmarContraseña, opt => opt.MapFrom(src => src.ConfirmPassword))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Rol))
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.Error, opt => opt.Ignore());

            CreateMap<RegisterRequest, EditUsuarioViewModel>()
               .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Password))
               .ForMember(dest => dest.ConfirmarContraseña, opt => opt.MapFrom(src => src.ConfirmPassword))
               .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Rol))
               .ForMember(dest => dest.HasError, opt => opt.Ignore())
               .ForMember(dest => dest.Error, opt => opt.Ignore())
               .ReverseMap();
            #endregion

            #region UserDto
            CreateMap<UserDto, UsuarioViewModel>()
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EmailConfirmed))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
            .ForMember(dest => dest.Contraseña, opt => opt.Ignore())
            .ForMember(dest => dest.Beneficiarios, opt => opt.Ignore())
            .ForMember(dest => dest.Productos, opt => opt.Ignore());

            CreateMap<UserDto, EditUsuarioViewModel>()
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EmailConfirmed))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
            .ForMember(dest => dest.Contraseña, opt => opt.Ignore())
            .ReverseMap();
            #endregion


        }
    }
}
