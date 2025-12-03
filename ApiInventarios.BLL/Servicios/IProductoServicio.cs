using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiInventarios.BLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiInventarios.BLL.Servicios
{
    public interface IProductoServicio
    {
        Task<CustomResponse<List<ProductoDto>>> ObtenerProductosAsync();
        Task<CustomResponse<ProductoDto>> ObtenerProductoPorIdAsync(int id);
        Task<CustomResponse<ProductoDto>> CrearProductoAsync(ProductoDto productoDto);
        Task<CustomResponse<ProductoDto>> ActualizarProductoAsync(ProductoDto productoDto);

        // Funcionalidades específicas solicitadas en el Caso 2
        Task<CustomResponse<bool>> ActualizarStockAsync(int id, int cantidad); // Sumar o restar
        Task<CustomResponse<bool>> VerificarStockAsync(int id, int cantidadRequerida);
    }
}