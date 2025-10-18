using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICrud<T>
    {
        bool Insertar(T entidad);
        bool Actualizar(T entidad);
        bool Eliminar(string id);
        T ObtenerPorId(string id);
        List<T> ObtenerTodos();
        bool Existe(string id);
        DataTable ObtenerParaCombo();
    }
}
