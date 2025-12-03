using ApiInventarios.BLL.Dtos;
using ApiInventarios.DLL.Entidades;
using AutoMapper;
using ApiInventarios.DLL.RepositorioGenerico;

namespace ApiInventarios.BLL.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IRepositorioGenerico<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoServicio(IRepositorioGenerico<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<ProductoDto>>> ObtenerProductosAsync()
        {
            var respuesta = new CustomResponse<List<ProductoDto>>();
            var productos = await _productoRepositorio.ObtenerTodosAsync();
            respuesta.Data = _mapper.Map<List<ProductoDto>>(productos);
            return respuesta;
        }

        public async Task<CustomResponse<ProductoDto>> ObtenerProductoPorIdAsync(int id)
        {
            var respuesta = new CustomResponse<ProductoDto>();
            var producto = await _productoRepositorio.ObtenerPorIdAsync(id);

            if (producto == null)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Producto no encontrado";
                return respuesta;
            }

            respuesta.Data = _mapper.Map<ProductoDto>(producto);
            return respuesta;
        }

        public async Task<CustomResponse<ProductoDto>> CrearProductoAsync(ProductoDto productoDto)
        {
            var respuesta = new CustomResponse<ProductoDto>();

            // Validaciones básicas (opcional)
            if (productoDto.Precio < 0)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "El precio no puede ser negativo";
                return respuesta;
            }

            var producto = _mapper.Map<Producto>(productoDto);
            _productoRepositorio.AgregarAsync(producto);

            if (!await _productoRepositorio.GuardarCambiosAsync())
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Error al guardar el producto";
                return respuesta;
            }

            respuesta.Data = _mapper.Map<ProductoDto>(producto);
            return respuesta;
        }

        public async Task<CustomResponse<ProductoDto>> ActualizarProductoAsync(ProductoDto productoDto)
        {
            var respuesta = new CustomResponse<ProductoDto>();
            var producto = await _productoRepositorio.ObtenerPorIdAsync(productoDto.Id);

            if (producto == null)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Producto no existe";
                return respuesta;
            }

            // Actualizar campos
            producto.Nombre = productoDto.Nombre;
            producto.Descripcion = productoDto.Descripcion;
            producto.Precio = productoDto.Precio;
            // Nota: El stock normalmente se actualiza por separado, pero si el requerimiento "Modificar productos" incluye stock manual, se puede dejar:
            producto.Stock = productoDto.Stock;

            _productoRepositorio.Actualizar(producto);

            if (!await _productoRepositorio.GuardarCambiosAsync())
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Error al actualizar el producto";
            }

            return respuesta;
        }

        // Requerimiento: Actualizar stock (sumar o restar)
        public async Task<CustomResponse<bool>> ActualizarStockAsync(int id, int cantidad)
        {
            var respuesta = new CustomResponse<bool>();
            var producto = await _productoRepositorio.ObtenerPorIdAsync(id);

            if (producto == null)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Producto no encontrado";
                return respuesta;
            }

            // Sumar cantidad (si es negativa, restará automáticamente)
            producto.Stock += cantidad;

            if (producto.Stock < 0)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Stock insuficiente para realizar la operación";
                return respuesta;
            }

            _productoRepositorio.Actualizar(producto);
            respuesta.Data = await _productoRepositorio.GuardarCambiosAsync();
            return respuesta;
        }

        // Requerimiento: Verificar si hay stock
        public async Task<CustomResponse<bool>> VerificarStockAsync(int id, int cantidadRequerida)
        {
            var respuesta = new CustomResponse<bool>();
            var producto = await _productoRepositorio.ObtenerPorIdAsync(id);

            if (producto == null)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Producto no encontrado";
                respuesta.Data = false;
                return respuesta;
            }

            respuesta.Data = producto.Stock >= cantidadRequerida;
            respuesta.Mensaje = respuesta.Data ? "Hay stock disponible" : "No hay stock suficiente";
            return respuesta;
        }
    }
}