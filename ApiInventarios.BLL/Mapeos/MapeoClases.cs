using ApiInventarios.BLL.Dtos;
using ApiInventarios.DLL.Entidades;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiInventarios.BLL.Mapeos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
        }
    }
}