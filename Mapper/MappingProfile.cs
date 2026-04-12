using Api_Services_Rest.Dmain.Data;
using Api_Servixe.Models.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Direcciones, DireccionDto>()
                 .ForMember(dest => dest.IdDireccion, opt => opt.MapFrom(src => src.idDireccion))

                 .ForMember(dest => dest.IdPerfilUsuario, opt =>
                 opt.MapFrom(src => src.idPerfilUsuario ?? 0))

                 .ForMember(dest => dest.PerfilUsuariosDto, opt =>
                 opt.MapFrom(src => src.perfilUsuario))

                 .ForMember(dest => dest.PerfilUsuariosDto, opt =>
                 {
                     opt.PreCondition(src => src.perfilUsuario != null);
                     opt.MapFrom(src => src.perfilUsuario);
                 });

            CreateMap<DireccionDto, Direcciones>()
                .ForMember(dest => dest.idDireccion, opt => opt.MapFrom(src => src.IdDireccion))
                .ForMember(dest => dest.idPerfilUsuario, opt => opt.MapFrom(src => src.IdPerfilUsuario))
                .ForMember(dest => dest.perfilUsuario, opt => opt.MapFrom(src => src.PerfilUsuariosDto));

            //CreateMap<perfilUsuario, PerfilUsuariosDto>().ReverseMap();
            CreateMap<perfilUsuario, PerfilUsuariosDto>()
                .ForMember(dest => dest.IdPerfilUsuario, opt => opt.MapFrom(src => src.idPerfilUsuario))
                .ForMember(dest => dest.DireccionDto, opt => opt.MapFrom(src => src.Direcciones.Select(d => new DireccionDto
                {
                    IdDireccion = d.idDireccion,
                    Calle = d.calle,
                    Colonia = d.colonia,
                    NumExterior = d.NumExterior,
                    NumInterior = d.NumInterior,
                    Municipio = d.Municipio,
                    IdPerfilUsuario = d.idPerfilUsuario ?? 0,
                    PerfilUsuariosDto = null
                })))

                .ForMember(dest => dest.TelefonoDto, opt => opt.MapFrom(src => src.Telefonos.Select(t => new TelefonoDto
                {
                    IdTelefono = t.idTelefono,
                    Celular = t.celular,
                    Casa = t.casa,
                    Oficina = t.oficina,
                    IdPerfilUsuario = t.idPerfilUsuario ?? 0,
                    PerfilUsuarioDto = null
                })

                ));

            CreateMap<PerfilUsuariosDto, perfilUsuario>()
                .ForMember(dest => dest.idPerfilUsuario, opt => opt.MapFrom(src => src.IdPerfilUsuario))
                .ForMember(dest => dest.Direcciones, o => o.MapFrom(y => y.DireccionDto.Select(d => new Direcciones
                {
                    idDireccion = d.IdDireccion,
                    calle = d.Calle,
                    colonia = d.Colonia,
                    NumExterior = d.NumExterior,
                    NumInterior = d.NumInterior,
                    Municipio = d.Municipio,
                    idPerfilUsuario = d.IdPerfilUsuario,
                    perfilUsuario = null

                })))
                .ForMember(dest => dest.Telefonos, o => o.MapFrom(y => y.TelefonoDto.Select(t => new Telefonos
                {
                    idTelefono = t.IdTelefono,
                    celular = t.Celular,
                    casa = t.Casa,
                    oficina = t.Oficina,
                    idPerfilUsuario = t.IdPerfilUsuario,
                    perfilUsuario = null
                })));

            //CreateMap<Telefonos, TelefonoDto>().ReverseMap();
            CreateMap<Telefonos, TelefonoDto>()
                .ForMember(dest => dest.IdTelefono, opt => opt.MapFrom(src => src.idTelefono))
                .ForMember(dest => dest.IdPerfilUsuario, opt => opt.MapFrom(src => src.idPerfilUsuario ?? 0))
                .ForMember(dest => dest.PerfilUsuarioDto, opt => opt.Ignore()
            );

            CreateMap<TelefonoDto, Telefonos>()
                .ForMember(dest => dest.idTelefono, opt => opt.MapFrom(src => src.IdTelefono))
                .ForMember(dest => dest.perfilUsuario, opt => opt.Ignore());


            //para los roles
            CreateMap<roles, RolesDto>().ReverseMap();
            //para usuarios 

            CreateMap<usuarios, UsuarioDto>()
            .ForMember(dest => dest.UsuarioRolDto, opt => opt.MapFrom(src => src.UsuarioRoles))
            .ReverseMap();

            CreateMap<UsuarioRoles, UsuarioRolesDto>()
                .ForMember(dest => dest.RolesDto, opt => opt.MapFrom(src => src.roles))
                .ReverseMap();
        }
    }
}
