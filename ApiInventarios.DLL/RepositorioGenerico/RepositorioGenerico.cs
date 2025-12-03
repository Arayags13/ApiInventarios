using ApiInventarios.DLL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInventarios.DLL.RepositorioGenerico
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly Caso2Context _context;
        private readonly DbSet<T> _dbSet;

        public RepositorioGenerico(Caso2Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> ObtenerTodosAsync()
        {

            return await _dbSet.ToListAsync();
        }

        public void AgregarAsync(T entidad)
        {
            _dbSet.Add(entidad);
        }

        public void Actualizar(T entidad)
        {
            _dbSet.Update(entidad);
        }

        public void Eliminar(T entidad)
        {
            _dbSet.Remove(entidad);
        }

        public async Task<bool> GuardarCambiosAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}