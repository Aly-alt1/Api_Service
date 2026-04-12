using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Servixe.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; set; }

        public static void Registro()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
                //en el caso de tener prerfiles
                //cfg.AddProfile<OtroPerfil>();
            });

            //config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }

    }
}