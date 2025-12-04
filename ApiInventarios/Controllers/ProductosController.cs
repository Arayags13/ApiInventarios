using Microsoft.AspNetCore.Mvc;
using ApiInventarios.BLL.Dtos;
using ApiInventarios.BLL.Servicios;

namespace ApiInventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : Controller
    {
        private readonly IProductoServicio _productoServicio;

        public ProductosController(IProductoServicio productoServicio)
        {
            _productoServicio = productoServicio;
        }

        [HttpGet(Name = "ObtenerProductos")]
        public async Task<IActionResult> ObtenerProductos()
        {
            var respuesta = await _productoServicio.ObtenerProductosAsync();
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerProducto")]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var respuesta = await _productoServicio.ObtenerProductoPorIdAsync(id);
            if (respuesta.EsError) return NotFound(respuesta); 
            return Ok(respuesta);
        }

        [HttpPost(Name = "CrearProducto")]
        public async Task<IActionResult> CrearProducto(ProductoDto producto)
        {
            var respuesta = await _productoServicio.CrearProductoAsync(producto);
            if (respuesta.EsError) return BadRequest(respuesta.Mensaje);
            return Ok(respuesta);
        }

        [HttpPut(Name = "ActualizarProducto")]
        public async Task<IActionResult> ActualizarProducto(ProductoDto producto)
        {
            var respuesta = await _productoServicio.ActualizarProductoAsync(producto);
            if (respuesta.EsError) return BadRequest(respuesta.Mensaje);
            return Ok(respuesta);
        }

        [HttpPost("stock/{id}", Name = "ActualizarStock")]
        public async Task<IActionResult> ActualizarStock(int id, [FromBody] int cantidad)
        {
            var respuesta = await _productoServicio.ActualizarStockAsync(id, cantidad);
            if (respuesta.EsError) return BadRequest(respuesta.Mensaje);
            return Ok(respuesta);
        }

        [HttpGet("verificar-stock/{id}/{cantidad}", Name = "VerificarStock")]
        public async Task<IActionResult> VerificarStock(int id, int cantidad)
        {
            var respuesta = await _productoServicio.VerificarStockAsync(id, cantidad);

            if (respuesta.EsError)
            {
                return NotFound(respuesta); 
            }
            return Ok(respuesta);
        }
    }
}