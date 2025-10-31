using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPLSQLRepository<T>
    {
        bool Insertar(T entidad);
        bool Actualizar(T entidad);
        bool Eliminar(string id);
        bool Existe(string id);
    }
}
