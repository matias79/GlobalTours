using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Interfaces
{
    public interface ILugarRepositorio
    {
        Task<Lugar> GetLugarAsync(int id);
        Task<IReadOnlyList<Lugar>> GetLugaresAsync();
        
        
    }
}