using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInventarios.DLL.RepositorioGenerico
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<T> ObtenerPorIdAsync(int id);
        Task<List<T>> ObtenerTodosAsync();
        void AgregarAsync(T entidad);
        void Actualizar(T entidad);
        void Eliminar(T entidad);

        Task<bool> GuardarCambiosAsync();
    }
}